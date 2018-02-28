using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private EscapeMode escapeMode = EscapeMode.None;
        private Timer timer;
        private State state;

        public ICommand LeftArrowCommand { get; private set; }
        public ICommand RightArrowCommand { get; private set; }
        public ICommand ButtonCommand { get; private set; }

        #region Properties
        public Buttons Buttons { get; } = new Buttons();

        private string topStatus = "";
        public string TopStatus
        {
            get { return topStatus; }
            private set { Set(ref topStatus, value); }
        }

        private bool isTopStatusVisible = true;
        public bool IsTopStatusVisible
        {
            get { return isTopStatusVisible; }
            private set { Set(ref isTopStatusVisible, value); }
        }

        private string bottomStatus = "";
        public string BottomStatus
        {
            get { return bottomStatus; }
            private set { Set(ref bottomStatus, value); }
        }

        private bool isBottomStatusVisible = true;
        public bool IsBottomStatusVisible
        {
            get { return isBottomStatusVisible; }
            private set { Set(ref isBottomStatusVisible, value); }
        }

        private string display = " 0";
        public string Display
        {
            get { return display; }
            private set { Set(ref display, value); }
        }

        private bool isDisplayVisible = true;
        public bool IsDisplayVisible
        {
            get { return isDisplayVisible; }
            private set { Set(ref isDisplayVisible, value); }
        }
        #endregion
        #region Constructor
        public MainPageViewModel()
        {
            LeftArrowCommand = new Command(HandleLeftArrow, IsStateOn);
            RightArrowCommand = new Command(HandleRightArrow, IsStateOn);
            ButtonCommand = new Command<Button>(HandleButton, IsButtonEnabled);

            timer = new Timer(TimerElapsed);

            State.Timer = timer;
            State.Buttons = Buttons;
            DefaultState.AssignButtonOperations();

            state = new DefaultState();
        }
        #endregion

        private void HandleLeftArrow()
        {
            if (!(state is WaitForDefaultState))
            {
                timer.StartWithInactivityInterval();
            }
            escapeMode = escapeMode == EscapeMode.Left ? ClearEscapeMode() : GoToLeft();
        }

        private void HandleRightArrow()
        {
            if (!(state is WaitForDefaultState))
            {
                timer.StartWithInactivityInterval();
            }
            escapeMode = escapeMode == EscapeMode.Right ? ClearEscapeMode() : GoToRight();
        }

        private void HandleButton(Button button)
        {
            if (!(state is WaitForDefaultState))
            {
                timer.StartWithInactivityInterval();
            }

            state = state.HandleButton(button, escapeMode);

            UpdateDisplay();

            escapeMode = ClearEscapeMode();
        }

        private void TimerElapsed()
        {
            state = state.TimerElapsed();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            Display = State.Display;
            TopStatus = State.TopStatus;
            BottomStatus = State.BottomStatus;
            IsDisplayVisible = State.IsDisplayVisible;
            IsTopStatusVisible = State.IsTopStatusVisible;
            IsBottomStatusVisible = State.IsBottomStatusVisible;
        }

        private EscapeMode GoToLeft()
        {
            TopStatus = "  <=";
            return EscapeMode.Left;
        }

        private EscapeMode ClearEscapeMode()
        {
            return EscapeMode.None;
        }

        private KeyboardState GoToDefault()
        {
            TopStatus = "";
            BottomStatus = "";
            return KeyboardState.Default;
        }

        private EscapeMode GoToRight()
        {
            TopStatus = "        =>";
            return EscapeMode.Right;
        }

        private bool IsButtonEnabled(Button button)
        {
            if (button == Buttons.Clear)
                return true;
            else
                return IsStateOn();
        }

        private bool IsStateOn()
        {
            return !(state is OffState);
        }
    }
}
