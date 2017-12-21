using System.Windows;

namespace HP32SII.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Logic.MainPageViewModel(); ;
        }
    }
}
