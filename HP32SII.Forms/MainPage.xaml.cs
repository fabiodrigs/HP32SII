using Xamarin.Forms;

namespace HP32SII
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Logic.MainPageViewModel();
        }
    }
}
