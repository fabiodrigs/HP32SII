using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
{
    class RecallSubtract : State
    {
        public RecallSubtract() : base()
        {
            Display = $"RCL -  _";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"RCL -  {button.Letter}";
                var storedValue = calculator.Recall(button.Letter);
                output.FromDouble(output.ToDouble() - storedValue);
                return new WaitForDefault();
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
    }
}
