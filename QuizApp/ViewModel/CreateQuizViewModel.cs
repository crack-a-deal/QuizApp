using QuizApp.Model;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.IO;
using System.Text;


namespace QuizApp.ViewModel
{
    internal class CreateQuizViewModel:INotifyPropertyChanged
    {
        private List<Question> questions = new List<Question>();

        private Question questionModel = new Question();
        public Question Question
        {
            get { return questionModel; }
            set
            {
                questionModel = value;
                OnPropertyChanged(nameof(Question));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Properties
        private string quizName;

        public string QuizName
        {
            get { return quizName; }
            set
            {
                if (quizName != value)
                {
                    quizName = value;
                    OnPropertyChanged(nameof(QuizName));
                }
            }
        }

        public string QuestionText
        {
            get { return questionModel.Text; }
            set
            {
                if (questionModel.Text != value)
                {
                    questionModel.Text = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        private ObservableCollection<OptionControlViewModel> optionControlCollection = new ObservableCollection<OptionControlViewModel>();
        public ObservableCollection<OptionControlViewModel> OptionControlCollection
        {
            get { return optionControlCollection; }
            set
            {
                optionControlCollection = value;
                OnPropertyChanged(nameof(OptionControlCollection));
            }
        }
        #endregion

        // Добавление варианта ответа
        private RelayCommand addNewOption;
        public RelayCommand AddNewOption
        {
            get
            {
                return addNewOption ?? new RelayCommand(obj =>
                {
                    AddOptionControl();
                });
            }
        }
        private void AddOptionControl()
        {
            OptionControlViewModel newOption = new OptionControlViewModel();
            newOption.Index=(OptionControlCollection.Count + 1).ToString();

            OptionControlCollection.Add(newOption);
        }

        // Удалить все варианты ответа
        private RelayCommand clearAllOption;
        public RelayCommand ClearAllOption
        {
            get
            {
                return clearAllOption ?? new RelayCommand(obj =>
                {
                    ClearAllOptionControl();
                });
            }
        }
        private void ClearAllOptionControl()
        {
            OptionControlCollection.Clear();
        }


        // Добавление вопроса
        private RelayCommand addQuestion;
        public RelayCommand AddQuestion
        {
            get
            {
                return addQuestion ?? new RelayCommand(obj =>
                {
                    AddQuestionMethod();
                });
            }
        }
        private void AddQuestionMethod()
        {
            // Проверка на пустые поля
            if (QuestionText == "" || QuestionText == null)
            {
                MessageBox.Show("Введите вопрос");
                return;
            }
            if (optionControlCollection.Count <= 1)
            {
                MessageBox.Show("Вариантов ответа должно быть больше двух");
                return;
            }
            List<string> options = new List<string>();
            List<int> correctOptionIndices = new List<int>();
            foreach (var  option in OptionControlCollection)
            {
                //Проверка на пустые поля
                if (option.Answer == "")
                {
                    MessageBox.Show("Ответ не может быть пустым");
                    return;
                }
                options.Add(option.Answer);
                if (option.IsCorrect)
                {
                    correctOptionIndices.Add(Convert.ToInt16(option.Index));
                }
            }

            if (correctOptionIndices.Count < 1)
            {
                MessageBox.Show("Правильных вариантов ответа должно быть больше одного");
                return;
            }

            Question newQuestion = new Question(QuestionText,options,correctOptionIndices);
            questions.Add(newQuestion);
            ClearFiels();
        }

        // Очищает поля для ввода данных
        private void ClearFiels()
        {
            QuestionText = "";
            optionControlCollection.Clear();
        }


        // Сохраняет тест в json файл 
        private RelayCommand saveQuiz;
        public RelayCommand SaveQuiz
        {
            get
            {
                return saveQuiz ?? new RelayCommand(obj =>
                {
                    SaveQuizMethod();
                });
            }
        }
        private void SaveQuizMethod()
        {
            //Проверка на пустые поля

            if(QuizName == "" || QuizName == null)
            {
                MessageBox.Show("Введите название теста");
                return;
            }
            if(questions.Count < 1) {
                MessageBox.Show("Вопросов в тесте должно быть больше одного");
                return;
            }

            Quiz newQuiz = new Quiz(QuizName, questions, true);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string quiz = JsonSerializer.Serialize(newQuiz, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                });
                File.WriteAllText(filePath, quiz, Encoding.UTF8);
                MessageBox.Show("Файл сохранен");
            }

            View.MainWindow main = new View.MainWindow();
            main.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
