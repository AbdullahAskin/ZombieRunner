using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class MenuService : ServiceBase
    {
        [SerializeField] private List<MenuBase> menuBases;

        public T GetMenu<T>() where T : MenuBase
        {
            return (T)menuBases.Find(x => x.GetType() == typeof(T));
        }    
       
    }
}