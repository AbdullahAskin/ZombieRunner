using System;
using DG.Tweening;
using UnityEngine;

namespace Service
{
    public abstract class ExtensionBase : MonoBehaviour
    {
        public float appearDuration;
        public float disappearDuration;
        [SerializeField] protected float targetValue;
        [SerializeField] protected Ease ease = Ease.OutQuad;

        public abstract void Disappear();

        public abstract void Appear();

    }
}