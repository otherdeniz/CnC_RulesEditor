namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class SideDefinition
    {
        private Image? _logoImage;

        public string Name { get; set; } = null!;

        public string Logo { get; set; } = null!;

        public Image GetLogoImage()
        {
            return _logoImage ??= LogoRepository.Instance.GetLogo(Logo);
        }
    }
}
