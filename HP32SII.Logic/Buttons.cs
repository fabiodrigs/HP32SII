using System.Collections.Generic;

namespace HP32SII.Logic
{
    public class Buttons
    {
        // First row
        public Button Sqrt { get; } = new Button("SQRT", "A");
        public Button Exp { get; } = new Button("EXP", "B");
        public Button Ln { get; } = new Button("LN", "C");
        public Button Pow { get; } = new Button( "POW", "D" );
        public Button Invert { get; } = new Button( "1/X", "E" );
        public Button Sum { get; } = new Button( "SUM", "F" );
        // Second row
        public Button Store { get; } = new Button( "STO", "G" );
        public Button Recall { get; } = new Button( "RCL", "H" );
        public Button RollDown { get; } = new Button( "R", "I" );
        public Button Sin { get; } = new Button( "SIN", "J" );
        public Button Cos { get; } = new Button( "COS", "K" );
        public Button Tan { get; } = new Button( "TAN", "L" );
        // Third row
        public Button Enter { get; } = new Button( "ENTER", "M" );
        public Button Swap { get; } = new Button( "SWAP", "N" );
        public Button ChangeSign { get; } = new Button( "+/-", "O" );
        public Button E { get; } = new Button( "E", "P" );
        public Button Back { get; } = new Button( "BACK", null );
        // Fourth row
        public Button Xeq { get;  } = new Button( "XEQ", null );
        public Button Seven { get; } = new Button( "7", "Q" );
        public Button Eight { get; } = new Button( "8", "R" );
        public Button Nine { get; } = new Button( "9", "S" );
        public Button Divide { get; } = new Button( "/", null );
        // Fifth row
        public Button Left { get; } = new Button( "LEFT", null );
        public Button Four { get; } = new Button( "4", "T" );
        public Button Five { get; } = new Button( "5", "U" );
        public Button Six { get; } = new Button( "6", "V" );
        public Button Multiply { get; } = new Button( "*", null );
        // Sixth row
        public Button Right { get; } = new Button( "RIGHT", null );
        public Button One { get; } = new Button( "1", "W" );
        public Button Two { get; } = new Button( "2", "X" );
        public Button Three { get; } = new Button( "3", "Y" );
        public Button Subtract { get; } = new Button( "-", null );
        // Seventh row
        public Button Clear { get; } = new Button( "C", null );
        public Button Zero { get; } = new Button( "0", "Z" );
        public Button Dot { get; } = new Button( ".", "i" );
        public Button Solve { get; } = new Button( "R/S", null );
        public Button Add { get; } = new Button( "+", null );
    }
}
