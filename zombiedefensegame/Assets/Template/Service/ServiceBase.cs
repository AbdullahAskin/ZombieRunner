using System;
using UnityEngine;

namespace Service
{
    public class ServiceBase : MonoBehaviour
    {
        private void Awake()=> ServiceManager.AddService(this);
        private void OnDestroy()=> ServiceManager.RemoveService(this);
    }
}