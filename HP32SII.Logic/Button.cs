using System;

namespace HP32SII.Logic
{
    public class Button
    {
        public string Name { get; set; }
        public string Letter { get; set; }
        public Func<KeyboardState> DefaultOperation { get; set; }
        public Func<KeyboardState> LeftOperation { get; set; }
        public Func<KeyboardState> RightOperation { get; set; }

        public Button(string name, string letter)
        {
            Name = name;
            Letter = letter;
        }

        public override bool Equals(object obj)
        {
            return obj is Button && this == obj as Button;
        }

        public static bool operator ==(Button x, Button y)
        {
            return x.Name == y.Name && x.Letter == y.Letter;
        }

        public static bool operator !=(Button x, Button y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return new { Name, Letter }.GetHashCode();
        }
    }
}
