using System;
using Scripts;
using UnityEngine;

namespace Views
{
    public abstract class BaseBehaviour : MonoBehaviour
    {
        
        internal Action CloseAction;
        internal Action DestroyAction;

        protected virtual void Start()
        {
            ServiceInjector.ResolveServicesInObject(this.gameObject);
        }

        protected virtual void OnDestroy()
        {
            DestroyAction?.Invoke();
        }
        
    }
}