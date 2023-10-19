using System;
using System.IO;
using Controllers;
using Defines.Constants;
using Scripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Views
{
    public class BaseMediator<TBehaviour> : BaseController where TBehaviour : BaseBehaviour
    {

        protected TBehaviour Behaviour { get; set; }
        protected GameObject Container { get; set; }
        
        protected BaseMediator()
        {
            var componentName = GetType().Name.Replace("Mediator", "");
            Initialize(componentName, Main.Canvas);
        }

        protected virtual void Initialize(string componentName, GameObject container)
        {
            
            if (container == null)
            {
                throw new ArgumentException($"The Container for UI component with name \"{componentName}\" has not been found");
            }

            Container = container;

            var prefabPath = Path.Combine(DirectoryNames.Prefabs, componentName);
            var component = Resources.Load<GameObject>(prefabPath);

            if (component == null)
            {
                throw new ArgumentException($"The prefab for {componentName} not loaded");
            }
            
            var viewComponent = Object.Instantiate(component, Container.transform);
            
            Behaviour = viewComponent.GetComponent<TBehaviour>();

            if (Behaviour == null)
            {
                Object.Destroy(viewComponent);
                throw new ArgumentException($"The Behaviour with type \"{typeof(TBehaviour).FullName}\" has not been found in the component with name \"{componentName}\"");
            }

            OnRegister();
            
        }

        protected virtual void OnRegister()
        {
            Behaviour.CloseAction = OnRemove;
        }

        protected virtual void OnRemove()
        {

            Behaviour.CloseAction = null;

            Behaviour.StopAllCoroutines();
            
            Object.Destroy(Behaviour);

            Behaviour = null;
            Container = null;

        }

        public virtual void Hide()
        {
            Behaviour.gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            Behaviour.gameObject.SetActive(true);
        }

    }
}