using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiScriptEditControl : EntityEditBaseControl
    {
        private bool _doEvents;

        public AiScriptEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            base.LoadEntity(entity, filterKeyValue);
            _doEvents = false;
            labelKey.Text = entity.EntityKey;
            textName.Text = entity.EntityName;
            _doEvents = true;
            LoadValuesGrid();
            LoadTeamsList();
        }

        private void LoadValuesGrid()
        {
            var keyValueModelList = EntityModel!.FileSection.KeyValues
                .Select(k => (num: int.TryParse(k.Key, out var keyNumber) ? (int?)keyNumber : null, k))
                .Where(k => k.num.HasValue)
                .OrderBy(k => k.num)
                .Select(k => new AiScriptKeyValueModel(EntityModel, k.k.Key, null))
                .ToList();
            valuesGrid.DataSource = keyValueModelList;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].Width = 30;
            if (ReadonlyMode)
            {
                valuesGrid.DisplayLayout.Bands[0].Columns["UpImage"].Hidden = true;
                valuesGrid.DisplayLayout.Bands[0].Columns["DownImage"].Hidden = true;
                valuesGrid.DisplayLayout.Bands[0].Columns["DeleteImage"].Hidden = true;
            }
        }

        private void LoadTeamsList(string? selectedTeamKey = null)
        {
            if (EntityModel?.RootModel is AiRootModel aiRootModel)
            {
                var childFilter = new FilterByParentModel(EntityModel, "Script", EntityModel.EntityKey);
                entitiesListTeams.LoadListModel(aiRootModel, aiRootModel.TeamEntities, childFilter, selectedTeamKey);
            }
        }

        protected override void OnReadonlyChanged()
        {
            panelButtons.Visible = !ReadonlyMode;
            toolStripAdd.Visible = !ReadonlyMode;
            entitiesListTeams.ReadonlyMode = ReadonlyMode;
            textName.ReadOnly = ReadonlyMode;
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel!.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }

        private void valuesGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            e.Row.Cells["Key"].Activation = Activation.NoEdit;
            e.Row.Cells["Action"].Activation = Activation.NoEdit;
            e.Row.Cells["ParameterName"].Activation = Activation.NoEdit;
            e.Row.Cells["Parameter"].Activation = Activation.NoEdit;
            e.Row.Cells["Parameter2"].Activation = Activation.NoEdit;
        }

        private void EditScriptAction(AiScriptKeyValueModel keyValueModel)
        {
            if (AiScriptEditForm.ExecuteEdit(this.ParentForm!, (AiRootModel)EntityModel!.RootModel, keyValueModel))
            {
                LoadValuesGrid();
            }
        }

        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (ReadonlyMode) return;
            valuesGrid.Selected.Rows.Clear();
            if (e.Cell.Row.ListObject is AiScriptKeyValueModel keyValueModel)
            {
                if (e.Cell.Column.Key is "Key" or "Action" or "ParameterName" or "Parameter" or "Parameter2")
                {
                    valuesGrid.Selected.Rows.Add(e.Cell.Row);
                    EditScriptAction(keyValueModel);
                }
                else if (e.Cell.Column.Key == "UpImage"
                         && keyValueModel.UpImage != null)
                {
                    var nameLine = EntityModel!.FileSection.KeyValues.FirstOrDefault(k => k.Key == "Name");
                    var lines = EntityModel!.FileSection.Lines.OfType<IniFileLineKeyValue>()
                        .Where(l => int.TryParse(l.Key, out _)).ToList();
                    for (var i = 0; i < lines.Count; i++)
                    {
                        var newKey = i.ToString();
                        if (i.ToString() == keyValueModel.Key)
                        {
                            newKey = (i - 1).ToString();
                        }
                        else if ((i + 1).ToString() == keyValueModel.Key)
                        {
                            newKey = (i + 1).ToString();
                        }
                        lines[i] = new IniFileLineKeyValue(EntityModel!.FileSection, newKey, lines[i].Value, lines[i].Comment, true);
                    }
                    if (nameLine != null)
                    {
                        lines.Insert(0, nameLine);
                    }
                    EntityModel!.FileSection.Lines = lines
                        .OrderBy(l => int.TryParse(l.Key, out var keyNumber) ? keyNumber : -1)
                        .OfType<IniFileLineBase>().ToList();
                    LoadValuesGrid();
                }
                else if (e.Cell.Column.Key == "DownImage"
                         && keyValueModel.DownImage != null)
                {
                    var nameLine = EntityModel!.FileSection.KeyValues.FirstOrDefault(k => k.Key == "Name");
                    var lines = EntityModel!.FileSection.Lines.OfType<IniFileLineKeyValue>()
                        .Where(l => int.TryParse(l.Key, out _)).ToList();
                    for (var i = 0; i < lines.Count; i++)
                    {
                        var newKey = i.ToString();
                        if (i.ToString() == keyValueModel.Key)
                        {
                            newKey = (i + 1).ToString();
                        }
                        else if ((i - 1).ToString() == keyValueModel.Key)
                        {
                            newKey = (i - 1).ToString();
                        }
                        lines[i] = new IniFileLineKeyValue(EntityModel!.FileSection, newKey, lines[i].Value, lines[i].Comment, true);
                    }
                    if (nameLine != null)
                    {
                        lines.Insert(0, nameLine);
                    }
                    EntityModel!.FileSection.Lines = lines
                        .OrderBy(l => int.TryParse(l.Key, out var keyNumber) ? keyNumber : -1)
                        .OfType<IniFileLineBase>().ToList();
                    LoadValuesGrid();
                }
                else if (e.Cell.Column.Key == "DeleteImage")
                {
                    var nameLine = EntityModel!.FileSection.KeyValues.FirstOrDefault(k => k.Key == "Name");
                    var lines = EntityModel!.FileSection.Lines.OfType<IniFileLineKeyValue>()
                        .Where(l => l.Key != keyValueModel.Key && int.TryParse(l.Key, out _)).ToList();
                    for (var i = 0; i < lines.Count; i++)
                    {
                        lines[i] = new IniFileLineKeyValue(EntityModel!.FileSection, i.ToString(), lines[i].Value, lines[i].Comment, true);
                    }
                    if (nameLine != null)
                    {
                        lines.Insert(0, nameLine);
                    }
                    EntityModel!.FileSection.Lines = lines.OfType<IniFileLineBase>().ToList();
                    LoadValuesGrid();
                }
            }
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            var newKey = (EntityModel!.FileSection.GetMaxKeyValue() + 1)?.ToString() ?? "0";
            var keyValueModel = new AiScriptKeyValueModel(EntityModel!, newKey, null);
            if (AiScriptEditForm.ExecuteEdit(this.ParentForm!, (AiRootModel)EntityModel!.RootModel, keyValueModel))
            {
                LoadValuesGrid();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (EntityModel!.RootModel is AiRootModel aiRootModel)
            {
                if (MessageBox.Show("Do you want to delete this Script\n" +
                                    "and all Teams of this Script?\n" +
                                    "and all AITriggers where any Script-Team is Team1?", "Delete with all relatives?", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    // delete all teams
                    var teamsToDelete = aiRootModel.TeamEntities.Where(t =>
                        t.EntityModel.FileSection.KeyValues.Any(k =>
                            k.Key == "Script" && k.Value == EntityModel.EntityKey))
                        .ToList();
                    foreach (var teamToDelete in teamsToDelete)
                    {
                        // delete Triggers (by Team1)
                        var triggersToDelete = aiRootModel.TriggerEntities.Where(t =>
                            t.EntityModel.FileSection.KeyValues.Any(k =>
                                k.Key == "Team1" && k.Value == teamToDelete.EntityModel.EntityKey));
                        aiRootModel.TriggerEntities.RemoveWhere(t => triggersToDelete.Any(d => d.EntityModel.EntityKey == t.EntityModel.EntityKey));
                        aiRootModel.File.GetSection("AITriggerTypes")?.RemoveValues(v => triggersToDelete.Any(d => d.EntityModel.EntityKey == v.Key));
                        // update Triggers (by Team2)
                        foreach (var updateTrigger in aiRootModel.TriggerEntities.Where(t =>
                                     t.EntityModel.FileSection.KeyValues.Any(k =>
                                         k.Key == "Team2" && k.Value == teamToDelete.EntityModel.EntityKey)))
                        {
                            updateTrigger.EntityModel.FileSection.SetValue("Team2", "<none>");
                        }
                        // delete Team
                        aiRootModel.TeamEntities.RemoveWhere(t => t.EntityModel.EntityKey == teamToDelete.EntityModel.EntityKey);
                        aiRootModel.File.GetSection("TeamTypes")?.RemoveValues(v => v.Key == teamToDelete.EntityModel.EntityKey);
                        aiRootModel.LookupItems.RemoveWhere(l => l.Key == teamToDelete.EntityModel.EntityKey);
                        if (aiRootModel.LookupEntities.TryGetValue("TeamTypes", out var teamLookupEntities))
                        {
                            teamLookupEntities.RemoveWhere(l => l.EntityKey == teamToDelete.EntityModel.EntityKey);
                        }
                    }
                    // delete TaskForce
                    aiRootModel.ScriptEntities.RemoveWhere(t => t.EntityModel.EntityKey == EntityModel.EntityKey);
                    aiRootModel.File.GetSection("ScriptTypes")?.RemoveValues(v => v.Key == EntityModel.EntityKey);
                    aiRootModel.LookupItems.RemoveWhere(l => l.Key == EntityModel.EntityKey);
                    if (aiRootModel.LookupEntities.TryGetValue("ScriptTypes", out var lookupEntities))
                    {
                        lookupEntities.RemoveWhere(l => l.EntityKey == EntityModel.EntityKey);
                    }
                    RaiseEntityDeleted();
                }
            }
        }

        private void entitiesListTeams_AddedEntity(object sender, GameEntityEventArgs e)
        {
            e.GameEntity.FileSection.SetValue("Name", AiTeamEditControl.GenerateName(e.GameEntity));
        }
    }
}
