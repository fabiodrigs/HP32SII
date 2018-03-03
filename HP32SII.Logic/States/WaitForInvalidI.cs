using System;

namespace HP32SII.Logic
{
    class WaitForInvalidI : State
    {
        public WaitForInvalidI() : base()
        {
            Timer.StartWithDisplayLetterInterval();
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            return this;
        }

        public override State TimerElapsed()
        {
            return new InvalidI();
        }
    }
}
