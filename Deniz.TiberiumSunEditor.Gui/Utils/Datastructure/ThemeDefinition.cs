namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class ThemeDefinition
    {
        public string Name { get; set; } = string.Empty;

        public Color ControlsBackColor { get; set; } = SystemColors.Control;
        public Color ControlsTextColor { get; set; } = Color.Black;
        public Color TextBoxBackColor { get; set; } = Color.White;
        public Color TextBoxTextColor { get; set; } = Color.Black;
        public Color PlainBackColor { get; set; } = Color.White;
        public Color SplitterColor { get; set; } = Color.FromArgb(180, 180, 180);
        public Color LinkTextColor { get; set; } = Color.FromArgb(0, 0, 255);
        public Color ModifiedTextColor { get; set; } = Color.FromArgb(255, 0, 0);
        public Color HintTextColor { get; set; } = Color.FromArgb(128, 128, 128);
        public Color HotTrackBackColor { get; set; } = Color.FromArgb(178,214,242);
        public Color HotTrackBorderColor { get; set; } = Color.FromArgb(0,120,215);
        public Color HotTrackTextColor { get; set; } = Color.Black;
        public Color GridHeaderBackColor { get; set; } = SystemColors.Control;
        public Color GridHeaderTextColor { get; set; } = Color.Black;
        public Color GridReadonlyCellBackColor { get; set; } = Color.FromArgb(230, 230, 230);
        public Color GridEditableCellBackColor { get; set; } = Color.White;
        public Color GridModifiedCellBackColor { get; set; } = Color.FromArgb(255, 222, 173);
        public bool GridUsesOsTheme { get; set; } = true;
        public bool TabsUsesOsTheme { get; set; } = true;
        public bool ButtonUsesOsTheme { get; set; } = true;
        public bool WindowUseDarkHeader { get; set; } = false;
        public string BlankTechnoBitmap { get; set; } = "_BLANK.bmp";
    }
}
