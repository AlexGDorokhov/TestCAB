using System;
using System.Collections.Generic;
using Controllers;
using Events;
using Utils;

namespace Services
{
    public static class ControllersService
    {

        private static IDictionary<Type, IBaseController> _controllers = new Dictionary<Type, IBaseController>();
        private static IDictionary<Type, IBaseController> _registeredForUpdates = new Dictionary<Type, IBaseController>();
        private static IDictionary<Type, IBaseController> _registeredForFixedUpdate = new Dictionary<Type, IBaseController>();

        public static T Bind<T>(bool registerForUpdate = false, bool registerForFixedUpdate = false) where T : IBaseController
        {
            if (!_controllers.ContainsKey(typeof(T)))
            {
                Log.Message("ControllersService: Created and bound new controller " + typeof(T).ToString());
                var newController = Activator.CreateInstance<T>();
                var controllerType = typeof(T);
                _controllers.Add(controllerType, newController);
                if (registerForUpdate)
                {
                    _registeredForUpdates.Add(controllerType, newController);
                }
                if (registerForFixedUpdate)
                {
                    _registeredForFixedUpdate.Add(controllerType, newController);
                }
                newController.Init();
                newController.AddEventsHandlers();
                return newController;
            }
            else
            {
                Log.Error("ControllersService: Controller can't be bound to ControllersService more thane one time", true);
            }

            return default(T);
        }

        public static void Unbind<T>() where T : IBaseController
        {
            var type = typeof(T);
            if (!_controllers.ContainsKey(type))
            {
                Log.Error("ControllersService: Controller of " + type + " not found for unbind", true);
                return;
            }

            try
            {
                var controller = (IBaseController)_controllers[type];
                controller.RemoveEventsHandlers();
                _controllers[type] = null;
                _controllers.Remove(type);

                if (_registeredForUpdates.ContainsKey(type))
                {
                    controller = (IBaseController)_registeredForUpdates[type];
                    controller.RemoveEventsHandlers();
                    _registeredForUpdates[type] = null;
                    _registeredForUpdates.Remove(type);
                }

                if (_registeredForFixedUpdate.ContainsKey(type))
                {
                    controller = (IBaseController)_registeredForFixedUpdate[type];
                    controller.RemoveEventsHandlers();
                    _registeredForFixedUpdate[type] = null;
                    _registeredForFixedUpdate.Remove(type);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message, true);
            }
        }
        
        public static bool IsBound<T>() where T : BaseController
        {
            return _controllers.ContainsKey(typeof(T));
        }
        
        public static T Get<T>(bool withoutWarnings = false) where T : BaseController
        {
            try
            {
                return (T)_controllers[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                if (!withoutWarnings)
                {
                    Log.Warn("ControllersService: The requested controller "  + typeof(T).ToString() + " is not bound");
                }
                return default(T);
            }
        }

        public static void Update()
        {
            var controllersEnumerator = _registeredForUpdates.GetEnumerator();
            while(controllersEnumerator.MoveNext())
            {
                var controller = (IBaseController)controllersEnumerator.Current.Value; 
                controller.Update();
            }
        }

        public static void FixedUpdate()
        {
            var controllersEnumerator = _registeredForUpdates.GetEnumerator();
            while(controllersEnumerator.MoveNext())
            {
                controllersEnumerator.Current.Value.FixedUpdate();
            }
        }

        public static void FireEvent<T>(T firedEvent, bool silentForLogging = false)
        {
            if ((firedEvent as BaseEvent)!.StopPropagation)
            {
                return;
            }

            var firedEventType = firedEvent.GetType(); 

            var controllersEnumerator = _controllers.GetEnumerator();
            while (controllersEnumerator.MoveNext())
            {
                var controller = (BaseController)controllersEnumerator.Current.Value;
                var eventHandlerTypeKeyEnumerator = controller.eventsHandlers.GetEnumerator();
                while (eventHandlerTypeKeyEnumerator.MoveNext())
                {
                    var eventHandlerTypeKey = eventHandlerTypeKeyEnumerator.Current.Key;
                    if (eventHandlerTypeKey == firedEventType)
                    {
                        var action = controller.eventsHandlers[eventHandlerTypeKey];
                        var genericActionType = typeof(Action<>).MakeGenericType(firedEventType);
                        genericActionType.GetMethod("Invoke")?.Invoke(action, new object[] { firedEvent });
                        if (!silentForLogging)
                        {
                            Log.Event(firedEvent, action);
                        }
                    }
                }
            }
        }
    }
}