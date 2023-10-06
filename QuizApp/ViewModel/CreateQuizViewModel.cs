using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.IO;
using System.Collections.ObjectModel;

namespace QuizApp.ViewModel
{
    internal class CreateQuizViewModel:INotifyPropertyChanged
    {
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

        private string questionText;
        public string QuestionText
        {
            get { return questionText; }
            set
            {
                if (questionText != value)
                {
                    questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        public ObservableCollection<OptionControl> Options { get; set; }
        #endregion

        private RelayCommand addNewOption;
        public RelayCommand AddNewOption
        {
            get
            {
                return addNewOption ?? new RelayCommand(obj =>
                {
                    AddNewOptionMethod();
                });
            }
        }

        private void CreateNewAnswerField()
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;

            CheckBox indexLabel = new CheckBox();
            //indexLabel.Content = answersStackPanel.Children.Count + 1;

            TextBox newAnswerTextBox = new TextBox();
            newAnswerTextBox.Width = 300;
            newAnswerTextBox.Margin = new Thickness(5);
            newAnswerTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            newAnswerTextBox.FontSize = 18;

            Button addImage = new Button();
            addImage.Content = "Добавить изображение";

            newStackPanel.Children.Add(indexLabel);
            newStackPanel.Children.Add(newAnswerTextBox);
            newStackPanel.Children.Add(addImage);

            //answersStackPanel.Children.Add(newStackPanel);
        }

        private void AddNewOptionMethod()
        {
            // Проверка на пустые поля
            if (QuestionText == "")
            {
                MessageBox.Show("Введите вопрос");
                return;
            }
            //if (answersStackPanel.Children.Count <= 1)
            //{
            //    MessageBox.Show("Вариантов ответа должно быть больше двух");
            //    return;
            //}

            //List<string> options = new List<string>();
            //List<int> correctOptionIndices = new List<int>();

            //// Добавить варианты ответа
            //foreach (StackPanel stackPanel in answersStackPanel.Children)
            //{
            //    TextBox textBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault();
            //    if (textBox.Text == "")
            //    {
            //        MessageBox.Show("Ответ не может быть пустым");
            //        return;
            //    }
            //    options.Add(textBox.Text);
            //}

            //// Добавить индексы правильных ответов
            //foreach (StackPanel stackPanel in answersStackPanel.Children)
            //{
            //    CheckBox checkBox = stackPanel.Children.OfType<CheckBox>().FirstOrDefault();
            //    if (checkBox.IsChecked == true)
            //    {
            //        correctOptionIndices.Add((int)checkBox.Content);
            //    }
            //}

            //if (correctOptionIndices.Count < 1)
            //{
            //    MessageBox.Show("Правильных вариантов ответа должно быть больше одного");
            //    return;
            //}

            //questions.Add(new Question(questionText.Text, options, correctOptionIndices));

            //ClearQuestionFields();
        }

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
            // Проверка на пустые поля

            //TODO Проверка на пустые поля

            //Quiz newQuiz = new Quiz(quizText.Text, questions, (bool)canSkipBox.IsChecked);

            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Текстовые файлы (*.json)|*.json";

            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    string filePath = saveFileDialog.FileName;
            //    string quiz = JsonSerializer.Serialize(newQuiz, new JsonSerializerOptions
            //    {
            //        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //        WriteIndented = true
            //    });
            //    File.WriteAllText(filePath, quiz, Encoding.UTF8);
            //    MessageBox.Show("Файл сохранен");
            //}
        }
    }
}
