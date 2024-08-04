using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;
using System.Windows.Forms;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public enum ThumbnailKind
    {
        Image,
        Animation
    }

    public class ThumbnailModel
    {
        public ThumbnailModel(Image image)
        {
            Kind = ThumbnailKind.Image;
            Image = image;
        }

        public ThumbnailModel(string animationKeys)
        {
            Kind = ThumbnailKind.Animation;
            AnimationKeys = animationKeys;
            Image = BitmapRepository.Instance.BlankImage;
        }

        public ThumbnailKind Kind { get; }

        public string? AnimationKeys { get; }

        public Image Image { get; }

        public AnimationRequirementToken LoadAnimationAsync(Action<Image> afterLoad)
        {
            if (AnimationKeys == null)
                throw new RuntimeException("The Thumbnail is not an animation");
            return AnimationsAsyncLoader.Instance.LoadAnimation(AnimationKeys, afterLoad);
        }

        public Image? LoadAnimation()
        {
            if (AnimationKeys == null)
                throw new RuntimeException("The Thumbnail is not an animation");
            var keys = AnimationKeys.Split(",").Distinct();
            try
            {
                return CCGameRepository.Instance.GetAnimationsImage(string.Join(",", keys));
            }
            catch (Exception)
            {
                // GDI+ error, async generation still running
                return null;
            }
        }
    }
}
