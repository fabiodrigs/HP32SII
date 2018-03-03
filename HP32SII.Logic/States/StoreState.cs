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
                return new WaitForDefaultState();
            }
            else if (button == Buttons.Divide)
            {
                return new StoreDivideState();
            }
            else if (button == Buttons.Multiply)
            {
                return new StoreMultiplyState();
            }
            else if (button == Buttons.Subtract)
            {
                return new StoreSubtractState();
            }
            else if (button == Buttons.Add)
            {
                return new StoreAddState();
            }
            else if (button == Buttons.Clear || button == Buttons.Back)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else if (button == Buttons.Solve)
            {
                // TODO display "INVALID (i)"
                return this;
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
