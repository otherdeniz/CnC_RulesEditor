using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Model;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class EntityEditBaseControl : UserControl
    {
        public EntityEditBaseControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public GameEntityModel? EditEntity { get; protected set; }

        public virtual void LoadEntity(GameEntityModel entity)
        {
            EditEntity = entity;
        }
    }
}
