using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;


namespace IgnatiusConsole
{
    public class Program
    {

        static void Main(string[] args)
        {

            MainMenu();
            // Main Menu 
            IgnatiusBanner();

        }

        /// Get Data Methods
        public static List<QuizQuestion> GetData()
        {
            List<QuizQuestion> questionList = new List<QuizQuestion>();

            string questionPath = @"C:\Github\IgnatiusQuestionsCSV\Questions.csv";

            using (StreamReader reader = new StreamReader(questionPath))
            {
                // Skip header
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');

                    if (parts.Length < 7)
                        continue;

                    QuizQuestion q = new QuizQuestion(
                        parts[1], // Question
                        parts[2], // Subject
                        parts[3], // Option1
                        parts[4], // Option2
                        parts[5], // Option3
                        parts[6]  // Answer
                    );

                    questionList.Add(q);
                }
            }

            return questionList;
        }
        private static List<string> GetQuizTopics()
        {
            List<string> topics = new List<string>();

            string subjectPath = @"C:\Github\IgnatiusQuestionsCSV\Subjects.csv";

            using (StreamReader reader = new StreamReader(subjectPath))
            {
                reader.ReadLine(); // Skip header

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');

                    if (parts.Length >= 2)
                        topics.Add(parts[1]);
                }
            }

            return topics
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }


        private static void MainMenu() // Main Menu Method 
        {

            Console.Clear(); // Clear Screen wihen showing menu 
                             // Main Menu 
            IgnatiusBanner();
            // Console.WriteLine("Welcome To the Quiz Room");
            Console.WriteLine(" Please Enter the Number of the task you want to perform");
            Console.WriteLine("1) Start Quiz");
            Console.WriteLine("2) Questions");
           
            Console.WriteLine("3) Exit Program");

            int MenuChoice = int.Parse(Console.ReadLine());
            if (MenuChoice == 1)
            {
                //StartQuiz();
                QuizMenu();
            }

          if (MenuChoice == 2 )
            {
                ShowQuestions();
            }


          
            

            if (MenuChoice == 3)
            {
                ExitProgram();
            }
        

        }

        private static void QuizMenu()
        {
            Console.Clear();
            IgnatiusBanner();

            Console.WriteLine("Please Enter the Number Of The Desired Option");
            Console.WriteLine();
            Console.WriteLine("1) All Questions,All Subjects");
            Console.WriteLine("2) All Questions on Your choice of Subject");
            Console.WriteLine("3) Custom Quiz Mode");

            Console.WriteLine("4) Return To The Main Menu");

            int MenuDecision = int.Parse(Console.ReadLine());

            if (MenuDecision == 1)
            {
                QuizOnEverything();
            }

            if (MenuDecision == 2)
            {
                AllQuestionsOnSubject();
            }
            if (MenuDecision == 3)
            {
                RandomQuestionsOnSubject();
            }



            if (MenuDecision == 4)
            {
                MainMenu();
            }

        }

        private static void RandomQuestionsOnSubject()
        {
            Console.Clear();
            IgnatiusBanner();

            List<QuizQuestion> questionList = GetData();
            List<string> topicList = GetQuizTopics();

            Console.WriteLine("How many questions would you like?");
            Console.Write("Amount: ");

            if (!int.TryParse(Console.ReadLine(), out int quizLength))
                return;


            Console.Clear();
            IgnatiusBanner();

            Console.WriteLine("Select a Subject");
            Console.WriteLine();

            for (int i = 0; i < topicList.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {topicList[i]}");
            }

            Console.WriteLine();
            Console.Write("Choice: ");

            if (!int.TryParse(Console.ReadLine(), out int topicChoice))
                return;

            if (topicChoice < 1 || topicChoice > topicList.Count)
                return;


            string selectedSubject = topicList[topicChoice - 1];


            List<QuizQuestion> quizQuestions = questionList
                .Where(q => q.Subject.Equals(
                    selectedSubject,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();


            if (quizQuestions.Count == 0)
            {
                Console.WriteLine("No questions found for this subject.");
                Console.ReadLine();
                return;
            }


            Random random = new Random();

            quizQuestions = quizQuestions
                .OrderBy(x => random.Next())
                .Take(Math.Min(quizLength, quizQuestions.Count))
                .ToList();


            double playerScore = 0;
            double questionCounter = 0;


            foreach (QuizQuestion question in quizQuestions)
            {
                Console.Clear();
                IgnatiusBanner();

                Console.WriteLine($"Question {questionCounter + 1} of {quizQuestions.Count}");
                Console.WriteLine();

                Console.WriteLine(question.Question);
                Console.WriteLine($"1) {question.OptionONE}");
                Console.WriteLine($"2) {question.OptionTWO}");
                Console.WriteLine($"3) {question.OptionTHREE}");
                Console.WriteLine();


                Console.Write("Answer: ");

                string choice = Console.ReadLine();


                string userAnswer = "";

                switch (choice)
                {
                    case "1":
                        userAnswer = question.OptionONE;
                        break;

                    case "2":
                        userAnswer = question.OptionTWO;
                        break;

                    case "3":
                        userAnswer = question.OptionTHREE;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadLine();
                        continue;
                }


                if (userAnswer.Trim().Equals(
                    question.CorrectAnswer.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    Console.WriteLine("✅ Correct!");
                    playerScore++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("❌ Incorrect.");
                    Console.WriteLine("Correct Answer: " + question.CorrectAnswer);
                }


                questionCounter++;

                Console.WriteLine();
                Console.WriteLine("Press Enter for next question...");
                Console.ReadLine();
            }


            Console.Clear();
            IgnatiusBanner();


            double percentage = (playerScore / questionCounter) * 100;


            Console.WriteLine(
                $"You answered {playerScore} out of {questionCounter} questions correctly.");

            Console.WriteLine(
                $"Overall Percentage: {percentage:0.00}%");


            Console.WriteLine();
            Console.WriteLine("Type menu to return to Main Menu");
            Console.WriteLine("Type exit to close program");


            string quizEndDecision = Console.ReadLine().ToLower();


            if (quizEndDecision == "menu")
            {
                MainMenu();
            }

            Environment.Exit(0);
        }
        private static void AllQuestionsOnSubject()
        {
            Console.Clear();
            IgnatiusBanner();

            List<QuizQuestion> questionList = GetData();
            List<string> topicList = GetQuizTopics();

            Console.WriteLine("Select a Subject");
            Console.WriteLine();

            for (int i = 0; i < topicList.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {topicList[i]}");
            }

            Console.WriteLine();
            Console.Write("Choice: ");

            if (!int.TryParse(Console.ReadLine(), out int topicChoice))
                return;

            if (topicChoice < 1 || topicChoice > topicList.Count)
                return;

            string selectedSubject = topicList[topicChoice - 1];

            List<QuizQuestion> quizQuestions = questionList
                .Where(q => q.Subject.Equals(selectedSubject, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (quizQuestions.Count == 0)
            {
                Console.WriteLine("No questions found for this subject.");
                Console.ReadLine();
                return;
            }

            double playerScore = 0;
            double questionCounter = 0;

            for (int i = 0; i < quizQuestions.Count; i++)
            {
                Console.Clear();
                IgnatiusBanner();

                Console.WriteLine($"Question {i + 1} of {quizQuestions.Count}");
                Console.WriteLine();

                Console.WriteLine(quizQuestions[i].Question);
                Console.WriteLine($"1) {quizQuestions[i].OptionONE}");
                Console.WriteLine($"2) {quizQuestions[i].OptionTWO}");
                Console.WriteLine($"3) {quizQuestions[i].OptionTHREE}");
                Console.WriteLine();

                Console.Write("Answer: ");
                string choice = Console.ReadLine();

                string userAnswer = "";

                switch (choice)
                {
                    case "1":
                        userAnswer = quizQuestions[i].OptionONE;
                        break;

                    case "2":
                        userAnswer = quizQuestions[i].OptionTWO;
                        break;

                    case "3":
                        userAnswer = quizQuestions[i].OptionTHREE;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadLine();
                        i--;
                        continue;
                }

                if (userAnswer.Trim().Equals(
                    quizQuestions[i].CorrectAnswer.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    Console.WriteLine("✅ Correct!");
                    playerScore++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("❌ Incorrect.");
                    Console.WriteLine("Correct Answer: " + quizQuestions[i].CorrectAnswer);
                }

                questionCounter++;

                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            Console.Clear();
            IgnatiusBanner();

            double percentage = (playerScore / questionCounter) * 100;

            Console.WriteLine($"You answered {playerScore} out of {questionCounter} correctly.");
            Console.WriteLine($"Overall Percentage: {percentage:0.00}%");
            Console.WriteLine();

            Console.WriteLine("Type 'menu' to return to the Main Menu");
            Console.WriteLine("Type 'exit' to quit");

            string decision = Console.ReadLine().ToLower();

            if (decision == "menu")
            {
                MainMenu();
            }

            Environment.Exit(0);
        }





        private static void QuizOnEverything()
        {
            Console.Clear();
            IgnatiusBanner();

            List<QuizQuestion> questionList = GetData();

            double playerScore = 0;
            double questionCounter = 0;

            QuizQuestion[] qArray = questionList.ToArray();

            int quizLength = qArray.Length;

            for (int i = 0; i < quizLength; i++)
            {
                Console.Clear();
                IgnatiusBanner();

                Console.WriteLine($"Question {i + 1} of {quizLength}");
                Console.WriteLine();

                Console.WriteLine(qArray[i].Question);
                Console.WriteLine("1) " + qArray[i].OptionONE);
                Console.WriteLine("2) " + qArray[i].OptionTWO);
                Console.WriteLine("3) " + qArray[i].OptionTHREE);
                Console.WriteLine();

                Console.Write("Answer: ");
                string choice = Console.ReadLine();

                string userAnswer = "";

                switch (choice)
                {
                    case "1":
                        userAnswer = qArray[i].OptionONE;
                        break;

                    case "2":
                        userAnswer = qArray[i].OptionTWO;
                        break;

                    case "3":
                        userAnswer = qArray[i].OptionTHREE;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadLine();
                        i--;
                        continue;
                }

                if (userAnswer.Trim().Equals(
                    qArray[i].CorrectAnswer.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    Console.WriteLine("✅ Correct!");
                    playerScore++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("❌ Incorrect.");
                    Console.WriteLine("Correct Answer: " + qArray[i].CorrectAnswer);
                }

                questionCounter++;

                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            Console.Clear();
            IgnatiusBanner();

            double percentage = (playerScore / questionCounter) * 100;

            Console.WriteLine($"You answered {playerScore} out of {questionCounter} correctly.");
            Console.WriteLine($"Overall Percentage: {percentage:0.00}%");
            Console.WriteLine();

            Console.WriteLine("Type 'menu' for Main Menu");
            Console.WriteLine("Type 'exit' to quit");

            string decision = Console.ReadLine().ToLower();

            if (decision == "menu")
                MainMenu();

            Environment.Exit(0);
        }
       

        private static void ShowQuestions()
        {
            Console.Clear();
            IgnatiusBanner();
            List<QuizQuestion> QuestionList = GetData(); // Read in Data
            Console.WriteLine("The Following Questions are in the quiz bank: ");
            // Insert 2 lines before returning questions 
            Console.WriteLine();
            Console.WriteLine();
            int QCount = 0;
            foreach (QuizQuestion question in QuestionList)
            {
                // QCount++;
                Console.WriteLine("[" + QCount + "]" + " " + question.Question + " (" + question.Subject + ")");
                QCount++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("");
            Console.ReadLine(); // pause for user input 

            MainMenu();

        }

       

        private static void ShowQuizTopics()
        {
            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("The following Topics are currently availiable");

            int tCount = 0; // variable to count the number of topics 
            List<string> TopicList = GetQuizTopics();
            string[] TopicArray = TopicList.ToArray();
            for (int i = 0; i < TopicArray.Length; i++)
            {
                Console.WriteLine("[" + tCount + "]" + " " + TopicArray[i]);
                tCount++;
            }
            // blank line before prompt
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Press enter to return to the main menu");
           
        }
      
        private static void ExitProgram()
        {
            System.Environment.Exit(0);
        }



        public static void IgnatiusBanner() // Banner Method 
        {
            // Define Variale to store banner 
            String Banner = @"
  _____                  _   _           
 |_   _|                | | (_)          
   | |  __ _ _ __   __ _| |_ _ _   _ ___ 
   | | / _` | '_ \ / _` | __| | | | / __|
  _| || (_| | | | | (_| | |_| | |_| \__ \
 |_____\__, |_| |_|\__,_|\__|_|\__,_|___/
        __/ |                            
       |___/  ";
            Console.WriteLine(Banner); // Print to screen 

            // Blank lines after printing ignatius banner 
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}



