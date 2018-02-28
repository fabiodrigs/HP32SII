using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public sealed class StoreState : State
    {
        public StoreState() : base()
        {

        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"STO  {button.Letter}";
                calculator.Store(button.Letter, output.ToDouble());
                Timer.StartWithDisplayLetterInterval();
                return new WaitForDefaultState();
            }
            else if (button == Buttons.Solve)
            {
                // TODO display "INVALID (i)"
                return this;
            }
            else if (button == Buttons.Divide)
            {
                Display = $"STO /  _";
                return this;
            }
            else if (button == Buttons.Multiply)
            {
                Display = $"STO *  _";
                return this;
            }
            else if (button == Buttons.Subtract)
            {
                Display = $"STO -  _";
                return this;
            }
            else if (button == Buttons.Add)
            {
                Display = $"STO +  _";
                return this;
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
