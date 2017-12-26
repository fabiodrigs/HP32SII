using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private State state = State.Default;
        private Dictionary<string, Button> keyboard;
        private Dictionary<Tuple<string, State>, Func<double, double>> monadicOperators;
        private Dictionary<Tuple<string, State>, Func<double, double>> dyadicOperators;
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

            keyboard = new Dictionary<string, Button>
            {
                // First row
                { "SQRT", new Button(MonadicOperation, MonadicOperation, DoNothing) },
                { "EXP", new Button(MonadicOperation, MonadicOperation, DoNothing) },
                { "LN", new Button(MonadicOperation, MonadicOperation, DoNothing) },
                { "POW", new Button(DyadicOperation, DoNothing, DoNothing) },
                { "1/X", new Button(MonadicOperation, MonadicOperation, DoNothing) },
                { "SUM", new Button(DoNothing, DoNothing, DoNothing) },
                // Second row
                { "STO", new Button(DoNothing, DoNothing, DoNothing) },
                { "RCL", new Button(DoNothing, DoNothing, DoNothing) },
                { "R", new Button(DoNothing, DoNothing, DoNothing) },
                { "SIN", new Button(DoNothing, DoNothing, DoNothing) },
                { "COS", new Button(DoNothing, DoNothing, DoNothing) },
                { "TAN", new Button(DoNothing, DoNothing, DoNothing) },
                // Third row
                { "ENTER", new Button(Enter, DoNothing, DoNothing) },
                { "SWAP", new Button(DyadicOperation, DoNothing, DoNothing) },
                { "+/-", new Button(ChangeSign, DoNothing, DoNothing) },
                { "E", new Button(DoNothing, DoNothing, DoNothing) },
                { "BACK", new Button(Backspace, DoNothing, DoNothing) },
                // Fourth row
                { "XEQ", new Button(DoNothing, DoNothing, DoNothing) },
                { "7", new Button(NumericKey, DoNothing, DoNothing) },
                { "8", new Button(NumericKey, DoNothing, DoNothing) },
                { "9", new Button(NumericKey, DoNothing, DoNothing) },
                { "/", new Button(DyadicOperation, DoNothing, DoNothing) },
                // Fifth row
                { "LEFT", new Button(LeftArrowDefaultOrRightState, LeftArrowLeftState, LeftArrowDefaultOrRightState) },
                { "4", new Button(NumericKey, DoNothing, DoNothing) },
                { "5", new Button(NumericKey, DoNothing, DoNothing) },
                { "6", new Button(NumericKey, MonadicOperation, MonadicOperation) },
                { "*", new Button(DyadicOperation, DoNothing, DoNothing) },
                // Sixth row
                { "RIGHT", new Button(RightArrowDefaultOrLeftState, RightArrowDefaultOrLeftState, RightArrowRightState) },
                { "1", new Button(NumericKey, MonadicOperation, MonadicOperation) },
                { "2", new Button(NumericKey, MonadicOperation, MonadicOperation) },
                { "3", new Button(NumericKey, MonadicOperation, MonadicOperation) },
                { "-", new Button(DyadicOperation, DoNothing, DoNothing) },
                // Seventh row
                { "C", new Button(Clear, TurnOff, TurnOff) },
                { "0", new Button(NumericKey, DoNothing, DoNothing) },
                { ".", new Button(HandleDot, DoNothing, DoNothing) },
                { "R/S", new Button(DoNothing, DoNothing, DoNothing) },
                { "+", new Button(DyadicOperation, DoNothing, DoNothing) },
            };

            monadicOperators = new Dictionary<Tuple<string, State>, Func<double, double>>
            {
                { new Tuple<string, State>("SQRT", State.Default), calculator.SquareRoot },
                { new Tuple<string, State>("SQRT", State.Left), calculator.Square },
                { new Tuple<string, State>("EXP", State.Default), calculator.Exponential },
                { new Tuple<string, State>("EXP", State.Left), calculator.PowerOfTen },
                { new Tuple<string, State>("LN", State.Default), calculator.NaturalLogarithm },
                { new Tuple<string, State>("LN", State.Left), calculator.LogBase10 },
                { new Tuple<string, State>("1/X", State.Default), calculator.Invert },
                { new Tuple<string, State>("1/X", State.Left), calculator.Factorial },
            };

            dyadicOperators = new Dictionary<Tuple<string, State>, Func <double, double>>
            {
                { new Tuple<string, State>("/", State.Default), calculator.Divide },
                { new Tuple<string, State>("*", State.Default), calculator.Multiply },
                { new Tuple<string, State>("-", State.Default), calculator.Subtract },
                { new Tuple<string, State>("+", State.Default), calculator.Add },
                { new Tuple<string, State>("SWAP", State.Default), calculator.Swap },
                { new Tuple<string, State>("POW", State.Default), calculator.Power },
                { new Tuple<string, State>("6", State.Left), calculator.ToDegree },
                { new Tuple<string, State>("6", State.Right), calculator.ToRadian },
                { new Tuple<string, State>("1", State.Left), calculator.ToKilo },
                { new Tuple<string, State>("1", State.Right), calculator.ToPound },
                { new Tuple<string, State>("2", State.Left), calculator.ToCelsius },
                { new Tuple<string, State>("2", State.Right), calculator.ToFahrenheit },
                { new Tuple<string, State>("3", State.Left), calculator.ToCentimeter },
                { new Tuple<string, State>("3", State.Right), calculator.ToInch },
            };
        }

        private void HandleButton(string button)
        {
            switch (state)
            {
                case State.Off:
                    if (button == "C")
                    {
                        Clear(button);
                        state = State.Default;
                    }
                    break;
                case State.Default:
                    state = keyboard[button].DefaultAction(button);
                    break;
                case State.Left:
                    state = keyboard[button].LeftAction(button);
                    break;
                case State.Right:
                    state = keyboard[button].RightAction(button);
                    break;
                default:
                    break;
            }

            switch (state)
            {
                case State.Off:
                    Display = "";
                    TopStatus = "";
                    BottomStatus = "";
                    break;
                case State.Default:
                    TopStatus = "";
                    break;
                case State.Left:
                    TopStatus = "  <=";
                    break;
                case State.Right:
                    TopStatus = "        =>";
                    break;
                default:
                    break;
            }
        }

        private State DoNothing(string button)
        {
            return state;
        }


        private State Enter(string button)
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
            return State.Default;
        }

        private State LeftArrowDefaultOrRightState(string button)
        {
            return State.Left;
        }

        private State LeftArrowLeftState(string button)
        {
            return State.Default;
        }

        private State RightArrowDefaultOrLeftState(string button)
        {
            return State.Right;
        }

        private State RightArrowRightState(string button)
        {
            return State.Default;
        }

        private State ChangeSign(string button)
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
            return State.Default;
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
            return State.Default;
        }

        private State HandleDot(string button)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDot();
            Display = output.ToString();
            return State.Default;
        }

        private State Clear(string button)
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
            return State.Default;
        }

        private State TurnOff(string button)
        {
            return State.Off;
        }

        private State Backspace(string button)
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
            return State.Default;
        }

        private State MonadicOperation(string button)
        {
            pushAtNextAppend = true;
            var operation = monadicOperators[new Tuple<string, State>(button, state)];
            var z = operation(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
            return State.Default;
        }

        private State DyadicOperation(string button)
        {
            pushAtNextAppend = false;
            var operation = dyadicOperators[new Tuple<string, State>(button, state)];
            var z = operation(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
            return State.Default;
        }
    }
}
