using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using HP32SII.Logic.EscapeModes;
using HP32SII.Logic.States;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private Timer timer;

        private EscapeMode escapeMode;
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

            escapeMode = new NoEscapeMode();
            state = new DefaultState();
        }
        #endregion

        private void HandleLeftArrow()
        {
            if (!(state is WaitForDefault))
            {
                timer.StartWithInactivityInterval();
            }
            escapeMode = escapeMode.HandleLeftArrow();
            TopStatus = EscapeMode.TopStatus;
        }

        private void HandleRightArrow()
        {
            if (!(state is WaitForDefault))
            {
                timer.StartWithInactivityInterval();
            }
            escapeMode = escapeMode.HandleRightArrow();
            TopStatus = EscapeMode.TopStatus;
        }

        private void HandleButton(Button button)
        {
            if (!(state is WaitForDefault))
            {
                timer.StartWithInactivityInterval();
            }

            state = state.HandleButton(button, escapeMode);
            escapeMode = new NoEscapeMode();

            UpdateDisplay();

        }

        private void TimerElapsed()
        {
            state = state.TimerElapsed();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TopStatus = EscapeMode.TopStatus;
            Display = State.Display;
            BottomStatus = State.BottomStatus;
            IsDisplayVisible = State.IsDisplayVisible;
            IsTopStatusVisible = State.IsTopStatusVisible;
            IsBottomStatusVisible = State.IsBottomStatusVisible;
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
