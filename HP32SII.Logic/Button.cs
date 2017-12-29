using System;

namespace HP32SII.Logic
{
    class Button
    {
        public Func<string, State> DefaultAction;
        public Func<string, State> LeftAction;
        public Func<string, State> RightAction;

        public Button(Func<string, State> defaultAction, Func<string, State> leftAction, Func<string, State> rightAction)
        {
            DefaultAction = defaultAction;
            LeftAction = leftAction;
            RightAction = rightAction;
        }
    }
}
