namespace HP32SII.Logic
{
    class DivideByZero : State
    {
        public DivideByZero() : base()
        {
            BottomStatus = "";
            Display = "DIVIDE BY 0";
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
