using QuizApp.Model;
using System.ComponentModel;
using System.Windows;

namespace QuizApp.ViewModel
{
    public class OptionControlViewModel
    {
        private AnswerOption model;

        public OptionControlViewModel()
        {
            model = new AnswerOption();
        }

        public AnswerOption AnswerOption
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(AnswerOption));
            }
        }


        public bool IsCorrect
        {
            get { return AnswerOption.IsCorrect; }
            set
            {
                if (AnswerOption.IsCorrect != value)
                {
                    AnswerOption.IsCorrect = value;
                    OnPropertyChanged(nameof(IsCorrect));
                }
            }
        }

        public string Index
        {
            get { return AnswerOption.Index; }
            set
            {
                if (AnswerOption.Index != value)
                {
                    AnswerOption.Index = value;
                    OnPropertyChanged(nameof(Index));
                }
            }
        }

        public string Answer
        {
            get { return AnswerOption.Answer; }
            set
            {
                if (AnswerOption.Answer != value)
                {
                    AnswerOption.Answer = value;
                    OnPropertyChanged(nameof(Answer));
                }
            }
        }

        private RelayCommand addImage;
        public RelayCommand AddImage
        {
            get
            {
                return addImage ?? new RelayCommand(obj =>
                {
                    AddImageMethod();
                });
            }
        }

        private void AddImageMethod()
        {
            MessageBox.Show("AddImage");
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
