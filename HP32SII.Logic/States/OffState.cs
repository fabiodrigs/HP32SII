using HP32SII.Logic.EscapeModes;
using System;

namespace HP32SII.Logic.States
{
    public sealed class OffState : State
    {
        public OffState() : base()
        {

        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button == Buttons.Clear)
            {
                pushAtNextAppend = false;
                TurnScreenOn();
                Timer.StartWithInactivityInterval();
                return new DefaultState();
            }
            else
            {
                return this;
            }
        }

        public override State TimerElapsed()
        {
            return this;
        }
    }
}
