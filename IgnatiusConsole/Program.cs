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
            Console.ForegroundColor = ConsoleColor.Yellow;
            MainMenu();
            // Main Menu 
            IgnatiusBanner();

        }

        /// Get Data Methods
        public static List<QuizQuestion> GetData()
        {
            List<QuizQuestion> QuestionList = new List<QuizQuestion>();

            using (StreamReader reader = new StreamReader("./quiz.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split('|');

                    string Question = parts[0];


                    string Subject = parts[1];
                    string A = parts[2];
                    string B = parts[3];
                    string C = parts[4];

                    string CorrectAnswer = parts[5];

                    QuizQuestion Q = new QuizQuestion(Question, Subject, A, B, C, CorrectAnswer);

                    QuestionList.Add(Q);

                }
                return QuestionList;
            }
        }

        private static List<string> GetQuizTopics(QuizQuestion[] quizQuestions)
        {
            List<string> topicList = quizQuestions
                .Select(q => q.Subject.Trim())
                .Distinct()
                .ToList();

            return topicList;
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
            //Console.WriteLine("3) Quiz Topics");
            Console.WriteLine("3) Instructions");

            Console.WriteLine("4) Exit Program");

            int MenuChoice = int.Parse(Console.ReadLine());
            if (MenuChoice == 1)
            {
                //StartQuiz();
                QuizMenu();
            }

            if (MenuChoice == 2)
            {
                QuestionsMenu();
            }


            //if (MenuChoice == 3)
            //{
            //   QuizTopicsMenu();
            //}


            if (MenuChoice == 3)
            {
                HelpMenu();
            }
               
           
                    
            

            if (MenuChoice == 4)
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
            IgnatiusBanner();
            List<QuizQuestion> QuestionList = GetData(); // Assuming GetData() returns a list of QuizQuestion
            List<string> TopicList = GetQuizTopics(QuestionList.ToArray());
            string[] TopicArray = TopicList.ToArray();
            List<QuizQuestion> quizQuestionSet = new List<QuizQuestion>();
            double PlayerScore = 0; // Define Player Score counter Variable
            double QuestionCounter = 0; // Count have many questions to been shown to player
            QuizQuestion[] qArray = QuestionList.ToArray(); // Convet List to Array 
            // Generate Random Number to use later 
            Random RandQuestion = new Random();
            int R = RandQuestion.Next(0, qArray.Length);

            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("How Many Questions would you like");

            int quizLength = int.Parse(Console.ReadLine());


            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("Please enter the number of the subject you would like to be tested on");
            int tCount = 0; // variable to count the number of topics 

            for (int i = 0; i < TopicArray.Length; i++)
            {
                Console.WriteLine("[" + tCount + "]" + " " + TopicArray[i]);
                tCount++;
            }

            int topicChoice = int.Parse(Console.ReadLine());



            // for loop to read in subject specfic questions 
            for (int i = 0; i < qArray.Length; i++)
            {
                if (qArray[i].Subject == TopicArray[topicChoice])
                {
                    quizQuestionSet.Add(qArray[i]);
                }
            }
            // create quiz array 
            QuizQuestion[] QuizArray = quizQuestionSet.ToArray();
            if (QuestionCounter + 1 <= quizLength)
            {

                for (int i = 0; i < quizLength; i++)
                {
                    Console.Clear();
                    IgnatiusBanner();
                    R = RandQuestion.Next(0, QuizArray.Length);


                    double questionDisplayCount = QuestionCounter + 1;
                    int QuestionAmountShown = quizLength;
                    Console.WriteLine("Question " + questionDisplayCount + " of " + QuestionAmountShown);
                    // return question at array postion R 
                    Console.WriteLine(QuizArray[R].Question);
                    Console.WriteLine("   1) " + QuizArray[R].OptionONE);
                    Console.WriteLine("   2) " + QuizArray[R].OptionTWO);
                    Console.WriteLine("   3) " + QuizArray[R].OptionTHREE);

                    String UserAnswer = Console.ReadLine().ToUpper();

                    if (UserAnswer == QuizArray[R].CorrectAnswer)
                    {
                        Console.WriteLine("Correct");
                        PlayerScore++; // Increase score if answer is Correct 
                        QuestionCounter++; // Increase Question Counter 
                        if (QuestionCounter <= quizLength - 1)
                        {
                            Console.WriteLine("Press Enter For the next Question");
                        }

                        if (QuestionCounter == quizLength)
                        {
                            Console.WriteLine("Press Enter For Quiz Results");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry The Answer is Incorrect");
                        Console.WriteLine("The Correct Answer was: " + QuizArray[R].CorrectAnswer);
                        QuestionCounter++; // Increase Question Counter 
                        if (QuestionCounter <= quizLength - 1)
                        {
                            Console.WriteLine("Press Enter For the next Question");
                        }

                        if (QuestionCounter == quizLength)
                        {
                            Console.WriteLine("Press Enter For Quiz Results");
                        }
                    }

                    UserAnswer = Console.ReadLine();
                    if (QuestionCounter == quizLength - 1)
                    {
                        Console.WriteLine("Press Enter For the next Question");
                    }

                }


                // Display Player Score when # is entered 
                Console.Clear();
                IgnatiusBanner();
                double percentage = PlayerScore / QuestionCounter * 100;
                Console.WriteLine("You have Answered " + PlayerScore + " out of " + QuestionCounter + " Questions Correctly");
                Console.WriteLine("Overall Percentage: {0:0.00} Percent", percentage);

                Console.WriteLine("Type exit to close program");
                Console.WriteLine("Type menu to return to the main Menu");

                string QuizEndDecision = Console.ReadLine();

                if (QuizEndDecision == "menu")
                {
                    MainMenu();

                }

                if (QuizEndDecision == "exit")
                {
                    System.Environment.Exit(0);
                }
                else // Close program if user enters anything other than "menu" OR "exit" 
                {
                    System.Environment.Exit(0);
                }
            }
        }
        private static void QuestionsMenu()
        {
            Console.Clear();
            IgnatiusBanner();
          
            
            Console.WriteLine("Please Enter the Number Of The Desired Option");
            Console.WriteLine("1) Add Question");
            Console.WriteLine("2) Show All Questions");
            //Console.WriteLine("3) Delete Questions");
            Console.WriteLine("3) Return To Main Menu");
            int menuChoice = int.Parse(Console.ReadLine());
            if (menuChoice == 1)
            {
                AddQuestion();
            }
            if(menuChoice== 2)
            {
                ShowQuestions();
            }

            if(menuChoice== 3)
            {
                MainMenu();                
            }

        }

        private static void QuizTopicsMenu()
        {
            Console.Clear();
            IgnatiusBanner();

            Console.WriteLine("Please Enter the Number Of The Desired Option");
            Console.WriteLine("1) Show Availiable Quiz Topics");
            Console.WriteLine("2) Add New Quiz Topic");
        
            Console.WriteLine("3) Return To Main Menu");
            int menuChoice = int.Parse(Console.ReadLine());
            if (menuChoice == 1)
            {
                ShowQuizTopics();
            }
            if (menuChoice == 2)
            {
              AddQuizTopic();
            }

            if (menuChoice == 3)
            {
                MainMenu();
            }
        }
        private static void AllQuestionsOnSubject()
        {
            Console.Clear();
            IgnatiusBanner();
            List<QuizQuestion> QuestionList = GetData(); // Assuming GetData() returns a list of QuizQuestion
            List<string> TopicList = GetQuizTopics(QuestionList.ToArray());
            string[] TopicArray = TopicList.ToArray();

            double PlayerScore = 0;
            double QuestionCounter = 0;
            Console.Clear();
            IgnatiusBanner();

            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("Please enter the number of the subject you would like to be tested on");

            for (int i = 0; i < TopicArray.Length; i++)
            {
                Console.WriteLine($"[{i}] {TopicArray[i]}");
            }

            int topicChoice;
            while (!int.TryParse(Console.ReadLine(), out topicChoice) || topicChoice < 0 || topicChoice >= TopicArray.Length)
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }

            List<QuizQuestion> quizQuestionSet = QuestionList.Where(q => q.Subject.Trim() == TopicArray[topicChoice]).ToList();

            int quizLength = quizQuestionSet.Count;

            for (int i = 0; i < quizLength; i++)
            {
                Console.Clear();
                IgnatiusBanner();

                double questionDisplayCount = QuestionCounter + 1;
                int QuestionAmountShown = quizLength;

                Console.WriteLine($"Question {questionDisplayCount} of {QuestionAmountShown}");
                Console.WriteLine(quizQuestionSet[i].Question);
                Console.WriteLine($"   A) {quizQuestionSet[i].OptionONE}");
                Console.WriteLine($"   B) {quizQuestionSet[i].OptionTWO}");
                Console.WriteLine($"   C) {quizQuestionSet[i].OptionTHREE}");

                string userAnswer = Console.ReadLine()?.ToUpper();

                if (userAnswer == quizQuestionSet[i].CorrectAnswer)
                {
                    Console.WriteLine("Correct");
                    PlayerScore++;
                }
                else
                {
                    Console.WriteLine($"Sorry, the answer is incorrect. The correct answer was: {quizQuestionSet[i].CorrectAnswer}");
                }

                QuestionCounter++;

                if (QuestionCounter < quizLength)
                {
                    Console.WriteLine("Press Enter for the next question");
                    Console.ReadLine(); // Wait for Enter key
                }
            }

            Console.Clear();
            IgnatiusBanner();
            double percentage = PlayerScore / QuestionCounter * 100;
            Console.WriteLine($"You have answered {PlayerScore} out of {QuestionCounter} questions correctly");
            Console.WriteLine($"Overall Percentage: {percentage:0.00} Percent");

            Console.WriteLine("Type 'menu' to return to the main Menu");
            Console.WriteLine("Type 'exit' to close the program");

            string quizEndDecision = Console.ReadLine();

            if (quizEndDecision?.ToLower() == "menu")
            {
                MainMenu();
            }
            else if (quizEndDecision?.ToLower() == "exit")
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(0);
            }
        }





        private static void QuizOnEverything() // Quiz Method
        {
            Console.Clear();
            IgnatiusBanner();

            List<QuizQuestion> QuestionList = GetData(); // Take data from Second Method 
            double PlayerScore = 0; // Define Player Score counter Variable
            double QuestionCounter = 0; // Count have many questions to been shown to player

            QuizQuestion[] qArray = QuestionList.ToArray(); // Convet List to Array 


            int quizLength = qArray.Length;
            if (QuestionCounter + 1 <= quizLength)
            {

                for (int i = 0; i < quizLength; i++)
                {
                    Console.Clear();
                    IgnatiusBanner();

                    double questionDisplayCount = QuestionCounter + 1;
                    int QuestionAmountShown = quizLength;
                    Console.WriteLine("Question " + questionDisplayCount + " of " + QuestionAmountShown);
                    // return question at array postion i 
                    Console.WriteLine(qArray[i].Question);
                    Console.WriteLine("   1): " + qArray[i].OptionONE);
                    Console.WriteLine("   2) " + qArray[i].OptionTWO);
                    Console.WriteLine("   3) " + qArray[i].OptionTHREE);

                    String UserAnswer = Console.ReadLine().ToUpper();

                    if (UserAnswer == qArray[i].CorrectAnswer)
                    {
                        Console.WriteLine("Correct");
                        PlayerScore++; // Increase score if answer is Correct 
                        QuestionCounter++; // Increase Question Counter 
                        if (QuestionCounter <= quizLength - 1)
                        {
                            Console.WriteLine("Press Enter For the next Question");
                        }

                        if (QuestionCounter == quizLength)
                        {
                            Console.WriteLine("Press Enter For Quiz Results");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry The Answer is Incorrect");
                        Console.WriteLine("The Correct Answer was: " + qArray[i].CorrectAnswer);
                        QuestionCounter++; // Increase Question Counter 
                        if (QuestionCounter <= quizLength - 1)
                        {
                            Console.WriteLine("Press Enter For the next Question");
                        }

                        if (QuestionCounter == quizLength)
                        {
                            Console.WriteLine("Press Enter For Quiz Results");
                        }
                    }

                    UserAnswer = Console.ReadLine();


                }

                // Display Player Score when # is entered 
                Console.Clear();
                IgnatiusBanner();

                double percentage = PlayerScore / QuestionCounter * 100;
                Console.WriteLine("You have Answered " + PlayerScore + " out of " + QuestionCounter + " Questions Correctly");
                Console.WriteLine("Overall Percentage: {0:0.00} Percent", percentage);

                Console.WriteLine("Type exit to close program");
                Console.WriteLine("Type menu to return to the main Menu");

                string QuizEndDecision = Console.ReadLine();

                if (QuizEndDecision == "menu")
                {
                    MainMenu();

                }

                if (QuizEndDecision == "exit")
                {
                    System.Environment.Exit(0);
                }
                else // Close program if user enters anything other than "menu" OR "exit" 
                {
                    System.Environment.Exit(0);
                }

            }
        }
        private static void AddQuestion() // Add Question Method 
        {
            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("Please Enter Question");
            string q = Console.ReadLine().Trim();
            Console.WriteLine("Which Paper is the Question for");


            String qSubject = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine("Enter Multiple Choice Option A ");
            String A = Console.ReadLine().Trim();
            Console.WriteLine("Enter Multiple Choice Option B");
            String B = Console.ReadLine().Trim();
            Console.WriteLine("Enter Multiple Choice Option C ");
            String C = Console.ReadLine().Trim();
            Console.WriteLine("Enter option Number of Correct Answer ");
            String CorrectANS = Console.ReadLine().Trim().ToUpper();

            // write to console application quiz bank 
            using (StreamWriter writer = File.AppendText("quiz.txt"))
            {
                string QuestionToADD = (q + "|" + qSubject + "|" + A + "|" + B + "|" + C + "|" + CorrectANS);

                writer.WriteLine(QuestionToADD);
            }
            // create new variable to store SqlCorrectAnswer String 
            string SqlCorrectAnswer = string.Empty;
            // update correct answer string to string value of the answer 
            if (CorrectANS == "1")
            {
                SqlCorrectAnswer = A;
            }

          else  if (CorrectANS == "2")
            {
                SqlCorrectAnswer = B;
            }
            else if (CorrectANS == "3")
            {
                SqlCorrectAnswer = C;
            }
        
            /* check if SQLQuestions text file exists and if not create and writeSql query to file
            / before questions
            */

         

            // write sql query data to file 
            using (StreamWriter writer = File.AppendText("SQLQuestions.txt"))
            {
                string SQLQuestionToADD = "('" +
                                       q.Replace("'", "''") + "','" +
                                       qSubject.Replace("'", "''") + "','" +
                                       A.Replace("'", "''") + "','" +
                                       B.Replace("'", "''") + "','" +
                                       C.Replace("'", "''") + "','" +
                                      SqlCorrectAnswer + "'), \n\n";

                writer.WriteLine(SQLQuestionToADD);
            }

            ContinueAddingQuestionsDialog();

        }

        private static void ContinueAddingQuestionsDialog()
        {
            Console.WriteLine("Press enter to add another question");
            Console.WriteLine("Type return to stop adding questions and return to main menu");


            string MenuChoice = Console.ReadLine().Trim().ToLower();

            if (MenuChoice == "")
            {
                AddQuestion();
            }

            if (MenuChoice == "return")
            {
                MainMenu();
            }
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

        private static void AddQuizTopic()
        {
            Console.Clear();
            IgnatiusBanner();

            Console.WriteLine("Add New Quiz Topic");
            Console.WriteLine("The following Topics are currently availiable");

            int tCount = 0; // variable to count the number of topics 
            List<QuizQuestion> QuestionList = GetData(); // Assuming GetData() returns a list of QuizQuestion
            List<string> TopicList = GetQuizTopics(QuestionList.ToArray());
            string[] TopicArray = TopicList.ToArray();
            for (int i = 0; i < TopicArray.Length; i++)
            {
                Console.WriteLine("[" + tCount + "]" + " " + TopicArray[i]);
                tCount++;
            }
            // blank line before prompt
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Please enter the quiz Topic you wish to add to the quiz bank");

            string AddTopic = Console.ReadLine();
           
            using (StreamWriter writer = File.AppendText("quizTopics.txt"))
            {
                writer.WriteLine(AddTopic);
            }

            Console.WriteLine("The following topic has been added: " + AddTopic);
            Console.WriteLine("Press enter for the main menu ");
            Console.ReadLine();
            QuestionsMenu();

        }

        private static void ShowQuizTopics()
        {
            Console.Clear();
            IgnatiusBanner();
            Console.WriteLine("The following Topics are currently availiable");

            int tCount = 0; // variable to count the number of topics 
            List<QuizQuestion> QuestionList = GetData(); // Assuming GetData() returns a list of QuizQuestion
            List<string> TopicList = GetQuizTopics(QuestionList.ToArray());
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
            string QuizEndDecision = Console.ReadLine();

            if (QuizEndDecision == "menu")
            {
                QuizTopicsMenu();

            }

            else
            {
                QuizTopicsMenu();
            }
        }
        private static void HelpMenu()
        {
            Console.Clear();
            IgnatiusBanner();
            string helpText = @"*** Help Menu ***
Important points to remember 
1) The Quiz topic must exist in the quizTopics.txt file in order to have option of being quizzed on it.
2) when you add a question ensure that the Subject feild Matches the subject in the quizTopics.txt file exactly
   if needed use the ShowQuizTopic command before the AddQuestion Command.
3) if there are no questions in the quiz bank the application will close when quiz is started";

            Console.WriteLine(helpText);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   Type menu to return to the main Menu");

            string QuizEndDecision = Console.ReadLine();

            if (QuizEndDecision == "menu")
            {
                MainMenu();

            }

            else
            {
                MainMenu();
            }
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



