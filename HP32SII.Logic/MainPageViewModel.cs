using AdvancedTimer.Forms.Plugin.Abstractions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private const int DisplayLetterIntervalInMs = 200;
        private const int InactivityIntervalInMs = 10 * 60 * 1000;

        private EscapeMode escapeMode = EscapeMode.None;
        private KeyboardState keyboardState = KeyboardState.Default;
        private Dictionary<string, Func<KeyboardState>> defaultKeyboard;
        private Dictionary<string, Func<KeyboardState>> leftKeyboard;
        private Dictionary<string, Func<KeyboardState>> rightKeyboard;
        private Calculator calculator = new Calculator();
        private Output output = new Output();
        private bool pushAtNextAppend = false;
        private IAdvancedTimer timer = DependencyService.Get<IAdvancedTimer>();

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

        public MainPageViewModel()
        {
            ButtonCommand = new Command<Button>(HandleButton);
            timer.initTimer(InactivityIntervalInMs, TimerElapsed, false);
            timer.startTimer();

            Func<Func<double, double>, KeyboardState> monadic = MonadicOperation;
            Func<Func<double, double>, KeyboardState> dyadic = DyadicOperation;
            Func<string, KeyboardState> numeric = NumericKey;

            defaultKeyboard = new Dictionary<string, Func<KeyboardState>>
            {
                // First row
                { "SQRT", monadic.Compose(calculator.SquareRoot) },
                { "EXP", monadic.Compose(calculator.Exponential) },
                { "LN", monadic.Compose(calculator.NaturalLogarithm) },
                { "POW", dyadic.Compose(calculator.Power) },
                { "1/X", monadic.Compose(calculator.Invert) },
                { "SUM", DoNothing },
                // Second row
                { "STO", Store },
                { "RCL", Recall },
                { "R", DoNothing },
                { "SIN", DoNothing },
                { "COS", DoNothing },
                { "TAN", DoNothing },
                // Third row
                { "ENTER", Enter },
                { "SWAP", dyadic.Compose(calculator.Swap) },
                { "+/-", ChangeSign },
                { "E", DoNothing },
                { "BACK", Backspace },
                // Fourth row
                { "XEQ", DoNothing },
                { "7", numeric.Compose("7") },
                { "8", numeric.Compose("8") },
                { "9", numeric.Compose("9") },
                { "/", dyadic.Compose(calculator.Divide) },
                // Fifth row
                { "4", numeric.Compose("4") },
                { "5", numeric.Compose("5") },
                { "6", numeric.Compose("6") },
                { "*", dyadic.Compose(calculator.Multiply) },
                // Sixth row
                { "1", numeric.Compose("1") },
                { "2", numeric.Compose("2") },
                { "3", numeric.Compose("3") },
                { "-", dyadic.Compose(calculator.Subtract) },
                // Seventh row
                { "C", Clear },
                { "0", numeric.Compose("0") },
                { ".", HandleDot },
                { "R/S", DoNothing },
                { "+", dyadic.Compose(calculator.Add) },
            };

            leftKeyboard = new Dictionary<string, Func<KeyboardState>>
            {
                // First row
                { "SQRT", monadic.Compose(calculator.Square) },
                { "EXP", monadic.Compose(calculator.PowerOfTen) },
                { "LN", monadic.Compose(calculator.LogBase10) },
                { "POW", DoNothing },
                { "1/X", DoNothing },
                { "SUM", DoNothing },
                // Second row
                { "STO", DoNothing },
                { "RCL", DoNothing },
                { "R", DoNothing },
                { "SIN", DoNothing },
                { "COS", DoNothing },
                { "TAN", DoNothing },
                // Third row
                { "ENTER", DoNothing },
                { "SWAP", DoNothing },
                { "+/-", DoNothing },
                { "E", DoNothing },
                { "BACK", DoNothing },
                // Fourth row
                { "XEQ", DoNothing },
                { "7", DoNothing },
                { "8", DoNothing },
                { "9", DoNothing },
                { "/", DoNothing },
                // Fifth row
                { "4", DoNothing },
                { "5", DoNothing },
                { "6", monadic.Compose(calculator.ToRadian) },
                { "*", DoNothing },
                // Sixth row
                { "1", monadic.Compose(calculator.ToPound) },
                { "2", monadic.Compose(calculator.ToFahrenheit) },
                { "3", monadic.Compose(calculator.ToInch) },
                { "-", DoNothing },
                // Seventh row
                { "C", TurnOff },
                { "0", DoNothing },
                { ".", DoNothing },
                { "R/S", DoNothing },
                { "+", DoNothing },
            };

            rightKeyboard = new Dictionary<string, Func<KeyboardState>>
            {
                // First row
                { "SQRT", monadic.Compose(calculator.Square) },
                { "EXP", monadic.Compose(calculator.PowerOfTen) },
                { "LN", monadic.Compose(calculator.LogBase10) },
                { "POW", DoNothing },
                { "1/X", monadic.Compose(calculator.Factorial) },
                { "SUM", DoNothing },
                // Second row
                { "STO", DoNothing },
                { "RCL", DoNothing },
                { "R", DoNothing },
                { "SIN", DoNothing },
                { "COS", DoNothing },
                { "TAN", DoNothing },
                // Third row
                { "ENTER", DoNothing },
                { "SWAP", DoNothing },
                { "+/-", DoNothing },
                { "E", DoNothing },
                { "BACK", DoNothing },
                // Fourth row
                { "XEQ", DoNothing },
                { "7", DoNothing },
                { "8", DoNothing },
                { "9", DoNothing },
                { "/", DoNothing },
                // Fifth row
                { "4", DoNothing },
                { "5", DoNothing },
                { "6", monadic.Compose(calculator.ToRadian) },
                { "*", DoNothing },
                // Sixth row
                { "1", monadic.Compose(calculator.ToPound) },
                { "2", monadic.Compose(calculator.ToFahrenheit) },
                { "3", monadic.Compose(calculator.ToInch) },
                { "-", DoNothing },
                // Seventh row
                { "C", TurnOff },
                { "0", DoNothing },
                { ".", DoNothing },
                { "R/S", DoNothing },
                { "+", DoNothing },
            };
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            if (keyboardState == KeyboardState.WaitForDefault)
            {
                Display = output.ToString();
                timer.setInterval(InactivityIntervalInMs);
                timer.startTimer();
                keyboardState = GoToDefault();
            }
            else
            {
                keyboardState = TurnOff();
            }

        }

        private void HandleButton(Button button)
        {
            if (keyboardState == KeyboardState.Off && button != Buttons.Clear)
                return;

            RestartInactivityTimer();

            if (button == Buttons.Left)
            {
                if (escapeMode == EscapeMode.Left)
                {
                    escapeMode = ClearEscapeMode();
                }
                else
                {
                    escapeMode = GoToLeft();
                }
                return;
            }
            else if (button == Buttons.Right)
            {
                if (escapeMode == EscapeMode.Right)
                {
                    escapeMode = ClearEscapeMode();
                }
                else
                {
                    escapeMode = GoToRight();
                }
                return;
            }

            switch (keyboardState)
            {
                case KeyboardState.Off:
                    if (button == Buttons.Clear)
                    {
                        pushAtNextAppend = false;
                        TurnScreenOn();
                        keyboardState = GoToDefault();
                    }
                    break;
                case KeyboardState.Default:
                    if (escapeMode == EscapeMode.None)
                    {
                        keyboardState = defaultKeyboard[button.Name]();
                    }
                    else if (escapeMode == EscapeMode.Left)
                    {
                        keyboardState = leftKeyboard[button.Name]();
                    }
                    else
                    {
                        keyboardState = rightKeyboard[button.Name]();
                    }
                    break;
                case KeyboardState.Store:
                    if (button.Letter != null)
                    {
                        BottomStatus = "";
                        Display = $"STO  {button.Letter}";
                        calculator.Store(button.Letter, output.ToDouble());
                        timer.stopTimer();
                        timer.setInterval(DisplayLetterIntervalInMs);
                        timer.startTimer();
                        keyboardState = KeyboardState.WaitForDefault;
                    }
                    else if (button == Buttons.Solve)
                    {
                        // TODO display "INVALID (i)"
                    }
                    else if (button == Buttons.Divide)
                    {
                        Display = $"STO /  _";
                    }
                    else if (button == Buttons.Multiply)
                    {
                        Display = $"STO *  _";
                    }
                    else if (button == Buttons.Subtract)
                    {
                        Display = $"STO -  _";
                    }
                    else if (button == Buttons.Add)
                    {
                        Display = $"STO +  _";
                    }
                    else if (button == Buttons.Clear || button == Buttons.Back)
                    {
                        Display = output.ToString();
                        keyboardState = GoToDefault();
                    }
                    break;
                case KeyboardState.Recall:
                    if (button.Letter != null)
                    {
                        BottomStatus = "";
                        Display = $"RCL  {button.Letter}";
                        var recalled = calculator.Recall(button.Letter);
                        output.FromDouble(recalled);
                        timer.stopTimer();
                        timer.setInterval(DisplayLetterIntervalInMs);
                        timer.startTimer();
                        keyboardState = KeyboardState.WaitForDefault;
                    }
                    else if (button == Buttons.Clear || button == Buttons.Back)
                    {
                        Display = output.ToString();
                        keyboardState = GoToDefault();
                    }
                    break;
                case KeyboardState.WaitForDefault:
                    break;
                default:
                    break;
            }

            escapeMode = ClearEscapeMode();
        }

        private KeyboardState DoNothing()
        {
            return keyboardState;
        }

        private KeyboardState Enter()
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
            return GoToDefault();
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

        private KeyboardState ChangeSign()
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
            return GoToDefault();
        }

        private KeyboardState NumericKey(string button)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDigit(button);
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState HandleDot()
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDot();
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState Clear()
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState TurnOff()
        {
            TurnScreenOff();
            return KeyboardState.Off;
        }

        private KeyboardState Backspace()
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState MonadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = true;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState DyadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = false;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return GoToDefault();
        }

        private KeyboardState Store()
        {
            if (output.IsEditable)
            {
                output.Freeze();
                pushAtNextAppend = true;
            }

            Display = "STO  _";
            BottomStatus = "A..Z";
            return KeyboardState.Store;
        }

        private KeyboardState Recall()
        {
            if (output.IsEditable)
            {
                output.Freeze();
            }
            calculator.Push(output.ToDouble());

            Display = "RCL  _";
            BottomStatus = "A..Z";
            return KeyboardState.Recall;
        }

        private void TurnScreenOff()
        {
            IsDisplayVisible = false;
            IsTopStatusVisible = false;
            IsBottomStatusVisible = false;
        }

        private void TurnScreenOn()
        {
            IsDisplayVisible = true;
            IsTopStatusVisible = true;
            IsBottomStatusVisible = true;
        }

        private void RestartInactivityTimer()
        {
            timer.stopTimer();
            timer.startTimer();
        }
    }
}
