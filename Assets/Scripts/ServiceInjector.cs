using System;
using System.Collections.Generic;
using System.Reflection;
using Services.Attributes;
using UnityEngine;

namespace Scripts
{

    public static class ServiceInjector
    {
        private static Dictionary<object, object> _services = new Dictionary<object, object>();

        public static void Inject(Type injectedType, object instance)
        {
            if (!_services.ContainsKey(injectedType))
            {
                Debug.Log("ServiceInjector: Inject new " + injectedType.ToString());
                _services.Add(injectedType, instance);
            }
            else
            {
                Debug.Log("ServiceInjector: Inject override " + injectedType.ToString());
                _services[injectedType] = instance;
            }
        }


        public static object Get(Type injectedType, bool withoutWarnings = false)
        {
            try
            {
                return _services[injectedType];
            }
            catch (KeyNotFoundException)
            {
                if (!withoutWarnings)
                {
                    Debug.LogWarning("ServiceInjector: The requested service is not registered " + injectedType.ToString());
                }

                return null;
            }
        }

        public static void InjectServicesFromObject(GameObject targetObject)
        {
            Component[] allComponents = targetObject.GetComponents<Component>();

            foreach (Component component in allComponents)
            {
                InjectServicesFromComponent(component);
            }
        }

        public static void InjectServicesFromComponent(Component component)
        {
            Type monoType = component.GetType();
            ServiceInjectAttribute attribute = Attribute.GetCustomAttribute(monoType, typeof(ServiceInjectAttribute)) as ServiceInjectAttribute;
            if (attribute != null)
            {
                Inject(attribute.injectedType, component);
            }

        }

        internal static void ResolveServicesInObject(GameObject targetObject, bool withoutWarnings = false)
        {
            Component[] allComponents = targetObject.GetComponents<Component>();

            foreach (Component component in allComponents)
            {
                ResolveServicesInComponent(component, withoutWarnings);
            }
        }

        internal static void ResolveServicesInComponent(Component component, bool withoutWarnings = false)
        {
            Type monoType = component.GetType();

            FieldInfo[] objectFields = monoType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            for (int i = 0; i < objectFields.Length; i++)
            {
                InjectService attribute = Attribute.GetCustomAttribute(objectFields[i], typeof(InjectService)) as InjectService;
                if (attribute != null)
                {
                    objectFields[i].SetValue(component, Get(objectFields[i].FieldType,withoutWarnings));
                }
            }

        }
    }
}
