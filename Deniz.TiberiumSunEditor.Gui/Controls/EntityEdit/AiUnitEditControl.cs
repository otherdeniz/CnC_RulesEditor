﻿using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiUnitEditControl : EntityEditBaseControl
    {
        public AiUnitEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            base.LoadEntity(entity, filterKeyValue);
            labelName.Text = entity.EntityName;
            labelKey.Text = entity.EntityKey;
            var thumbnail = EntityModel!.Thumbnail;
            if (thumbnail?.Kind == ThumbnailKind.Animation)
            {
                pictureThumbnail.Image = BitmapRepository.Instance.BlankImage;
                thumbnail.LoadAnimationAsync(img =>
                {
                    pictureThumbnail.Image = img;
                });
            }
            else
            {
                pictureThumbnail.Image = thumbnail?.Image
                                         ?? BitmapRepository.Instance.BlankImage;
            }
            RefreshInfoNumber();
            if (entity.RootModel is AiRootModel aiRootModel)
            {
                entitiesListTaskForces.LoadListModel(aiRootModel, aiRootModel.TaskForceEntities,
                    new FilterByParentModel(entity, k => k.Value.EndsWith($",{entity.EntityKey}")));
            }
        }

        public void RefreshInfoNumber()
        {
            if (EntityModel == null) return;
            var infoNumber = EntityModel.GetInfoNumber();
            labelModifications.Text = $"{infoNumber} Task Forces";
            labelModifications.ForeColor = infoNumber == 0
                ? ThemeManager.Instance.CurrentTheme.ControlsTextColor
                : ThemeManager.Instance.CurrentTheme.ModifiedTextColor;
        }

        protected override void OnReadonlyChanged()
        {
            entitiesListTaskForces.ReadonlyMode = ReadonlyMode;
        }

        private void entitiesListTaskForces_AddEntityManual(object sender, EventArgs e)
        {
            if (EntityModel!.RootModel is AiRootModel aiRootModel)
            {
                var newEntityListItem = aiRootModel.AddGameEntity("TaskForces");
                newEntityListItem.EntityModel.FileSection.SetValue("Name", $"1 {EntityModel.GetNameOrKey()}");
                newEntityListItem.EntityModel.FileSection.SetValue("Group", "-1");
                newEntityListItem.EntityModel.FileSection.SetValue("0", $"1,{EntityModel.EntityKey}");
                aiRootModel.RaiseGlobalEntityNotification(EntityModel.EntityKey, "RefreshInfoNumber");
                entitiesListTaskForces.LoadListModel(aiRootModel, aiRootModel.TaskForceEntities,
                    new FilterByParentModel(EntityModel, k => k.Value.EndsWith($",{EntityModel.EntityKey}")),
                    selectKey: newEntityListItem.EntityModel.EntityKey);
            }
        }
    }
}
