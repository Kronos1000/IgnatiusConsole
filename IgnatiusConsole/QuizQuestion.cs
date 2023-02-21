using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnatiusConsole
{
    public class QuizQuestion
    {

        private int arraycount;

        public int ArrayCount
        {
            get { return arraycount; }
            set { arraycount = value; }
        }

        private string question;

        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        private string optionONE;

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        public string OptionONE
        {
            get { return optionONE; }
            set { optionONE = value; }
        }

        private string optionTWO;

        public string OptionTWO
        {
            get { return optionTWO; }
            set { optionTWO = value; }
        }


        private string optionTHREE;

        public string OptionTHREE
        {
            get { return optionTHREE; }
            set { optionTHREE = value; }
        }


        private string correctAnswer;

        public string CorrectAnswer
        {
            get { return correctAnswer; }
            set { correctAnswer = value; }
        }

        public QuizQuestion(string question, string subject, string optionONE, string optionTWO, string optionTHREE, string correctAnswer)
        {
            Question = question;
            Subject = subject;
            OptionONE = optionONE;
            OptionTWO = optionTWO;
            OptionTHREE = optionTHREE;
            CorrectAnswer = correctAnswer;
        }




    }
}
