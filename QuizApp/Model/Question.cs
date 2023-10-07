using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public List<int> CorrectOptionIndices { get; set; }

        public Question(string text, List<string> options, List<int> correctOptionIndices)
        {
            Text = text;
            Options = options;
            CorrectOptionIndices = correctOptionIndices;
        }

        public Question(string text, List<string> options, int correctOptionIndices)
        {
            Text = text;
            Options = options;
            CorrectOptionIndices = new List<int> { correctOptionIndices };
        }
        public Question()
        {

        }
    }

    public class Option
    {
        public string Text { get; set; }
        //public string ImageUrl { get; set; }
    }
}
