using QuizApp.Model;
using QuizApp.View;
using System;
using System.ComponentModel;
using System.Windows;

namespace QuizApp.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand openCreateQuiz;
        public RelayCommand OpenCreateQuiz
        {
            get
            {
                return openCreateQuiz ?? new RelayCommand(obj =>
                {
                    OpenCreateQuizMethod();
                });
            }
        }

        private RelayCommand openQuiz;
        public RelayCommand OpenQuiz
        {
            get
            {
                return openQuiz ?? new RelayCommand(obj =>
                {
                    OpenQuizMethod();
                });
            }
        }

        // Открытие окон
        private void OpenCreateQuizMethod()
        {
            CreateQuiz newQuizWindow = new CreateQuiz();
            OpenWindow(newQuizWindow);
        }
        private void OpenQuizMethod()
        {
            MessageBox.Show("Open Quiz");
        }
        private void OpenWindow(Window newWindow)
        {
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
