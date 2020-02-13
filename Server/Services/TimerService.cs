using System;
using System.Threading;

namespace Server.Services
{
    public class TimerService
    {
        private static object _lock = new object();
        private static Timer _timer;
        private int _timeInterval;
        private Action _callback;
        
        public void Start(int interval, Action callback)
        {
            _timeInterval = interval;
            _callback = callback;
            _timer = new Timer(Callback, null, 0, _timeInterval);
        }
 
        public void Stop()
        {
            _timer.Dispose();
        }
 
        private void Callback(object state)
        {
            var hasLock = false;
 
            try
            {
                Monitor.TryEnter(_lock, ref hasLock);
                if (!hasLock)
                {
                    return;
                }
                
                _callback();
            }
            finally
            {
                if (hasLock)
                {
                    Monitor.Exit(_lock);
                }
            }
        }
    }
}