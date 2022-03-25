using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Service
{
    public class MenuBase : MonoBehaviour
    {
        [SerializeField] private List<ExtensionBase> extensionBases;
        [SerializeField] protected GameObject content;
        protected MenuState MenuState = MenuState.Disactivated;
        protected Action PostAppear;
        protected Action PostDisappear;
        protected Action PreAppear;
        protected Action PreDisappear;

        private void Awake()
        {
            PreAppear += () => content.gameObject.SetActive(true);
            PostDisappear += () => content.gameObject.SetActive(false);
        }

        public void Disappear()
        {
            if (MenuState == MenuState.Disactivated) return;
            PreDisappear?.Invoke();
            extensionBases.ForEach(x => x.DisappearExtension());
            var maxDuration = extensionBases.Max(x => x.duration);
            DOVirtual.DelayedCall(maxDuration, () => PostDisappear?.Invoke());
        }

        public void Appear()
        {
            if (MenuState == MenuState.Activated) return;
            PreAppear?.Invoke();
            extensionBases.ForEach(x => x.AppearExtension());
            var maxDuration = extensionBases.Max(x => x.duration);
            DOVirtual.DelayedCall(maxDuration, () => PostAppear?.Invoke());
        }
    }

    public enum MenuState
    {
        Activated,
        Disactivated
    }
}