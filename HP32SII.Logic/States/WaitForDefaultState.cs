using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public sealed class WaitForDefaultState : State
    {
        public WaitForDefaultState() : base()
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
    }
}
