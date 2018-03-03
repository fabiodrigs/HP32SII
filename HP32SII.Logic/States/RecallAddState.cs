using System;

namespace HP32SII.Logic
{
    class RecallAddState : State
    {
        public RecallAddState() : base()
        {
            Display = $"RCL +  _";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"RCL +  {button.Letter}";
                var storedValue = calculator.Recall(button.Letter);
                output.FromDouble(output.ToDouble() + storedValue);
                return new WaitForDefaultState();
            }
            else if (button == Buttons.Clear)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else if (button == Buttons.Back)
            {
                return new RecallState();
            }
            else if (button == Buttons.Solve)
            {
                Display = $"RCL - (i)";
                return new WaitForInvalidI();
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
