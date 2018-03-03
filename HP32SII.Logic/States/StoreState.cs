using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
{
    public sealed class StoreState : State
    {
        public StoreState() : base()
        {
            Display = "STO  _";
            BottomStatus = "A..Z";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"STO  {button.Letter}";
                calculator.Store(button.Letter, output.ToDouble());
                return new WaitForDefault();
            }
            else if (button == Buttons.Divide)
            {
                return new StoreDivide();
            }
            else if (button == Buttons.Multiply)
            {
                return new StoreMultiply();
            }
            else if (button == Buttons.Subtract)
            {
                return new StoreSubtract();
            }
            else if (button == Buttons.Add)
            {
                return new StoreAdd();
            }
            else if (button == Buttons.Clear || button == Buttons.Back)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else if (button == Buttons.Solve)
            {
                Display = $"STO (i)";
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
