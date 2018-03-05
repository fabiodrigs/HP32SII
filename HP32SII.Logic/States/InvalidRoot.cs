using HP32SII.Logic.EscapeModes;

namespace HP32SII.Logic.States
{
    class InvalidRoot : State
    {
        public InvalidRoot() : base()
        {
            BottomStatus = "";
            Display = "INVALID y^x";
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
    }
}
