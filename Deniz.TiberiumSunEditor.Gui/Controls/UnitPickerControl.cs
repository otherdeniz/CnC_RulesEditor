using Deniz.TiberiumSunEditor.Gui.Model;
using System.ComponentModel;
using System.Drawing.Imaging;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitPickerControl : UserControl
    {
        private GameEntityModel? _entityModel;
        private bool _isFavorte;
        private int _modificationCount;
        private bool _readonlyMode;
        private bool _isSelected;
        private Bitmap? _unitPicture;
        private Bitmap? _selectedUnitPicture;
        private AnimationRequirementToken? _animationRequirementToken;

        public UnitPickerControl()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(0, Color.White);
        }

        public event EventHandler<EventArgs>? FavoriteClick;

        public event EventHandler<EventArgs>? UnitClick;

        [Browsable(false)]
        public GameEntityModel? EntityModel => _entityModel;

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
                    ? (_selectedUnitPicture ??= UnitPictureGenerator.Instance.GetUnitPicture(_entityModel!, true, null, out _))
                    : _unitPicture;
            }
        }

        public void LoadModel(GameEntityModel entityModel)
        {
            _entityModel = entityModel;
            _entityModel.FileSection.ValueChanged += FileSectionOnValueChanged;
            _unitPicture = UnitPictureGenerator.Instance.GetUnitPicture(entityModel, false, LoadAnimatedThumbnail, out var requirementToken);
            _animationRequirementToken = requirementToken;
            BackgroundImage = _unitPicture;
            RefreshModifications();
            RefreshIsFavorite();
        }

        private void FileSectionOnValueChanged(object? sender, IniFileSectionChangedEventArgs e)
        {
            if (e.Key == "Owner"
                || e.Key == "AnimList"
                || e.Key == "Name")
            {
                _selectedUnitPicture = null;
                if (_animationRequirementToken != null)
                {
                    _animationRequirementToken.StillNeeded = false;
                }
                _unitPicture = UnitPictureGenerator.Instance.GetUnitPicture(_entityModel!, false, LoadAnimatedThumbnail, out var requirementToken);
                _animationRequirementToken = requirementToken;
                BackgroundImage = _isSelected
                    ? (_selectedUnitPicture = UnitPictureGenerator.Instance.GetUnitPicture(_entityModel!, true, null, out _))
                    : _unitPicture;
            }
        }

        public void RefreshIsFavorite()
        {
            IsFavorite = _entityModel!.Favorite;
        }

        public void RefreshModifications()
        {
            ModificationCount = _entityModel!.ModificationCount;
        }

        [Browsable(false)]
        [DefaultValue("")]
        public string UnitKey => _entityModel?.EntityKey ?? "";

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
                }
            }
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
            if (_entityModel != null)
            {
                _entityModel.FileSection.ValueChanged -= FileSectionOnValueChanged;
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
            _entityModel!.Favorite = IsFavorite;
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
    }
}
