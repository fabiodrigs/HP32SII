using AdvancedTimer.Forms.Plugin.Abstractions;
using System;

namespace HP32SII.Logic
{
    public abstract class State
    {
        protected static Calculator calculator = new Calculator();
        protected static Output output = new Output();
        protected static bool pushAtNextAppend = false;

        public static string Display { get; protected set; } = "";
        public static string BottomStatus { get; protected set; } = "";

        public static bool IsDisplayVisible { get; protected set; } = true;
        public static bool IsTopStatusVisible { get; protected set; } = true;
        public static bool IsBottomStatusVisible { get; protected set; } = true;
        
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

        protected static void TurnScreenOff()
        {
            IsDisplayVisible = false;
            IsTopStatusVisible = false;
            IsBottomStatusVisible = false;
        }

        protected static void TurnScreenOn()
        {
            IsDisplayVisible = true;
            IsTopStatusVisible = true;
            IsBottomStatusVisible = true;
        }

        public State()
        {
            if (timer == null)
                throw new InvalidOperationException("timer not initialized");
            if (buttons == null)
                throw new InvalidOperationException("buttons not initialized");
        }

        public abstract State HandleButton(Button button, EscapeMode escapeMode);

        public abstract State TimerElapsed();
    }
}
