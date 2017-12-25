using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private Dictionary<string, Button> keyboard;
        private Dictionary<string, Func<double, double>> dyadicOperators;
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
                { "SQRT", new Button(null, null, null) },
                { "EXP", new Button(null, null, null) },
                { "LN", new Button(null, null, null) },
                { "POW", new Button(null, null, null) },
                { "1/X", new Button(null, null, null) },
                { "SUM", new Button(null, null, null) },
                // Second row
                { "STO", new Button(null, null, null) },
                { "RCL", new Button(null, null, null) },
                { "R", new Button(null, null, null) },
                { "SIN", new Button(null, null, null) },
                { "COS", new Button(null, null, null) },
                { "TAN", new Button(null, null, null) },
                // Third row
                { "ENTER", new Button(Enter, null, null) },
                { "SWAP", new Button(DyadicOperation, null, null) },
                { "+/-", new Button(ChangeSign, null, null) },
                { "E", new Button(null, null, null) },
                { "BACK", new Button(Backspace, null, null) },
                // Fourth row
                { "XEQ", new Button(null, null, null) },
                { "7", new Button(NumericKey, null, null) },
                { "8", new Button(NumericKey, null, null) },
                { "9", new Button(NumericKey, null, null) },
                { "/", new Button(DyadicOperation, null, null) },
                // Fifth row
                { "LEFT", new Button(LeftArrow, null, null) },
                { "4", new Button(NumericKey, null, null) },
                { "5", new Button(NumericKey, null, null) },
                { "6", new Button(NumericKey, null, null) },
                { "*", new Button(DyadicOperation, null, null) },
                // Sixth row
                { "RIGHT", new Button(RightArrow, null, null) },
                { "1", new Button(NumericKey, null, null) },
                { "2", new Button(NumericKey, null, null) },
                { "3", new Button(NumericKey, null, null) },
                { "-", new Button(DyadicOperation, null, null) },
                // Seventh row
                { "C", new Button(Clear, null, null) },
                { "0", new Button(null, null, null) },
                { ".", new Button(HandleDot, null, null) },
                { "R/S", new Button(null, null, null) },
                { "+", new Button(DyadicOperation, null, null) },
            };

            dyadicOperators = new Dictionary<string, Func<double, double>>
            {
                { "/", calculator.Divide },
                { "*", calculator.Multiply },
                { "-", calculator.Subtract },
                { "+", calculator.Add },
                { "SWAP", calculator.Swap },
            };
        }

        private void HandleButton(string button)
        {
            keyboard[button].DefaultAction(button);
        }

        private void Enter(string button)
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
        }

        private void LeftArrow(string button)
        {
            TopStatus = "  <=";
        }

        private void RightArrow(string button)
        {
            TopStatus = "        =>";
        }

        private void ChangeSign(string button)
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
        }

        private void NumericKey(string button)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDigit(button);
            Display = output.ToString();
        }

        private void HandleDot(string button)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDot();
            Display = output.ToString();
        }

        private void Clear(string button)
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
        }
        
        private void DyadicOperation(string button)
        {
            pushAtNextAppend = false;
            var operation = dyadicOperators[button];
            var z = operation(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }

        private void Backspace(string button)
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
        }
        
    }
}
