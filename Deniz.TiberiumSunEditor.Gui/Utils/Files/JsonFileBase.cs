using System.Text;
using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;
using Newtonsoft.Json;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Files
{
    public abstract class JsonFileBase
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
#if DEBUG
            Formatting = Formatting.Indented
#endif
        };

        private readonly object _saveLock = new();

        protected virtual string? FilePath { get; set; }

        protected FileChangeWatcher? ChangeWatcher { get; set; }

        //protected static TDatType Load<TDatType>(string dataRoot, bool saveOnCreate = true) where TDatType : DatFileBase, new()
        //{
        //    var datFilePath = GetDatFilePath(dataRoot, typeof(TDatType));
        //    return Load<TDatType>(datFilePath, saveOnCreate);
        //}

        protected static TDatType Load<TDatType>(string datFilePath, bool saveOnCreate = true) where TDatType : JsonFileBase, new()
        {
            if (File.Exists(datFilePath))
            {
                using (var fileStream = File.OpenRead(datFilePath))
                {
                    using (var fileReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        var fileJson = fileReader.ReadToEnd();
                        var data = JsonConvert.DeserializeObject<TDatType>(fileJson, SerializerSettings);
                        if (data == null)
                        {
                            throw new RuntimeException($"Load of file '{datFilePath}' failed");
                        }
                        data.FilePath = datFilePath;
                        return data;
                    }
                }
            }

            var createdDatFile = new TDatType
            {
                FilePath = datFilePath
            };
            if (saveOnCreate)
            {
                createdDatFile.Save();
            }
            return createdDatFile;
        }

        protected static TDatType Load<TDatType>(Stream fileStream) where TDatType : JsonFileBase, new()
        {
            using (var fileReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var fileJson = fileReader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<TDatType>(fileJson, SerializerSettings);
                if (data == null)
                {
                    throw new RuntimeException("Load of Stream failed");
                }
                return data;
            }
        }

        protected static string GetDatFilePath(string dataRoot, Type datType)
        {
            if (datType.GetCustomAttributes(typeof(JsonFileNameAttribute), false).FirstOrDefault() is
                JsonFileNameAttribute attribute)
            {
                return Path.Combine(dataRoot , attribute.Filename);
            }

            throw new MissingCodeException($"DatFileNameAttribute not declared on {datType.FullName}");
        }

        public virtual void Save()
        {
            if (FilePath == null)
            {
                throw new MissingCodeException($"DatFile {this.GetType()} has no FilePath set");
            }
            Task.Run(() =>
            {
                lock (_saveLock)
                {
                    ChangeWatcher?.StopWatching();
                    var directory = new FileInfo(FilePath).Directory;
                    if (directory?.Exists == false)
                    {
                        directory.Create();
                    }
                    using (var fileStream = File.OpenWrite(FilePath))
                    {
                        var utf8WithoutBom = new UTF8Encoding(false);
                        using (var fileWriter = new StreamWriter(fileStream, utf8WithoutBom, leaveOpen: true))
                        {
                            fileWriter.Write(JsonConvert.SerializeObject(this, SerializerSettings));
                            fileWriter.Flush();
                        }

                        fileStream.SetLength(fileStream.Position);
                    }
                    Thread.Sleep(500);
                    ChangeWatcher?.StartWatching();
                }
            });
        }

        public void EnsureExists()
        {
            // we call any function to ensure the instance is alive
            // this is for synchronisation ensurance, when multiple tasks try to acces the first instance at once, we call this method before parallel Tasks begin
            _ = GetType();
        }

    }
}
