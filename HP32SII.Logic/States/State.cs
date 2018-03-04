using HP32SII.Logic.EscapeModes;
using System;

namespace HP32SII.Logic.States
{
    public abstract class State
    {
        protected static Calculator calculator = new Calculator();
        protected static Output output = new Output();
        protected static bool pushAtNextAppend = false;

        public static string Display { get; protected set; } = "0";
        public static string BottomStatus { get; protected set; } = "";

        private static Timer timer = null;
        public static Timer Timer
        {
            get => timer;
            set => timer = timer == null ? value : throw new InvalidOperationException("Timer has already been assigned");
        }

        private static Buttons buttons = null;
        public static Buttons Buttons
        {
            get => buttons;
            set => buttons = buttons == null ? value : throw new InvalidOperationException("Buttons has already been assigned");
        }

        public State()
        {
            if (timer == null)
                throw new InvalidOperationException("Timer not initialized");
            if (buttons == null)
                throw new InvalidOperationException("Buttons not initialized");
        }

        public abstract State HandleButton(Button button, EscapeMode escapeMode);
    
        public virtual State TimerElapsed()
        {
            throw new InvalidOperationException("Unexpected timer elapsed");
        }

        public virtual bool IsWaiting()
        {
            return false;
        }
    }
}
