using System;

namespace HP32SII.Logic
{
    class Button
    {
        public Func<string, KeyboardState> DefaultAction;
        public Func<string, KeyboardState> LeftAction;
        public Func<string, KeyboardState> RightAction;

        public Button(Func<string, KeyboardState> defaultAction, Func<string, KeyboardState> leftAction, Func<string, KeyboardState> rightAction)
        {
            DefaultAction = defaultAction;
            LeftAction = leftAction;
            RightAction = rightAction;
        }
    }
}
