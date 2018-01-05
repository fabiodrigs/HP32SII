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

        private State state = State.Default;
        private Dictionary<string, Func<State>> defaultKeyboard;
        private Dictionary<string, Func<State>> leftKeyboard;
        private Dictionary<string, Func<State>> rightKeyboard;
        private Dictionary<string, string> alphabeticKeyboard;
        private Calculator calculator = new Calculator();
        private Output output = new Output();
        private bool pushAtNextAppend = false;
        private IAdvancedTimer timer = DependencyService.Get<IAdvancedTimer>();

        public ICommand ButtonCommand { get; private set; }

        #region Properties
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
            ButtonCommand = new Command<string>(HandleButton);
            timer.initTimer(InactivityIntervalInMs, TimerElapsed, false);
            timer.startTimer();

            Func<Func<double, double>, State> monadic = MonadicOperation;
            Func<Func<double, double>, State> dyadic = DyadicOperation;
            Func<string, State> numeric = NumericKey;

            defaultKeyboard = new Dictionary<string, Func<State>>
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
                { "LEFT", GoToLeft },
                { "4", numeric.Compose("4") },
                { "5", numeric.Compose("5") },
                { "6", numeric.Compose("6") },
                { "*", dyadic.Compose(calculator.Multiply) },
                // Sixth row
                { "RIGHT", GoToRight },
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

            leftKeyboard = new Dictionary<string, Func<State>>
            {
                // First row
                { "SQRT", monadic.Compose(calculator.SquareRoot) },
                { "EXP", monadic.Compose(calculator.Exponential) },
                { "LN", monadic.Compose(calculator.NaturalLogarithm) },
                { "POW", DoNothing },
                { "1/X", monadic.Compose(calculator.Invert) },
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
                { "LEFT", GoToDefault },
                { "4", DoNothing },
                { "5", DoNothing },
                { "6", monadic.Compose(calculator.ToDegree) },
                { "*", DoNothing },
                // Sixth row
                { "RIGHT", GoToRight },
                { "1", monadic.Compose(calculator.ToKilo) },
                { "2", monadic.Compose(calculator.ToCelsius) },
                { "3", monadic.Compose(calculator.ToCentimeter) },
                { "-", DoNothing },
                // Seventh row
                { "C", TurnOff },
                { "0", DoNothing },
                { ".", DoNothing },
                { "R/S", DoNothing },
                { "+", DoNothing },
            };

            rightKeyboard = new Dictionary<string, Func<State>>
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
                { "LEFT", GoToLeft },
                { "4", DoNothing },
                { "5", DoNothing },
                { "6", monadic.Compose(calculator.ToRadian) },
                { "*", DoNothing },
                // Sixth row
                { "RIGHT", GoToDefault },
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

            alphabeticKeyboard = new Dictionary<string, string>
            {
                // First row
                { "SQRT", "A" },
                { "EXP", "B" },
                { "LN", "C" },
                { "POW", "D" },
                { "1/X", "E" },
                { "SUM", "F" },
                // Second row
                { "STO", "G" },
                { "RCL", "H" },
                { "R", "I" },
                { "SIN", "J" },
                { "COS", "K" },
                { "TAN", "L" },
                // Third row
                { "ENTER", "M" },
                { "SWAP", "N" },
                { "+/-", "O" },
                { "E", "P" },
                { "BACK", null },
                // Fourth row
                { "XEQ", null },
                { "7", "Q" },
                { "8", "R" },
                { "9", "S" },
                { "/", null },
                // Fifth row
                { "LEFT", null },
                { "4", "T" },
                { "5", "U" },
                { "6", "V" },
                { "*", null },
                // Sixth row
                { "RIGHT", null },
                { "1", "W" },
                { "2", "X" },
                { "3", "Y" },
                { "-", null },
                // Seventh row
                { "C", null },
                { "0", "Z" },
                { ".", "i" },
                { "R/S", null },
                { "+", null },
            };
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            if (state == State.WaitForDefault)
            {
                Display = output.ToString();
                timer.setInterval(InactivityIntervalInMs);
                timer.startTimer();
                state = GoToDefault();
            }
            else
            {
                state = TurnOff();
            }

        }

        private void HandleButton(string button)
        {
            if (state != State.Off || button == "C")
            {
                timer.stopTimer();
                timer.startTimer();
            }

            switch (state)
            {
                case State.Off:
                    if (button == "C")
                    {
                        pushAtNextAppend = false;
                        TurnScreenOn();
                        state = GoToDefault();
                    }
                    break;
                case State.Default:
                    state = defaultKeyboard[button]();
                    break;
                case State.Left:
                    state = leftKeyboard[button]();
                    break;
                case State.Right:
                    state = rightKeyboard[button]();
                    break;
                case State.Store:
                    if (alphabeticKeyboard[button] != null)
                    {
                        BottomStatus = "";
                        Display = $"STO  {alphabeticKeyboard[button]}";
                        calculator.Store(alphabeticKeyboard[button], output.ToDouble());
                        timer.stopTimer();
                        timer.setInterval(DisplayLetterIntervalInMs);
                        timer.startTimer();
                        state = State.WaitForDefault;
                    }
                    else if (button == "C" || button == "BACK")
                    {
                        Display = output.ToString();
                        state = GoToDefault();
                    }
                    break;
                case State.Recall:
                    if (alphabeticKeyboard[button] != null)
                    {
                        BottomStatus = "";
                        Display = $"RCL  {alphabeticKeyboard[button]}";
                        var recalled = calculator.Recall(alphabeticKeyboard[button]);
                        output.FromDouble(recalled);
                        timer.stopTimer();
                        timer.setInterval(DisplayLetterIntervalInMs);
                        timer.startTimer();
                        state = State.WaitForDefault;
                    }
                    else if (button == "C" || button == "BACK")
                    {
                        Display = output.ToString();
                        state = GoToDefault();
                    }
                    break;
                case State.WaitForDefault:
                    break;
                default:
                    break;
            }
        }

        private State DoNothing()
        {
            return state;
        }

        private State Enter()
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
            return GoToDefault();
        }

        private State GoToLeft()
        {
            TopStatus = "  <=";
            return State.Left;
        }

        private State GoToDefault()
        {
            TopStatus = "";
            BottomStatus = "";
            return State.Default;
        }

        private State GoToRight()
        {
            TopStatus = "        =>";
            return State.Right;
        }

        private State ChangeSign()
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
            return GoToDefault();
        }

        private State NumericKey(string button)
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

        private State HandleDot()
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

        private State Clear()
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
            return GoToDefault();
        }

        private State TurnOff()
        {
            TurnScreenOff();
            return State.Off;
        }

        private State Backspace()
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
            return GoToDefault();
        }

        private State MonadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = true;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return GoToDefault();
        }

        private State DyadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = false;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return GoToDefault();
        }

        private State Store()
        {
            if (output.IsEditable)
            {
                output.Freeze();
                pushAtNextAppend = true;
            }

            Display = "STO  _";
            BottomStatus = "A..Z";
            return State.Store;
        }

        private State Recall()
        {
            if (output.IsEditable)
            {
                output.Freeze();
            }
            calculator.Push(output.ToDouble());

            Display = "RCL  _";
            BottomStatus = "A..Z";
            return State.Recall;
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
    }
}
