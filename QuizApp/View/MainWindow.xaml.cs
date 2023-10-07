using QuizApp.ViewModel;
using System.Windows;

namespace QuizApp.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
