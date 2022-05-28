using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Service
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuBase : MonoBehaviour
    {
        [SerializeField] private List<ComponentBase> extensionBases;
        [SerializeField] protected GameObject content;
        private CanvasGroup _canvasGroup;
        protected MenuState MenuState = MenuState.DisActivated;
        protected Action PostAppear;
        protected Action PostDisappear;
        protected Action PreAppear;
        protected Action PreDisappear;
        private CanvasGroup CanvasGroup => _canvasGroup ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();

        private void Awake()
        {
            PreAppear += () => content.gameObject.SetActive(true);
            PostDisappear += () => content.gameObject.SetActive(false);
        }

        public virtual void Disappear()
        {
            if (MenuState == MenuState.DisActivated) return;
            MenuState = MenuState.DisActivated;
            CanvasGroup.blocksRaycasts = false;
            PreDisappear?.Invoke();
            extensionBases.ForEach(x => x.Disappear());
            var maxDuration = extensionBases.Max(x => x.disappearDuration);
            DOVirtual.DelayedCall(maxDuration, () => PostDisappear?.Invoke());
        }

        public virtual void Appear()
        {
            if (MenuState == MenuState.Activated) return;
            MenuState = MenuState.Activated;
            CanvasGroup.blocksRaycasts = true;
            PreAppear?.Invoke();

            if (extensionBases.Count == 0) return;
            extensionBases.ForEach(x => x.Appear());
            var maxDuration = extensionBases.Max(x => x.disappearDuration);
            DOVirtual.DelayedCall(maxDuration, () => PostAppear?.Invoke());
        }
    }

    public enum MenuState
    {
        Activated,
        DisActivated
    }
}