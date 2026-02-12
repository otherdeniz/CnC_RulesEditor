using Deniz.TiberiumSunEditor.Gui.OpenRa;

namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public static class MixFileWriter
    {
        public static void Write(string outputPath, List<MixEditorEntry> entries)
        {
            // Build the local mix database from content filenames (lowercase, excluding the LMDB itself)
            var filenames = entries.Select(e => e.FileName.ToLowerInvariant()).ToList();
            var lmdb = new XccLocalDatabase(filenames);
            var lmdbData = lmdb.Data();

            // Add lmdb as an extra entry
            var allEntries = new List<MixEditorEntry>(entries);
            allEntries.Add(new MixEditorEntry("local mix database.dat", lmdbData));

            // Sort all entries by their MIX ID (ascending) to match the C++ std::map<int,...> order.
            // The game engine uses binary search on the index, so entries must be sorted by ID.
            allEntries.Sort((a, b) =>
                ((int)MixFile.GetFileID(a.FileName)).CompareTo((int)MixFile.GetFileID(b.FileName)));

            // Calculate sequential offsets (no alignment — matches C++ writer)
            var offsets = new List<int>();
            int offset = 0;
            for (int i = 0; i < allEntries.Count; i++)
            {
                offsets.Add(offset);
                offset += allEntries[i].Size;
            }
            int bodySize = offset;

            using var stream = File.Create(outputPath);
            using var writer = new BinaryWriter(stream);

            // MIX type (4 bytes) - unencrypted
            writer.Write((int)MixType.DEFAULT);

            // Header (6 bytes): file count (2) + body size (4)
            writer.Write((short)allEntries.Count);
            writer.Write(bodySize);

            // Index entries: each 12 bytes (identifier uint + offset int + size int)
            for (int i = 0; i < allEntries.Count; i++)
            {
                uint id = MixFile.GetFileID(allEntries[i].FileName);
                writer.Write(id);
                writer.Write(offsets[i]);
                writer.Write(allEntries[i].Size);
            }

            // Body: write file data contiguously (no padding — matches C++ writer)
            for (int i = 0; i < allEntries.Count; i++)
            {
                writer.Write(allEntries[i].Data);
            }
        }
    }
}
