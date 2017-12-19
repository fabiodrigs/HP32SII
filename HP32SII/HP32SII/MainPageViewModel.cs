using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;

namespace HP32SII
{
    public class MainPageViewModel : ViewModelBase
    {
        private string entryText = "";
        public string EntryText
        {
            get
            {
                return entryText;
            }
            set
            {
                entryText = value;
                RaisePropertyChanged(nameof(EntryText));
            }
        }

        public ICommand EnterCommand { get; private set; }
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
            EntryText = "Enter key pressed";
        }

        private void HandleNumericKey(string number)
        {
            EntryText += number;
        }

        private void HandleClearKey()
        {
            EntryText = "";
        }

        private void HandleBackspace()
        {
            EntryText = EntryText.Remove(EntryText.Length - 1);
        }

        private void HandleDivide()
        {

        }

        private void HandleMultiply()
        {

        }

        private void HandleSubtract()
        {

        }

        private void HandleAdd()
        {

        }
    }
}
