using DG.Tweening;

namespace Service
{
    public class ScaleComponent : ComponentBase
    {
        public override void Disappear()
        {
            transform.DOScale(0, disappearDuration).SetEase(ease);
        }

        public override void Appear()
        {
            transform.DOScale(targetValue, appearDuration).SetEase(ease);
        }
    }
}