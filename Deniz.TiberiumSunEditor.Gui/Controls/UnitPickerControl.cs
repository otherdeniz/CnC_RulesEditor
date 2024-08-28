using Deniz.TiberiumSunEditor.Gui.Model;
using System.ComponentModel;
using System.Drawing.Imaging;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Infragistics.Win;
using Infragistics.Win.UltraWinToolTip;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitPickerControl : UserControl
    {
        private bool _isFavorte;
        private int _modificationCount;
        private bool _readonlyMode;
        private bool _isSelected;
        private Bitmap? _unitPicture;
        private Bitmap? _selectedUnitPicture;
        private AnimationRequirementToken? _animationRequirementToken;
        private List<EntityGroupSetting>? _entityGroups;
        private EntityGroupSetting? _assignedToGroup;
        private bool _wasEmpty = false;

        public UnitPickerControl()
        {
            InitializeComponent();
            pictureFavorite.BackColor = Color.FromArgb(0, 0, 0, 0);
        }

        public event EventHandler<EventArgs>? FavoriteClick;

        public event EventHandler<EventArgs>? UnitClick;

        public event EventHandler<EventArgs>? GroupChanged;

        [Browsable(false)]
        public GameEntityModel? EntityModel { get; private set; }

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                pictureFavorite.Visible = !_readonlyMode;
            }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                BackgroundImage = _isSelected
                    ? (_selectedUnitPicture ??= UnitPictureGenerator.Instance.GetUnitPicture(EntityModel!, true, null, out _))
                    : _unitPicture;
            }
        }

        [Browsable(false)]
        [DefaultValue("")]
        public string UnitKey => EntityModel?.EntityKey ?? "";

        [Browsable(false)]
        [DefaultValue(false)]
        public bool IsFavorite
        {
            get => _isFavorte;
            set
            {
                _isFavorte = value;
                pictureFavorite.Image = _isFavorte
                    ? ImageListComponent.Instance.Favorite24.Images[1]
                    : ImageListComponent.Instance.Favorite24.Images[0];
            }
        }

        [Browsable(false)]
        [DefaultValue(0)]
        public int ModificationCount
        {
            get => _modificationCount;
            set
            {
                _modificationCount = value;
                if (_modificationCount == 0)
                {
                    labelModifications.Visible = false;
                }
                else
                {
                    labelModifications.Visible = true;
                    labelModifications.Text = _modificationCount.ToString();
                    labelModifications.BackColor = Color.FromArgb(0, 0, 0, 0);
                    labelModifications.ForeColor = ThemeManager.Instance.CurrentTheme.ModifiedTextColor;
                }
            }
        }

        public void LoadModel(GameEntityModel entityModel)
        {
            EntityModel = entityModel;
            EntityModel.FileSection.ValueChanged += FileSectionOnValueChanged;
            _wasEmpty = entityModel.FileSection.IsEmpty;
            _unitPicture = UnitPictureGenerator.Instance.GetUnitPicture(entityModel, false, LoadAnimatedThumbnail, out var requirementToken);
            _animationRequirementToken = requirementToken;
            BackgroundImage = _unitPicture;
            RefreshModifications();
            RefreshIsFavorite();
        }

        public void InitGroups(List<EntityGroupSetting> entityGroups, EntityGroupSetting? assignedToGroup)
        {
            _entityGroups = entityGroups;
            _assignedToGroup = assignedToGroup;
            var toolTipInfo = new UltraToolTipInfo
            {
                ToolTipText = "right click to open group-by menu",
                Enabled = DefaultableBoolean.True
            };
            ultraToolTips.SetUltraToolTip(this, toolTipInfo);
            ultraToolTips.SetUltraToolTip(pictureThumbnail, toolTipInfo);
        }

        private void FileSectionOnValueChanged(object? sender, IniFileSectionChangedEventArgs e)
        {
            if (e.Key == "Owner"
                || e.Key == "AnimList"
                || e.Key == "Name"
                || _wasEmpty
                || EntityModel!.FileSection.IsEmpty)
            {
                _wasEmpty = EntityModel!.FileSection.IsEmpty;
                _selectedUnitPicture = null;
                if (_animationRequirementToken != null)
                {
                    _animationRequirementToken.StillNeeded = false;
                }
                _unitPicture = UnitPictureGenerator.Instance.GetUnitPicture(EntityModel!, false, LoadAnimatedThumbnail, out var requirementToken);
                _animationRequirementToken = requirementToken;
                BackgroundImage = _isSelected
                    ? (_selectedUnitPicture = UnitPictureGenerator.Instance.GetUnitPicture(EntityModel!, true, null, out _))
                    : _unitPicture;
            }
        }

        public void RefreshIsFavorite()
        {
            IsFavorite = EntityModel!.Favorite;
        }

        public void RefreshModifications()
        {
            ModificationCount = EntityModel!.ModificationCount;
        }

        private void LoadAnimatedThumbnail(Image animatedImage)
        {
            if (animatedImage.RawFormat.Guid == ImageFormat.Gif.Guid)
            {
                pictureThumbnail.Image = animatedImage;
                pictureThumbnail.Visible = true;
            }
            else
            {
                pictureThumbnail.Visible = false;
            }
        }

        private void DisposeManaged()
        {
            BackgroundImage = null;
            if (EntityModel != null)
            {
                EntityModel.FileSection.ValueChanged -= FileSectionOnValueChanged;
            }
            _unitPicture?.Dispose();
            _unitPicture = null;
            _selectedUnitPicture?.Dispose();
            _selectedUnitPicture = null;
            if (_animationRequirementToken != null)
            {
                _animationRequirementToken.StillNeeded = false;
            }
        }

        private void pictureFavorite_Click(object sender, EventArgs e)
        {
            IsFavorite = !_isFavorte;
            EntityModel!.Favorite = IsFavorite;
            FavoriteClick?.Invoke(this, EventArgs.Empty);
        }

        private void labelModifications_Click(object sender, EventArgs e)
        {
            UnitClick?.Invoke(this, EventArgs.Empty);
        }

        private void UnitPickerControl_Click(object sender, EventArgs e)
        {
            UnitClick?.Invoke(this, EventArgs.Empty);
        }

        private void pictureThumbnail_Click(object sender, EventArgs e)
        {
            UnitClick?.Invoke(this, EventArgs.Empty);
        }

        private void AddToGroup(string groupName)
        {
            UserSettingsFile.Instance.AddEntityToGroup(EntityModel!.EntityType, UnitKey, groupName);
            GroupChanged?.Invoke(this, EventArgs.Empty);
        }

        private void popupButtonNewGroup_Click(object sender, EventArgs e)
        {
            AddToGroup(EntityGroupSetting.NewGroupName);
        }

        private void popupButtonRemoveFromGroup_Click(object sender, EventArgs e)
        {
            UserSettingsFile.Instance.RemoveEntityFromGroups(EntityModel!.EntityType, UnitKey);
            GroupChanged?.Invoke(this, EventArgs.Empty);
        }

        private void InitContextMenu()
        {
            popupButtonRemoveFromGroup.Enabled = _assignedToGroup != null;
            var contextItemsToRemove = contextMenu.Items.OfType<ToolStripMenuItem>().Skip(2).ToList();
            contextItemsToRemove.ForEach(contextMenu.Items.Remove);
            if (_entityGroups != null)
            {
                foreach (var entityGroup in _entityGroups)
                {
                    var toolStripitem = new ToolStripMenuItem($"add to group '{entityGroup.GroupName}'");
                    toolStripitem.Click += (sender, e) => AddToGroup(entityGroup.GroupName);
                    contextMenu.Items.Add(toolStripitem);
                }
            }
        }

        private void UnitPickerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && _entityGroups != null)
            {
                InitContextMenu();
                contextMenu.Show(this, PointToClient(MousePosition));
            }
        }

        private void pictureThumbnail_MouseDown(object sender, MouseEventArgs e)
        {
            UnitPickerControl_MouseDown(sender, e);
        }
    }
}
