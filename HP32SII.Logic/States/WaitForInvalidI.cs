using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
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

        public override bool IsWaiting()
        {
            return true;
        }
    }
}
