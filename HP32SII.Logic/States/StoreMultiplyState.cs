using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    class StoreMultiplyState : State
    {
        public StoreMultiplyState() : base()
        {
            Display = $"STO *  _";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"STO *  {button.Letter}";
                var storedValue = calculator.Recall(button.Letter);
                calculator.Store(button.Letter, storedValue * output.ToDouble());
                return new WaitForDefaultState();
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
