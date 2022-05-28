using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    [RequireComponent(typeof(Graphic))]
    public class FadeComponent : ComponentBase
    {
        private Graphic _sourceGraphic;
        private Graphic SourceGraphic => _sourceGraphic ? _sourceGraphic : _sourceGraphic = GetComponent<Graphic>();

        public override void Disappear()
        {
            SourceGraphic.DOFade(0, disappearDuration).SetEase(ease);
        }

        public override void Appear()
        {
            SourceGraphic.DOFade(targetValue, appearDuration).SetEase(ease);
        }
    }
}