using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using HP32SII.Logic.EscapeModes;
using HP32SII.Logic.States;
using Xamarin.Forms;
using Plugin.Battery;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private int BatteryLowThreshold = 15;

        private bool isOn = true;
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

        private string batteryStatus = "";
        public string BatteryStatus
        {
            get { return batteryStatus; }
            private set { Set(ref batteryStatus, value); }
        }

        private bool isBatteryStatusVisible = true;
        public bool IsBatteryStatusVisible
        {
            get { return isBatteryStatusVisible; }
            private set { Set(ref isBatteryStatusVisible, value); }
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
            LeftArrowCommand = new Command(HandleLeftArrow, () => isOn);
            RightArrowCommand = new Command(HandleRightArrow, () => isOn);
            ButtonCommand = new Command<Button>(HandleButton, IsButtonEnabled);

            timer = new Timer(TimerElapsed);

            State.Timer = timer;
            State.Buttons = Buttons;
            DefaultState.AssignButtonOperations();

            escapeMode = new NoEscapeMode();
            state = new DefaultState();
            UpdateDisplay();
        }
        #endregion

        private void HandleLeftArrow()
        {
            RestartTimerIfStateNotWaiting();
            escapeMode = escapeMode.HandleLeftArrow();
            TopStatus = EscapeMode.TopStatus;
        }

        private void HandleRightArrow()
        {
            RestartTimerIfStateNotWaiting();
            escapeMode = escapeMode.HandleRightArrow();
            TopStatus = EscapeMode.TopStatus;
        }

        private void HandleButton(Button button)
        {
            if (isOn)
            {
                if (IsTurningOff(button))
                {
                    TurnOff();
                }
                else
                {
                    RestartTimerIfStateNotWaiting();
                    state = state.HandleButton(button, escapeMode);
                }
            }
            else
            {
                if (button == Buttons.Clear)
                {
                    TurnOn();
                }
            }

            escapeMode = new NoEscapeMode();
            UpdateDisplay();
        }

        private void TimerElapsed()
        {
            if (state.IsWaiting())
            {
                state = state.TimerElapsed();
            }
            else
            {
                TurnOff();
            }

            UpdateDisplay();
        }

        private void RestartTimerIfStateNotWaiting()
        {
            if (!state.IsWaiting())
            {
                timer.StartWithInactivityInterval();
            }
        }

        private void UpdateDisplay()
        {
            TopStatus = EscapeMode.TopStatus;
            Display = State.Display;
            BottomStatus = State.BottomStatus;
            BatteryStatus = CrossBattery.Current.RemainingChargePercent < BatteryLowThreshold ? "BAT" : "";
        }

        private bool IsButtonEnabled(Button button)
        {
            if (button == Buttons.Clear)
                return true;
            else
                return isOn;
        }

        private bool IsTurningOff(Button button)
        {
            if (escapeMode.IsEscaped())
                return button == Buttons.Clear;
            else
                return false;
        }

        private void TurnOff()
        {
            if (state.IsWaiting())
            {
                state.TimerElapsed();
            }
            timer.Stop();
            TurnScreenOff();
            isOn = false;
        }

        private void TurnOn()
        {
            timer.StartWithInactivityInterval();
            TurnScreenOn();
            isOn = true;
        }

        private void TurnScreenOff()
        {
            IsDisplayVisible = false;
            IsTopStatusVisible = false;
            IsBottomStatusVisible = false;
            IsBatteryStatusVisible = false;
        }

        private void TurnScreenOn()
        {
            IsDisplayVisible = true;
            IsTopStatusVisible = true;
            IsBottomStatusVisible = true;
            IsBatteryStatusVisible = true;
        }
    }
}
