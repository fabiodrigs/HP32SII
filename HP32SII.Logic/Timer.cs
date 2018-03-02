using AdvancedTimer.Forms.Plugin.Abstractions;
using System;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class Timer
    {
        private const int DisplayLetterIntervalInMs = 200;
        private const int InactivityIntervalInMs = 10 * 60 * 1000;

        private IAdvancedTimer timer = DependencyService.Get<IAdvancedTimer>();

        public Timer(Action TimerElapsed)
        {
            timer.initTimer(InactivityIntervalInMs, (sender, e) => TimerElapsed(), false);
            timer.startTimer();
        }

        public void StartWithInactivityInterval()
        {
            timer.stopTimer();
            timer.setInterval(InactivityIntervalInMs);
            timer.startTimer();
        }

        public void StartWithDisplayLetterInterval()
        {
            timer.stopTimer();
            timer.setInterval(DisplayLetterIntervalInMs);
            timer.startTimer();
        }

        public void Stop()
        {
            timer.stopTimer();
        }
    }
}
