using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private State state = State.Default;
        private Dictionary<string, Func<State>> defaultKeyboard;
        private Dictionary<string, Func<State>> leftKeyboard;
        private Dictionary<string, Func<State>> rightKeyboard;
        private Dictionary<string, string> alphabeticKeyboard;
        private Calculator calculator = new Calculator();
        private Output output = new Output();
        private bool pushAtNextAppend = false;

        public ICommand ButtonCommand { get; private set; }

        #region TopStatus property
        private string topStatus = "";
        public string TopStatus
        {
            get
            {
                return topStatus;
            }
            set
            {
                topStatus = value;
                RaisePropertyChanged(nameof(TopStatus));
            }
        }
        #endregion
        #region BottomStatus property
        private string bottomStatus = "";
        public string BottomStatus
        {
            get
            {
                return bottomStatus;
            }
            set
            {
                bottomStatus = value;
                RaisePropertyChanged(nameof(BottomStatus));
            }
        }
        #endregion
        #region Display property
        private string display = " 0";
        public string Display
        {
            get
            {
                return display;
            }
            set
            {
                display = value;
                RaisePropertyChanged(nameof(Display));
            }
        }
        #endregion

        public MainPageViewModel()
        {
            ButtonCommand = new Command<string>(HandleButton);

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
                { "LEFT", GoToDefault },
                { "4", DoNothing },
                { "5", DoNothing },
                { "6", monadic.Compose(calculator.ToRadian) },
                { "*", DoNothing },
                // Sixth row
                { "RIGHT", GoToRight },
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

        private void HandleButton(string button)
        {
            switch (state)
            {
                case State.Off:
                    if (button == "C")
                    {
                        Clear();
                        state = State.Default;
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
                        Display = "STO  {alphabeticKeyboard[button]}";
                        Task.Delay(500).Wait();

                        calculator.Store(alphabeticKeyboard[button], output.ToDouble());
                        Display = output.ToString();
                        state = GoToDefault();
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
                        Display = "RCL  {alphabeticKeyboard[button]}";
                        Task.Delay(500).Wait();

                        var recalled = calculator.Recall(alphabeticKeyboard[button]);
                        output.FromDouble(recalled);
                        Display = output.ToString();
                        state = GoToDefault();
                    }
                    else if (button == "C" || button == "BACK")
                    {
                        Display = output.ToString();
                        state = GoToDefault();
                    }
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
            Display = "";
            TopStatus = "";
            BottomStatus = "";
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
    }
}
