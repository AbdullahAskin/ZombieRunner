using DG.Tweening;
using UnityEngine;

namespace Service
{
    public abstract class ExtensionBase : MonoBehaviour
    {
        [SerializeField] protected float endValue;
        [SerializeField] protected float duration;
        [SerializeField] protected Ease ease = Ease.OutQuad;
        
        public abstract void DisappearExtension();

        public abstract void AppearExtension();
    }
}