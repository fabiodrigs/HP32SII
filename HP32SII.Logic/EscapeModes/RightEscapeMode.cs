namespace HP32SII.Logic.EscapeModes
{
    public class RightEscapeMode : EscapeMode
    {
        public RightEscapeMode()
        {
            TopStatus = "        =>";
        }

        public override EscapeMode HandleLeftArrow()
        {
            return new LeftEscapeMode();
        }

        public override EscapeMode HandleRightArrow()
        {
            return new NoEscapeMode();
        }
    }
}
