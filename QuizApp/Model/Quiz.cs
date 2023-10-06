using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public bool CanSkipQuestion { get; set; }

        public Quiz(string name, List<Question> questions, bool canSkipQuestion)
        {
            Name = name;
            Questions = questions;
            CanSkipQuestion = canSkipQuestion;
        }
    }
}
