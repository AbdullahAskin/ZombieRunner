using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    [RequireComponent(typeof(Graphic))]
    public class FadeExtension : ExtensionBase
    {
        private Graphic _sourceGraphic;
        private Graphic SourceGraphic => _sourceGraphic ? _sourceGraphic : _sourceGraphic = GetComponent<Graphic>();

        public override void DisappearExtension()
        {
            SourceGraphic.DOFade(0, duration).SetEase(ease);
        }

        public override void AppearExtension()
        {
            SourceGraphic.DOFade(targetValue, duration).SetEase(ease);
        }
    }
}