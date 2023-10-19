using System;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    public static class Log
    {
        
        public static bool LogEnabledMessage = true;
        public static bool LogsEnabledWarn = true;
        public static bool LogsEnabledError = true;
        public static bool LogsEnabledEvent = true;

        public static void SetAllLogsEnabled()
        {
            LogEnabledMessage = true;
            LogsEnabledWarn = true;
            LogsEnabledError = true;
            LogsEnabledEvent = true;
        }
        
        public static void SetAllLogsDisabled()
        {
            LogEnabledMessage = false;
            LogsEnabledWarn = false;
            LogsEnabledError = false;
            LogsEnabledEvent = false;
        }
        
        public static void Message(object message)
        {
            if (LogEnabledMessage)
            {
                Debug.Log(message);
            }
        }
        
        public static void Warn(object message)
        {
            if (LogsEnabledWarn)
            {
                Debug.LogWarning(message);
            }
        }
        
        public static void Error(object message, bool throwException = false)
        {
            if (LogsEnabledError)
            {
                Debug.LogError(message);
            }

            if (throwException)
            {
                throw new Exception();
            }
        }
        
        public static void Null(object message = null)
        {
        }

        public static void NotImplemented()
        {
            Warn("Not implemented");
        }

        public static void Event(object firedEvent, object action, Exception e = null)
        {
            if (LogsEnabledEvent || e != null)
            {

                //var firedEventFields = Utils.GetFieldsDictionaryFromObject(firedEvent);
                var firedActionMethod = RuntimeReflectionExtensions.GetMethodInfo(action as Delegate);
                var firedActionFunctionName = firedActionMethod.Name;
                var firedActionFunctionClassName = firedActionMethod.DeclaringType.Name;
                //var firedActionFunctionClassFullName = firedActionMethod.DeclaringType.FullName;

                if (e == null)
                {
                    Debug.Log(
                        "Fired event " + firedEvent.GetType().Name + " for " + firedActionFunctionClassName + "::" +
                        firedActionFunctionName 
                    );
                }
                else
                {
                    Debug.LogError(
                        "Fired event " + firedEvent.GetType().Name + " for " + firedActionFunctionClassName + "::" + 
                        firedActionFunctionName + " | Error message: " + e.Message
                    );
                }
            }

        }
        
    }
}