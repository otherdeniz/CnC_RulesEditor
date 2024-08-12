using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class ThemeManager
    {
        private static ThemeManager? _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();

        private ThemeManager()
        {
            LoadSelectedTheme();
        }

        public ThemeDefinition CurrentTheme { get; private set; } = null!;

        public void LoadSelectedTheme(string? themeName = null)
        {
            if (themeName != null)
            {
                UserSettingsFile.Instance.SelectedTheme = themeName;
                UserSettingsFile.Instance.Save();
            }
            else
            {
                themeName = UserSettingsFile.Instance.SelectedTheme;
            }
            CurrentTheme =
                ThemesFile.Instance.Themes.FirstOrDefault(t => t.Name == themeName)
                ?? ThemesFile.Instance.Themes.FirstOrDefault()
                ?? new ThemeDefinition {Name = "Default"};
        }

        public void UseTheme(Control control)
        {
            control.BackColor = control.Tag as string == "PLAIN"
                ? CurrentTheme.PlainBackColor
                : CurrentTheme.ControlsBackColor;
            control.ForeColor = CurrentTheme.ControlsTextColor;
            UseThemeRecursive(control.Controls.OfType<Control>());
        }

        public void UseTheme(UltraToolbarsManager toolbarsManager)
        {
            toolbarsManager.ToolbarSettings.HotTrackAppearance.BackColor = CurrentTheme.HotTrackBackColor;
            toolbarsManager.ToolbarSettings.HotTrackAppearance.BorderColor = CurrentTheme.HotTrackBorderColor;
            toolbarsManager.ToolbarSettings.HotTrackAppearance.ForeColor = CurrentTheme.HotTrackTextColor;
            toolbarsManager.MenuSettings.Appearance.BackColor = CurrentTheme.ControlsBackColor;
            toolbarsManager.MenuSettings.IconAreaAppearance.BackColor = CurrentTheme.PlainBackColor;
            toolbarsManager.MenuSettings.HotTrackAppearance.BackColor = CurrentTheme.HotTrackBackColor;
            toolbarsManager.MenuSettings.HotTrackAppearance.BorderColor = CurrentTheme.HotTrackBorderColor;
            toolbarsManager.MenuSettings.HotTrackAppearance.ForeColor = CurrentTheme.HotTrackTextColor;
            foreach (var textBoxTool in toolbarsManager.Tools.OfType<TextBoxTool>())
            {
                textBoxTool.EditAppearance.BackColor = CurrentTheme.TextBoxBackColor;
                textBoxTool.EditAppearance.ForeColor = CurrentTheme.TextBoxTextColor;
            }
        }

        private void UseThemeRecursive(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control.BackColor.A > 0)
                {
                    control.BackColor = control.Tag as string == "PLAIN"
                        ? CurrentTheme.PlainBackColor
                        : CurrentTheme.ControlsBackColor;
                }
                control.ForeColor = CurrentTheme.ControlsTextColor;
                switch (control)
                {
                    case UltraTabControl tabControl:
                        tabControl.UseOsThemes = CurrentTheme.TabsUsesOsTheme
                            ? DefaultableBoolean.True 
                            : DefaultableBoolean.False;
                        UseThemeRecursive(tabControl.Controls.OfType<Control>());
                        break;
                    case UltraGrid gridControl:
                        gridControl.UseOsThemes = CurrentTheme.GridUsesOsTheme
                            ? DefaultableBoolean.True
                            : DefaultableBoolean.False;
                        gridControl.DisplayLayout.Appearance.BackColor = CurrentTheme.ControlsBackColor;
                        gridControl.DisplayLayout.Override.HeaderAppearance.BackColor = CurrentTheme.GridHeaderBackColor;
                        gridControl.DisplayLayout.Override.HeaderAppearance.ForeColor = CurrentTheme.GridHeaderTextColor;
                        gridControl.DisplayLayout.Override.RowSelectorAppearance.BackColor = CurrentTheme.GridHeaderBackColor;
                        gridControl.DisplayLayout.Override.RowSelectorAppearance.ForeColor = CurrentTheme.GridHeaderTextColor;
                        gridControl.DisplayLayout.Override.CellAppearance.BackColor = CurrentTheme.GridEditableCellBackColor;
                        gridControl.DisplayLayout.Override.CellAppearance.ForeColor = CurrentTheme.ControlsTextColor;
                        gridControl.DisplayLayout.Override.GroupByRowAppearance.BackColor = CurrentTheme.GridHeaderBackColor;
                        gridControl.DisplayLayout.Override.GroupByRowAppearance.ForeColor = CurrentTheme.GridHeaderTextColor;
                        gridControl.DisplayLayout.ScrollBarLook.Appearance.BackColor = CurrentTheme.ControlsBackColor;
                        gridControl.DisplayLayout.ScrollBarLook.Appearance.ForeColor = CurrentTheme.ControlsTextColor;
                        gridControl.DisplayLayout.DefaultSelectedBackColor = CurrentTheme.GridHeaderBackColor;
                        gridControl.DisplayLayout.DefaultSelectedForeColor = CurrentTheme.GridHeaderTextColor;
                        break;
                    case Button button:
                        if (CurrentTheme.ButtonUsesOsTheme)
                        {
                            button.FlatStyle = FlatStyle.Standard;
                            button.UseVisualStyleBackColor = true;
                        }
                        else
                        {
                            button.FlatStyle = FlatStyle.Flat;
                            button.UseVisualStyleBackColor = false;
                        }
                        break;
                    case ToolStrip toolStrip:
                        toolStrip.BackColor = CurrentTheme.ControlsBackColor;
                        break;
                    case Splitter splitterControl:
                        splitterControl.BackColor = CurrentTheme.SplitterColor;
                        break;
                    case Label labelControl:
                        if (labelControl.Font.Underline)
                        {
                            labelControl.ForeColor = CurrentTheme.LinkTextColor;
                        }
                        break;
                    case TextBox textBoxControl:
                        if (!textBoxControl.ReadOnly)
                        {
                            textBoxControl.BackColor = CurrentTheme.TextBoxBackColor;
                            textBoxControl.ForeColor = CurrentTheme.TextBoxTextColor;
                        }
                        break;
                    case UltraComboEditor ultraComboControl:
                        ultraComboControl.BackColor = CurrentTheme.TextBoxBackColor;
                        ultraComboControl.ForeColor = CurrentTheme.TextBoxTextColor;
                        break;
                    case ComboBox comboBoxControl:
                        comboBoxControl.FlatStyle = FlatStyle.Flat;
                        comboBoxControl.BackColor = CurrentTheme.TextBoxBackColor;
                        comboBoxControl.ForeColor = CurrentTheme.TextBoxTextColor;
                        break;
                    case SplitContainer splitterContainer:
                        splitterContainer.BackColor = CurrentTheme.SplitterColor;
                        UseThemeRecursive(splitterContainer.Controls.OfType<Control>());
                        break;
                    case UltraPanel ultraPanelControl:
                        ultraPanelControl.UseOsThemes = CurrentTheme.GridUsesOsTheme
                            ? DefaultableBoolean.True
                            : DefaultableBoolean.False;
                        ultraPanelControl.ScrollBarLook.Appearance.BackColor = CurrentTheme.ControlsBackColor;
                        ultraPanelControl.ScrollBarLook.Appearance.ForeColor = CurrentTheme.ControlsTextColor;
                        UseThemeRecursive(ultraPanelControl.ClientArea.Controls.OfType<Control>());
                        break;
                    case Panel or GroupBox or UserControl:
                        UseThemeRecursive(control.Controls.OfType<Control>());
                        break;
                }
            }
        }
    }
}
