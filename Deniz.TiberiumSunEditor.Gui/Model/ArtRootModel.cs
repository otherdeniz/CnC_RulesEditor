using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class ArtRootModel
    {
        private readonly bool _showMissingValues;

        public ArtRootModel(RootModel rulesRootModel,
            IniFile iniFile,
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false)
        {
            _showMissingValues = showMissingValues;
            RulesRootModel = rulesRootModel;
            IniFile = iniFile;
            DefaultFile = defaultFileOverwrite ?? GetDefaultFile(rulesRootModel.FileType.GameDefinition);
        }

        public event EventHandler<EventArgs>? EntitiesChanged;

        public RootModel RulesRootModel { get; }

        public IniFile IniFile { get; }

        public IniFile DefaultFile { get; }

        private static IniFile GetDefaultFile(GameDefinition gameDefinition)
        {
            return !string.IsNullOrEmpty(gameDefinition.ResourcesDefaultIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(gameDefinition.ResourcesDefaultIniFile))
                : new IniFile();
        }

    }
}
