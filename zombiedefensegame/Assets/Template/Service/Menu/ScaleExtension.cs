using DG.Tweening;

namespace Service
{
    public class ScaleExtension : ExtensionBase
    {
        public override void DisappearExtension()
        {
            transform.DOScale(0, duration).SetEase(ease);
        }

        public override void AppearExtension()
        {
            transform.DOScale(endValue, duration).SetEase(ease);
        }
    }
}