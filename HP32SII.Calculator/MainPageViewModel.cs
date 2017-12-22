using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private Calculator calculator = new Calculator();
        private Output output = new Output();
        private bool pushAtNextAppend = false;

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

        public ICommand EnterCommand { get; private set; }
        public ICommand ChangeSignCommand { get; private set; }
        public ICommand NumericCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DivideCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand SubtractCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand SwapCommand { get; private set; }

        public MainPageViewModel()
        {
            EnterCommand = new Command(HandleEnterKey);
            ChangeSignCommand = new Command(HandleChangeSign);
            NumericCommand = new Command<string>(HandleNumericKey);
            ClearCommand = new Command(HandleClearKey);
            DivideCommand = new Command(HandleDivide);
            MultiplyCommand = new Command(HandleMultiply);
            SubtractCommand = new Command(HandleSubtract);
            AddCommand = new Command(HandleAdd);
            BackspaceCommand = new Command(HandleBackspace);
            SwapCommand = new Command(HandleSwap);
        }

        private void HandleEnterKey()
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
        }

        private void HandleChangeSign()
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
        }

        private void HandleNumericKey(string digit)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.Append(digit);
            Display = output.ToString();
        }

        private void HandleClearKey()
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
        }

        private void HandleBackspace()
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
        }

        private void HandleDivide()
        {
            pushAtNextAppend = false;
            var z = calculator.Divide(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }

        private void HandleMultiply()
        {
            pushAtNextAppend = false;
            var z = calculator.Multiply(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }

        private void HandleSubtract()
        {
            pushAtNextAppend = false;
            var z = calculator.Subtract(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }

        private void HandleAdd()
        {
            pushAtNextAppend = false;
            var z = calculator.Add(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }

        private void HandleSwap()
        {
            pushAtNextAppend = false;
            var z = calculator.Swap(output.ToDouble());
            output.FromDouble(z);
            Display = output.ToString();
        }
    }
}
