using Deniz.TiberiumSunEditor.Gui.Model;
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
                entitiesListTaskForces.LoadModel(aiRootModel.TaskForceEntities, typeof(AiTaskForceEditControl),
                    new FilterByParentModel("0", $"1,{entity.EntityKey}",
                        k => k.Value.EndsWith($",{entity.EntityKey}")));
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

    }
}
