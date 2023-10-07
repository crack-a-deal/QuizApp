using Microsoft.Win32;
using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizApp.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private int currentIndex = 0;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                OnPropertyChanged(nameof(QuestionText));
                OnPropertyChanged(nameof(Options));
                OnPropertyChanged(nameof(Questions));
                OnPropertyChanged(nameof(QuestionIndex));
            }
        }
        public QuizViewModel(Quiz newQuiz)
        {
            quiz = newQuiz;
        }

        private Quiz quiz;
        public Quiz Quiz
        {
            get { return quiz; }
            set
            {
                quiz = value;
                OnPropertyChanged(nameof(Quiz));
            }
        }

        public string QuizName
        {
            get { return $"Тест: {Quiz.Name}"; }
        }
        public string QuestionText
        {
            get { return $"Вопрос: {Quiz.Questions[currentIndex].Text}"; }
        }
        public List<string> Options
        {
            get { return quiz.Questions[currentIndex].Options; }
            set
            {
                if (quiz.Questions[currentIndex].Options != value)
                {
                    quiz.Questions[currentIndex].Options = value;
                    OnPropertyChanged(nameof(Options));
                }
            }
        }
        public List<Question> Questions
        {
            get { return quiz.Questions; }
            set
            {
                if (quiz.Questions != value)
                {
                    quiz.Questions = value;
                    OnPropertyChanged(nameof(Questions));
                }
            }
        }
        public string QuestionIndex
        {
            get { return $"{currentIndex+1} из {Quiz.Questions.Count}"; }
        }

        private string _selectedRadioButtonText;
        public string SelectedRadioButtonText
        {
            get { return _selectedRadioButtonText; }
            set
            {
                if (_selectedRadioButtonText != value)
                {
                    _selectedRadioButtonText = value;
                    OnPropertyChanged(nameof(SelectedRadioButtonText));
                }
            }
        }

        // Загрузить следующий вопрос
        private RelayCommand nextQuestion;
        public RelayCommand NextQuestion
        {
            get
            {
                return nextQuestion ?? new RelayCommand(obj =>
                {
                    LoadNextQuestion();
                });
            }
        }
        private void LoadNextQuestion()
        {
            if (CurrentIndex == Quiz.Questions.Count-1)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите завершить тест?", "Окончить тест", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string message = $"Правильных ответов: {5}\nНеправильных ответов: {5}";
                    MessageBox.Show(message, "Результаты теста", MessageBoxButton.OK, MessageBoxImage.Information);

                    View.MainWindow main = new View.MainWindow();
                    main.Show();
                    Application.Current.Windows[0].Close();
                    return;
                }
                else
                {
                    return;
                }
            }
            CheckCorrectAnswer();
            CurrentIndex++;
        }
        private void CheckCorrectAnswer()
        {
            //MessageBox.Show(SelectedIndex);
            foreach (var item in Options)
            {
                
            }
        }
        private RelayCommand checkAnswerCommand;
        public ICommand CheckAnswerCommand
        {
            get
            {
                if (checkAnswerCommand == null)
                {
                    checkAnswerCommand = new RelayCommand(param => CheckCorrectAnswer());
                }
                return checkAnswerCommand;
            }
        }

        // Загрузить предыдущий вопрос
        private RelayCommand lastQuestion;
        public RelayCommand LastQuestion
        {
            get
            {
                return lastQuestion ?? new RelayCommand(obj =>
                {
                    LoadLastQuestion();
                });
            }
        }
        private void LoadLastQuestion()
        {
            if(CurrentIndex == 0)
            {
                return;
            }
            CurrentIndex--;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
