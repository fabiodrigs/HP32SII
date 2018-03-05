namespace HP32SII.Logic.EscapeModes
{
    public abstract class EscapeMode
    {
        public static string TopStatus { get; protected set; } = "";

        public abstract EscapeMode HandleLeftArrow();
        public abstract EscapeMode HandleRightArrow();
        public abstract bool IsEscaped();
    }
}
