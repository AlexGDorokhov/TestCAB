using System;
using System.Collections.Generic;
using Services;
using Utils;

namespace Controllers
{
    public abstract class BaseController : IBaseController
    {

        public Dictionary<Type, object> eventsHandlers = new Dictionary<Type, object>();


        public virtual void Init()
        {
            TimersService.InitializeService();
        }
        
        public virtual void Update()
        {
            //
        }
        
        public virtual void FixedUpdate()
        {
            //
        }
        
        public virtual void AddEventsHandlers()
        {
            //
        }
        
        public virtual void RemoveEventsHandlers()
        {
            //
        }
        
        protected void AddListener<T>(Action<T> callback)
        {
            if (callback == null)
            {
                Log.Error("BaseController::AddListener - Failed to add listener because the given callback is null");
                return;
            }

            var type = typeof(T);
            if (!eventsHandlers.ContainsKey(type))
            {
                eventsHandlers.Add(type, callback);
                return;
            }
            
            Log.Error("BaseController::AddEventHandler - EventHandler of " + typeof(T) + " can't be added for " + this.GetType() + " more thane one time", true);
        }

        protected void RemoveListener<T>()
        {
            var type = typeof(T);
            if (!eventsHandlers.ContainsKey(type))
            {
                Log.Error("BaseController::RemoveListener - EventHandler of " + typeof(T) + " not found for " + this.GetType(), true);
                return;
            }

            eventsHandlers[type] = null;
            eventsHandlers.Remove(type);
        }

        
    }
}