using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnatiusConsole
{
    public class QuizTopic
    {
        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public QuizTopic(string subject)
        {
            Subject = subject;
        }
    }
}
