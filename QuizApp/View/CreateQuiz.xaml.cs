using QuizApp.ViewModel;
using System.Windows;

namespace QuizApp.View
{
    public partial class CreateQuiz : Window
    {
        public CreateQuiz()
        {
            InitializeComponent();
            DataContext = new CreateQuizViewModel();
        }
    }
}
