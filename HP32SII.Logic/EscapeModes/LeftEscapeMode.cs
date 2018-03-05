namespace HP32SII.Logic.EscapeModes
{
    public class LeftEscapeMode : EscapeMode
    {
        public LeftEscapeMode()
        {
            TopStatus = "  <=";
        }

        public override EscapeMode HandleLeftArrow()
        {
            return new NoEscapeMode();
        }

        public override EscapeMode HandleRightArrow()
        {
            return new RightEscapeMode();
        }

        public override bool IsEscaped()
        {
            return true;
        }
    }
}
