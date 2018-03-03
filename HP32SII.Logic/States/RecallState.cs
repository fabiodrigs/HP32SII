using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
{
    public sealed class RecallState : State
    {
        public RecallState() : base()
        {
            Display = "RCL  _";
            BottomStatus = "A..Z";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"RCL  {button.Letter}";
                var storedValue = calculator.Recall(button.Letter);
                output.FromDouble(storedValue);
                return new WaitForDefault();
            }
            else if (button == Buttons.Divide)
            {
                return new RecallDivide();
            }
            else if (button == Buttons.Multiply)
            {
                return new RecallMultiply();
            }
            else if (button == Buttons.Subtract)
            {
                return new RecallSubtract();
            }
            else if (button == Buttons.Add)
            {
                return new RecallAdd();
            }
            else if (button == Buttons.Clear || button == Buttons.Back)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else if (button == Buttons.Solve)
            {
                Display = $"RCL (i)";
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
