using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<RadioButtonViewModel> _questionRadioButtons;
        public ObservableCollection<RadioButtonViewModel> QuestionRadioButtons
        {
            get { return _questionRadioButtons; }
            set
            {
                if (_questionRadioButtons != value)
                {
                    _questionRadioButtons = value;
                    OnPropertyChanged(nameof(QuestionRadioButtons));
                }
            }
        }

        private void LoadQuizFromJson()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Текстовые файлы (*.json)|*.json";

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //string filePath = openFileDialog.FileName;
            //    //string json = File.ReadAllText(filePath);
            //    //Quiz loadedQuiz = JsonSerializer.Deserialize<Quiz>(json);
            //    //QuizForm quizForm = new QuizForm(loadedQuiz);
            //    //quizForm.Show();
            //    //Close();
            //}
        }
    }
}
