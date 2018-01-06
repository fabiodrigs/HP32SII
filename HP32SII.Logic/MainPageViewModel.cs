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
        #region Constructor
        public MainPageViewModel()
        {
            ButtonCommand = new Command<Button>(HandleButton);
            timer.initTimer(InactivityIntervalInMs, TimerElapsed, false);
            timer.startTimer();

            Func<Func<double, double>, KeyboardState> monadic = MonadicOperation;
            Func<Func<double, double>, KeyboardState> dyadic = DyadicOperation;
            Func<string, KeyboardState> numeric = NumericKey;

            Buttons.Sqrt.DefaultOperation = monadic.Compose(calculator.SquareRoot);
            Buttons.Sqrt.LeftOperation = monadic.Compose(calculator.Square);
            Buttons.Sqrt.RightOperation = DoNothing;

            Buttons.Exp.DefaultOperation = monadic.Compose(calculator.Exponential);
            Buttons.Exp.LeftOperation = monadic.Compose(calculator.PowerOfTen);
            Buttons.Exp.RightOperation = monadic.Compose(calculator.PowerOfTen);

            Buttons.Ln.DefaultOperation = monadic.Compose(calculator.NaturalLogarithm);
            Buttons.Ln.LeftOperation = monadic.Compose(calculator.LogBase10);
            Buttons.Ln.RightOperation = monadic.Compose(calculator.LogBase10);

            Buttons.Pow.DefaultOperation = dyadic.Compose(calculator.Power);
            Buttons.Pow.LeftOperation = DoNothing;
            Buttons.Pow.RightOperation = DoNothing;

            Buttons.Invert.DefaultOperation = monadic.Compose(calculator.Invert);
            Buttons.Invert.LeftOperation = DoNothing;
            Buttons.Invert.RightOperation = monadic.Compose(calculator.Factorial);

            Buttons.Sum.DefaultOperation = DoNothing;
            Buttons.Sum.LeftOperation = DoNothing;
            Buttons.Sum.RightOperation = DoNothing;

            Buttons.Store.DefaultOperation = Store;
            Buttons.Store.LeftOperation = DoNothing;
            Buttons.Store.RightOperation = DoNothing;

            Buttons.Recall.DefaultOperation = Recall;
            Buttons.Recall.LeftOperation = DoNothing;
            Buttons.Recall.RightOperation = DoNothing;

            Buttons.RollDown.DefaultOperation = DoNothing;
            Buttons.RollDown.LeftOperation = DoNothing;
            Buttons.RollDown.RightOperation = DoNothing;

            Buttons.Sin.LeftOperation = DoNothing;
            Buttons.Sin.RightOperation = DoNothing;
            Buttons.Sin.DefaultOperation = DoNothing;

            Buttons.Cos.LeftOperation = DoNothing;
            Buttons.Cos.RightOperation = DoNothing;
            Buttons.Cos.DefaultOperation = DoNothing;

            Buttons.Tan.DefaultOperation = DoNothing;
            Buttons.Tan.LeftOperation = DoNothing;
            Buttons.Tan.RightOperation = DoNothing;

            Buttons.Enter.DefaultOperation = Enter;
            Buttons.Enter.LeftOperation = DoNothing;
            Buttons.Enter.RightOperation = DoNothing;

            Buttons.Swap.DefaultOperation = dyadic.Compose(calculator.Swap);
            Buttons.Swap.LeftOperation = DoNothing;
            Buttons.Swap.RightOperation = DoNothing;

            Buttons.ChangeSign.DefaultOperation = ChangeSign;
            Buttons.ChangeSign.LeftOperation = DoNothing;
            Buttons.ChangeSign.RightOperation = DoNothing;

            Buttons.E.LeftOperation = DoNothing;
            Buttons.E.DefaultOperation = DoNothing;
            Buttons.E.RightOperation = DoNothing;

            Buttons.Back.DefaultOperation = Backspace;
            Buttons.Back.LeftOperation = DoNothing;
            Buttons.Back.RightOperation = DoNothing;

            Buttons.Xeq.DefaultOperation = DoNothing;
            Buttons.Xeq.LeftOperation = DoNothing;
            Buttons.Xeq.RightOperation = DoNothing;

            Buttons.Seven.DefaultOperation = numeric.Compose("7");
            Buttons.Seven.LeftOperation = DoNothing;
            Buttons.Seven.RightOperation = DoNothing;

            Buttons.Eight.DefaultOperation = numeric.Compose("8");
            Buttons.Eight.LeftOperation = DoNothing;
            Buttons.Eight.RightOperation = DoNothing;

            Buttons.Nine.DefaultOperation = numeric.Compose("9");
            Buttons.Nine.LeftOperation = DoNothing;
            Buttons.Nine.RightOperation = DoNothing;

            Buttons.Divide.DefaultOperation = dyadic.Compose(calculator.Divide);
            Buttons.Divide.LeftOperation = DoNothing;
            Buttons.Divide.RightOperation = DoNothing;

            Buttons.Four.DefaultOperation = numeric.Compose("4");
            Buttons.Four.LeftOperation = DoNothing;
            Buttons.Four.RightOperation = DoNothing;

            Buttons.Five.DefaultOperation = numeric.Compose("5");
            Buttons.Five.LeftOperation = DoNothing;
            Buttons.Five.RightOperation = DoNothing;

            Buttons.Six.DefaultOperation = numeric.Compose("6");
            Buttons.Six.LeftOperation = monadic.Compose(calculator.ToRadian);
            Buttons.Six.RightOperation = monadic.Compose(calculator.ToRadian);

            Buttons.Multiply.DefaultOperation = dyadic.Compose(calculator.Multiply);
            Buttons.Multiply.LeftOperation = DoNothing;
            Buttons.Multiply.RightOperation = DoNothing;

            Buttons.One.DefaultOperation = numeric.Compose("1");
            Buttons.One.LeftOperation = monadic.Compose(calculator.ToPound);
            Buttons.One.RightOperation = monadic.Compose(calculator.ToPound);

            Buttons.Two.DefaultOperation = numeric.Compose("2");
            Buttons.Two.LeftOperation = monadic.Compose(calculator.ToFahrenheit);
            Buttons.Two.RightOperation = monadic.Compose(calculator.ToFahrenheit);

            Buttons.Three.DefaultOperation = numeric.Compose("3");
            Buttons.Three.LeftOperation = monadic.Compose(calculator.ToInch);
            Buttons.Three.RightOperation = monadic.Compose(calculator.ToInch);

            Buttons.Subtract.DefaultOperation = dyadic.Compose(calculator.Subtract);
            Buttons.Subtract.LeftOperation = DoNothing;
            Buttons.Subtract.RightOperation = DoNothing;

            Buttons.Clear.DefaultOperation = Clear;
            Buttons.Clear.LeftOperation = TurnOff;
            Buttons.Clear.RightOperation = TurnOff;

            Buttons.Zero.DefaultOperation = numeric.Compose("0");
            Buttons.Zero.LeftOperation = DoNothing;
            Buttons.Zero.RightOperation = DoNothing;

            Buttons.Dot.DefaultOperation = HandleDot;
            Buttons.Dot.LeftOperation = DoNothing;
            Buttons.Dot.RightOperation = DoNothing;

            Buttons.Solve.DefaultOperation = DoNothing;
            Buttons.Solve.LeftOperation = DoNothing;
            Buttons.Solve.RightOperation = DoNothing;

            Buttons.Add.DefaultOperation = dyadic.Compose(calculator.Add);
            Buttons.Add.LeftOperation = DoNothing;
            Buttons.Add.RightOperation = DoNothing;
        }
        #endregion

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
                        keyboardState = button.DefaultOperation();
                    }
                    else if (escapeMode == EscapeMode.Left)
                    {
                        keyboardState = button.LeftOperation();
                    }
                    else
                    {
                        keyboardState = button.RightOperation();
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
