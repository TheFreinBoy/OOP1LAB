using System;
using System.Timers;

namespace Time
{
    public class Timer
    {
        private System.Timers.Timer _timer;

        public delegate void TimerCallback();
        public event TimerCallback? OnTick;

        public Timer(double intervalInSeconds)
        {
            _timer = new System.Timers.Timer(intervalInSeconds * 1000);
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            OnTick?.Invoke();     
        }

        public void Start() 
        {
            _timer.Start();
        }

        public void Stop() 
        {
            _timer.Stop();
        }

    }
}