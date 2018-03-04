using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
{
    public sealed class WaitForDefault : State
    {
        public WaitForDefault() : base()
        {
            Timer.StartWithDisplayLetterInterval();
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            return this;
        }

        public override State TimerElapsed()
        {
            Display = output.ToString();
            Timer.StartWithInactivityInterval();
            return new DefaultState();
        }

        public override bool IsWaiting()
        {
            return true;
        }
    }
}
