using QuizApp.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizApp.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private int correctAnswers = 0;
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


        private int selectedRadioButtonIndex=-1;
        public int SelectedRadioButtonIndex
        {
            get { return selectedRadioButtonIndex; }
            set
            {
                if (selectedRadioButtonIndex != value)
                {
                    selectedRadioButtonIndex = value;
                    OnPropertyChanged(nameof(SelectedRadioButtonIndex));
                }
            }
        }

        private RelayCommand setSelectedRadioButtonCommand;
        public ICommand SetSelectedRadioButtonCommand
        {
            get
            {
                if (setSelectedRadioButtonCommand == null)
                {
                    setSelectedRadioButtonCommand = new RelayCommand(SetSelectedRadioButton);
                }
                return setSelectedRadioButtonCommand;
            }
        }

        private void SetSelectedRadioButton(object parameter)
        {
            var radioButton = parameter as RadioButton;
            if (radioButton != null)
            {
                int selectedIndex = Options.IndexOf(radioButton.Content.ToString());
                SelectedRadioButtonIndex = selectedIndex;
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
            CheckCorrectAnswer();
            if (CurrentIndex == Quiz.Questions.Count-1)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите завершить тест?", "Окончить тест", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string message = $"Правильных ответов: {correctAnswers}\nНеправильных ответов: {Questions.Count-correctAnswers}";
                    MessageBox.Show(message, "Результаты теста", MessageBoxButton.OK, MessageBoxImage.Information);

                    View.MainWindow mainWindow = new View.MainWindow();
                    Application.Current.Windows[0].Close();
                    mainWindow.Show();
                    return;
                }
                else
                {
                    return;
                }
            }
            CurrentIndex++;
        }
        private void CheckCorrectAnswer()
        {
            if(SelectedRadioButtonIndex == -1)
            {
                MessageBox.Show("Выберите вариант ответа");
                return;
            }
            if(SelectedRadioButtonIndex+1 == Questions[CurrentIndex].CorrectOptionIndices[0])
            {
                correctAnswers++;
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
