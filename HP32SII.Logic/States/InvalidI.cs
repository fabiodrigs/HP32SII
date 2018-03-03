using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    class InvalidI : State
    {
        public InvalidI() : base()
        {
            BottomStatus = "";
            Display = "INVALID  (i)";
        }
            
        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button == Buttons.Clear)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else
            {
                return new DefaultState().HandleButton(button, escapeMode);
            }
        }

        public override State TimerElapsed()
        {
            return new OffState();
        }
    }
}
