using Microsoft.Win32;
using QuizApp.Model;
using QuizApp.View;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace QuizApp.ViewModel
{
    public class MainWindowViewModel
    {
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
            View.Quiz newQuizWindow = new View.Quiz();
            newQuizWindow.DataContext = new QuizViewModel(LoadQuizFromJson());
            OpenWindow(newQuizWindow);
        }

        private void OpenWindow(Window newWindow)
        {
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newWindow.Show();
            Application.Current.MainWindow.Close();
        }
        private Model.Quiz LoadQuizFromJson()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Model.Quiz>(json);
            }
            return null;
        }
    }
}
