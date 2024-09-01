﻿using Deniz.TiberiumSunEditor.Gui.Model;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiTeamEditControl : EntityEditBaseControl
    {
        public AiTeamEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            base.LoadEntity(entity, filterKeyValue);
            labelKey.Text = entity.EntityKey;
            textName.Text = entity.EntityName;
            var hiddenValueKeys = new List<string> { "Name" };
            if (!string.IsNullOrEmpty(filterKeyValue?.Key))
            {
                hiddenValueKeys.Add(filterKeyValue.Key);
            }
            unitEdit.LoadModel(entity, hiddenValueKeys);
            LoadTriggersList();
        }

        private void LoadTriggersList(string? selectedTeamKey = null)
        {
            if (EntityModel?.RootModel is AiRootModel aiRootModel)
            {
                var childEntities = aiRootModel.TriggerEntities
                    .Where(e => e.EntityModel.FileSection.KeyValues
                        .Any(k => (k.Key == "Team1" && k.Value == EntityModel.EntityKey)
                                  || (k.Key == "Team2" && k.Value == EntityModel.EntityKey)))
                    .ToList();
                entitiesListTriggers.LoadModel(childEntities, typeof(AiTriggerEditControl), selectKey:selectedTeamKey);
            }
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }
    }
}
