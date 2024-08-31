using System.ComponentModel;
using System.Windows.Forms;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class EntityEditBaseControl : UserControl
    {
        public EntityEditBaseControl()
        {
            InitializeComponent();
        }

        public event EventHandler? NameChanged;

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

        protected void RaiseNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
