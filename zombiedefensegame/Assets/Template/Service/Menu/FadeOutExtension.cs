using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    [RequireComponent(typeof(Image))]
    public class FadeOutExtension : ExtensionBase
    {
        private Image _sourceImage;
        private Image sourceImage => _sourceImage ? _sourceImage : _sourceImage = GetComponent<Image>();

        public override void DisappearExtension()
        {
            sourceImage.DOFade(0, duration).SetEase(ease);
        }

        public override void AppearExtension()
        {
            sourceImage.DOFade(endValue, duration).SetEase(ease);
        }
    }
}