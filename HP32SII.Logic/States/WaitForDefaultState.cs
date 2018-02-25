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

        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            throw new NotImplementedException();
        }

        public override State TimerElapsed()
        {
            Display = output.ToString();
            Timer.setInterval(InactivityIntervalInMs);
            Timer.startTimer();
            return new DefaultState();
        }
    }
}
