namespace HP32SII.Logic.EscapeModes
{
    public class NoEscapeMode : EscapeMode
    {
        public NoEscapeMode()
        {
            TopStatus = "";
        }

        public override EscapeMode HandleLeftArrow()
        {
            return new LeftEscapeMode();
        }

        public override EscapeMode HandleRightArrow()
        {
            return new RightEscapeMode();
        }

        public override bool IsEscaped()
        {
            return false;
        }
    }
}
