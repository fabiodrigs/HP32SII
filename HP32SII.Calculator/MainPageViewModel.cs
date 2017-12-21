using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII.Logic
{
    public class MainPageViewModel : ViewModelBase
    {
        private Calculator calculator = new Calculator();

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
        }

        private void HandleEnterKey()
        {
            calculator.Push(Convert.ToDouble(Display.Replace(" ", "").Replace("_", "")));
            if (Display[Display.Length - 1] == '_')
            {
                Display = Display.Remove(Display.Length - 1);
            }
        }

        private void HandleChangeSign()
        {
            if (Convert.ToDouble(Display.Replace(" ", "").Replace("_", "")) != 0.0)
            {
                Display = (Display[0] == ' ' ? '-' : ' ') + Display.Substring(1);
            }
        }

        private void HandleNumericKey(string number)
        {
            if (Display.Length < 13)
            {
                if (Display.Length == 2 && Display[1] == '0' || Display[Display.Length - 1] == '_')
                {
                    Display = Display.Remove(Display.Length - 1);
                    Display += number + '_';
                }
                else
                {
                    Display = " " + number + "_";
                }
            }
        }

        private void HandleClearKey()
        {
            Display = " 0";
        }

        private void HandleBackspace()
        {
            if (Display.Length > 2)
            {
                if (Display.Length == 3)
                {
                    Display = " 0";
                }
                else if (Display[Display.Length - 1] == '_')
                {
                    Display = Display.Remove(Display.Length - 2);
                    Display += '_';                    
                }
                else
                {
                    Display = " 0";
                }
            }
        }

        private void HandleDivide()
        {
            var number = Convert.ToDouble(Display.Replace(" ", "").Replace("_", ""));
            var result = calculator.Divide(number).ToString();
            Display = (result[0] == '-' ? "" : " ") + result;
        }

        private void HandleMultiply()
        {
            var number = Convert.ToDouble(Display.Replace(" ", "").Replace("_", ""));
            var result = calculator.Multiply(number).ToString();
            Display = (result[0] == '-' ? "" : " ") + result;
        }

        private void HandleSubtract()
        {
            var number = Convert.ToDouble(Display.Replace(" ", "").Replace("_", ""));
            var result = calculator.Subtract(number).ToString();
            Display = (result[0] == '-' ? "" : " ") + result;
        }

        private void HandleAdd()
        {
            var number = Convert.ToDouble(Display.Replace(" ", "").Replace("_", ""));
            var result = calculator.Add(number).ToString();
            Display = (result[0] == '-' ? "" : " ") + result;
        }
    }
}
