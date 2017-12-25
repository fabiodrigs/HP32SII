using System;

namespace HP32SII.Logic
{
    class Button
    {
        public Action<string> DefaultAction;
        public Action<string> LeftAction;
        public Action<string> RightAction;

        public Button(Action<string> defaultAction, Action<string> leftAction, Action<string> rightAction)
        {
            DefaultAction = defaultAction;
            LeftAction = leftAction;
            RightAction = rightAction;
        }
    }
}
