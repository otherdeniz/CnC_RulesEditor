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
            if (filterKeyValue != null)
            {
                hiddenValueKeys.Add(filterKeyValue.Key);
            }
            unitEdit.LoadModel(entity, hiddenValueKeys);
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }
    }
}
