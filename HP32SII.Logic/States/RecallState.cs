using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public sealed class RecallState : State
    {
        public RecallState() : base()
        {

        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"RCL  {button.Letter}";
                var recalled = calculator.Recall(button.Letter);
                output.FromDouble(recalled);
                Timer.StartWithDisplayLetterInterval();
                return new WaitForDefaultState();
            }
            else if (button == Buttons.Clear || button == Buttons.Back)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else
            {
                return this;
            }
        }

        public override State TimerElapsed()
        {
            return new OffState();
        }
    }
}
