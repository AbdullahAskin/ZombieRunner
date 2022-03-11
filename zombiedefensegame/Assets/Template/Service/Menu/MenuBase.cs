using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class MenuBase : MonoBehaviour
    {
        [SerializeField] private List<ExtensionBase> extensionBases;
        protected MenuState menuState = MenuState.Disactivated;
        public void Disappear()
        {
            if (menuState == MenuState.Disactivated) return;
            extensionBases.ForEach(x=>x.DisappearExtension());    
        }
        
        public void Appear()
        {
            if (menuState == MenuState.Activated) return;
            extensionBases.ForEach(x=>x.AppearExtension());
        }
        
        //TODO Post and predisappear
    }

    public enum MenuState
    {
        Activated,
        Disactivated
    }
}