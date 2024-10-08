﻿using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class CustomModSetting
    {
        public string Key { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string GamePath { get; set; } = null!;

        public string BaseGameKey { get; set; } = null!;

        public string LogoFile { get; set; } = "";

        public string? RulesIniMixSource { get; set; }

        public string RulesIniPath { get; set; } = "";

        public string IniNameMatchDetection { get; set; } = "";

        public string? ArtIniMixSource { get; set; }

        public string ArtIniPath { get; set; } = "";

        public string? AiIniMixSource { get; set; }

        public string AiIniPath { get; set; } = "";

        public bool HasAres { get; set; }

        public bool HasPhobos { get; set; }

        public bool HasPhobosSectionInheritance { get; set; }

        public bool HasVinifera { get; set; }

        public bool HasSectionInheritance { get; set; }

        public string GetRulesIniFilePath()
        {
            return Path.Combine(GamePath, RulesIniPath);
        }

        public IniFile LoadRulesIniFile()
        {
            return IniFile.Load(GetRulesIniFilePath());
        }

        public string GetArtIniFilePath()
        {
            return string.IsNullOrEmpty(ArtIniPath) 
                ? "" 
                : Path.Combine(GamePath, ArtIniPath);
        }

        public IniFile? LoadArtIniFile()
        {
            return string.IsNullOrEmpty(ArtIniPath)
                ? null
                : IniFile.Load(GetArtIniFilePath());
        }

        public string GetAiIniFilePath()
        {
            return string.IsNullOrEmpty(AiIniPath)
                ? ""
                : Path.Combine(GamePath, AiIniPath);
        }

        public IniFile? LoadAiIniFile()
        {
            return string.IsNullOrEmpty(AiIniPath)
                ? null
                : IniFile.Load(GetAiIniFilePath());
        }

        public GameDefinition? ToGameDefinition()
        {
            var baseGameDefinition =
                GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == BaseGameKey);
            if (baseGameDefinition != null)
            {
                return new GameDefinition(this)
                {
                    NewMenuLabel = $"{BaseGameKey} - {Name}",
                    BitmapsFolders = baseGameDefinition.BitmapsFolders,
                    SnippetsFolder = baseGameDefinition.SnippetsFolder,
                    Logo = LogoFile,
                    GameKey = baseGameDefinition.GameKey,
                    GameExecutable = baseGameDefinition.GameExecutable,
                    MixFiles = baseGameDefinition.MixFiles,
                    ResourcesDefaultIniFile = GetRulesIniFilePath(),
                    ResourcesDefaultArtIniFile = GetArtIniFilePath(),
                    ResourcesDefaultAiIniFile = GetAiIniFilePath(),
                    ResourcesDescriptionIniFile = baseGameDefinition.ResourcesDescriptionIniFile ??
                                                  baseGameDefinition.ResourcesDefaultIniFile,
                    Sides = baseGameDefinition.Sides,
                    SaveAsFilename = baseGameDefinition.SaveAsFilename,
                    SaveAsArtFilename = baseGameDefinition.SaveAsArtFilename,
                    SaveAsAiFilename = baseGameDefinition.SaveAsAiFilename,
                    SaveAsRelativeToGameFolder = baseGameDefinition.SaveAsRelativeToGameFolder,
                    SoundIni = baseGameDefinition.SoundIni,
                    UseAres = HasAres,
                    UsePhobos = HasPhobos,
                    UsePhobosSectionInheritance = HasPhobosSectionInheritance,
                    UseVinifera = HasVinifera,
                    UseSectionInheritance = HasSectionInheritance
                };
            }
            return null;
        }
    }
}
