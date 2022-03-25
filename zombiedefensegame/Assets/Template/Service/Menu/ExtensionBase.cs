using System;
using DG.Tweening;
using UnityEngine;

namespace Service
{
    public abstract class ExtensionBase : MonoBehaviour
    {
        public float duration;
        [SerializeField] protected float targetValue;
        [SerializeField] protected Ease ease = Ease.OutQuad;

        public abstract void DisappearExtension();

        public abstract void AppearExtension();
    }
}