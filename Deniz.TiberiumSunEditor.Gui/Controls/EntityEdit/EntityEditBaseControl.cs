using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Model;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class EntityEditBaseControl : UserControl
    {
        private bool _readonlyMode;

        public EntityEditBaseControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? EntityDeleted;
        public event EventHandler<EventArgs>? NameChanged;

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                OnReadonlyChanged();
            }
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public GameEntityModel? EntityModel { get; private set; }

        [Browsable(false)]
        [DefaultValue(null)]
        public FilterByParentModel? FilterKeyValue { get; private set; }

        public virtual void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            EntityModel = entity;
            FilterKeyValue = filterKeyValue;
        }

        protected virtual void OnReadonlyChanged()
        {
        }

        protected void RaiseEntityDeleted()
        {
            EntityDeleted?.Invoke(this, EventArgs.Empty);
        }

        protected void RaiseNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
