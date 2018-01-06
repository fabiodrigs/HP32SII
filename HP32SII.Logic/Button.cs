using System;

namespace HP32SII.Logic
{
    public class Button
    {
        public string Name { get; set; }
        public string Letter { get; set; }

        public Button(string name, string letter)
        {
            Name = name;
            Letter = letter;
        }
    }
}
