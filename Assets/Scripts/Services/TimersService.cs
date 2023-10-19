using System;
using ElRaccoone.Timers;
using Utils;

namespace Services
{
    public static class TimersService
    {
        
        private static bool _timersInitialized;

        private static Action _everyOneSecondActions;
        private static Action _everyFiveSecondsActions;
        private static Action _everyTenSecondsActions;
        private static Action _everyMinuteActions;

        public static void ClearAllTimersActions()
        {
            _everyOneSecondActions = null;
            _everyFiveSecondsActions = null;
            _everyTenSecondsActions = null;
            _everyMinuteActions = null;
        }

        public static void InitializeService()
        {
            
            if (!_timersInitialized)
            {
                _timersInitialized = true;
                Timers.SetInterval(1000, () =>
                {
                    try
                    {
                        _everyOneSecondActions?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                });

                Timers.SetInterval(1000 * 5, () =>
                {
                    try
                    {
                        _everyFiveSecondsActions?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                });

                Timers.SetInterval(1000 * 10, () =>
                {
                    try
                    {
                        _everyTenSecondsActions?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                });

                Timers.SetInterval(1000 * 60, () =>
                {
                    try
                    {
                        _everyMinuteActions?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                });
            }
            
        }

        public static void EveryOneSecondActionAdd(Action action)
        {
            Log.Message("EveryOneSecondAction added for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyOneSecondActions -= action;
            _everyOneSecondActions += action;
        }
        public static void EveryOneSecondActionRemove(Action action)
        {
            Log.Message("EveryOneSecondAction removed for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyOneSecondActions -= action;
        }
        
        public static void EveryFiveSecondsActionAdd(Action action)
        {
            Log.Message("EveryFiveSecondsAction added for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyFiveSecondsActions -= action;
            _everyFiveSecondsActions += action;
        }
        public static void EveryFiveSecondsActionRemove(Action action)
        {
            Log.Message("EveryFiveSecondsAction removed for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyFiveSecondsActions -= action;
        }
        
        public static void EveryTenSecondsActionAdd(Action action)
        {
            Log.Message("EveryTenSecondsAction added for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyTenSecondsActions -= action;
            _everyTenSecondsActions += action;
        }
        public static void EveryTenSecondsActionRemove(Action action)
        {
            Log.Message("EveryTenSecondsAction removed for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyTenSecondsActions -= action;
        }
        
        public static void EveryMinuteActionAdd(Action action)
        {
            Log.Message("EveryMinuteAction added for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyMinuteActions -= action;
            _everyMinuteActions += action;
        }
        public static void EveryMinuteActionRemove(Action action)
        {
            Log.Message("EveryMinuteAction removed for Method " + action.Method.Name + " " + new System.Diagnostics.StackTrace());
            _everyMinuteActions -= action;
        }


        
    }
}