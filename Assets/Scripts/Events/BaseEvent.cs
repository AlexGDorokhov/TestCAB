using System.Collections.Generic;
using System;
using Services;
using Utils;

namespace Events
{
    public class BaseEvent
    {
        private Dictionary<Type, object> _customFields;

        public void Fire(bool silentForLogging = false)
        {
            ControllersService.FireEvent(this, silentForLogging);
        }

        public BaseEvent AddCustomField<T>(Type field, T fieldValue)
        {
            _customFields.Add(field, fieldValue);
            return this;
        }
        
        public Dictionary<Type, object> GetCustomFields<T>(Type field, T fieldValue)
        {
            return _customFields;
        }

        private bool _stopPropagation = false;
        public bool StopPropagation
        {
            get => _stopPropagation;
            set 
            {
                if (value)
                {
                    Log.Message("Propagation canceled for " + this.GetType().ToString());
                }
                else if (_stopPropagation && !value) 
                {
                    Log.Message("Propagation restored for " + this.GetType().ToString());
                }
                _stopPropagation = value;
            }
        }

    }
}