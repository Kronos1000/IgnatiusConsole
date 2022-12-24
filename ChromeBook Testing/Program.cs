using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
 

namespace QuizME
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

   

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

     private string  optionONE;
        public string  OptionONE
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

        static void Main(string[] args)
        {

            MainMenu();
           // GetData();
            // Main Menu 
      IgnatiusBanner();

        }


        public static List<QuizQuestion> GetData()
        {
            List<QuizQuestion> QuestionList = new List<QuizQuestion>();

         Console.Writeline("Please Copy data from quiz.txt One line at a Time");
                string line = Console.ReadLine();
                while (line != "#")
                {
                    
                    string[] parts = line.Split('|');

                    string Question = parts[0];
                    string Subject = parts[1];
                    string A = parts[2];
                    string B = parts[3];
                    string C = parts[4];

                    string CorrectAnswer = parts[5];

                    QuizQuestion Q = new QuizQuestion(Question, Subject, A, B, C, CorrectAnswer);
                    QuestionList.Add(Q);
line = Console.ReadLine();
                }

                return QuestionList;
            }

        

        private static void MainMenu() // Main Menu Method 
        {

            Console.Clear(); // Clear Screen wihen showing menu 
                             // Main Menu 
           IgnatiusBanner();
           // Console.WriteLine("Welcome To the Quiz Room");
            Console.WriteLine(" Please Enter the Number of the task you want to perform");
            Console.WriteLine("1. Start Quiz (10 Random Questions)");
            Console.WriteLine("2. Start Quiz (All Questions in the  Quiz Bank)");
            Console.WriteLine("3. Add Question");
            Console.WriteLine("4. Show All Questions");
            Console.WriteLine("5. Exit Program");

            int MenuChoice = int.Parse(Console.ReadLine());
            if (MenuChoice == 1)
            {
                StartQuiz();
            }

            if (MenuChoice == 2)
            {
                QuizOnEverything();
            }
            
            if (MenuChoice == 3 )
            {
                AddQuestion();
            }

            if (MenuChoice == 4)
            {
                ShowQuestions();
            }

            if (MenuChoice == 5)
            {
                ExitProgram();
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
                Console.WriteLine("["+ QCount +"]" + " "+ question.Question);
                QCount++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("");
            Console.ReadLine(); // pause for user input 

            MainMenu();

        }

        private static void AddQuestion() // Add Question Method 
        {
            Console.Clear();
    IgnatiusBanner();
            Console.WriteLine("Please Enter Question");
            string q = Console.ReadLine();
            Console.WriteLine("Enter question Subject");
            String qSubject = Console.ReadLine();
            Console.WriteLine("Enter Multiple Choice Option A ");
            String A = Console.ReadLine();
            Console.WriteLine("Enter Multiple Choice Option B");
            String B = Console.ReadLine();
            Console.WriteLine("Enter Multiple Choice Option C ");
            String C = Console.ReadLine();
            Console.WriteLine("Enter Letter  of Correct Answer ");
            String CorrectANS = Console.ReadLine();

            using (StreamWriter writer = File.AppendText("quizBackup.txt"))
            {
                string QuestionToADD = (q + " | " + qSubject + " | " + A + " | " + B + " | " + C + " | " + CorrectANS);

                writer.WriteLine(QuestionToADD);
            }
            MainMenu();

        }

        private static void StartQuiz() // Quiz Method (10 Random Questions)
        {
  IgnatiusBanner();


            List<QuizQuestion> QuestionList = GetData(); // Take data from Second Method 
            double PlayerScore = 0; // Define Player Score counter Variable
            double QuestionCounter = 0; // Count have many questions to been shown to player

            QuizQuestion[] qArray = QuestionList.ToArray(); // Convet List to Array 

            // Generate Random Number to use later 
            Random RandQuestion = new Random();
            int R = RandQuestion.Next(0, qArray.Length);

            int quizLength = 10;
            if (QuestionCounter + 1 <= quizLength)
            {

                for (int i = 0; i < quizLength; i++)
                {
                    Console.Clear();
                   IgnatiusBanner();
                    R = RandQuestion.Next(0, qArray.Length);
                    R = RandQuestion.Next(0, qArray.Length);
                    double questionDisplayCount = QuestionCounter + 1;
                    int QuestionAmountShown = quizLength;
                    Console.WriteLine("Question " + questionDisplayCount + " of " + QuestionAmountShown);
                    // return question at array postion R 
                    Console.WriteLine(qArray[R].Question);
                    Console.WriteLine("   A: " + qArray[R].OptionONE);
                    Console.WriteLine("   B: " + qArray[R].OptionTWO);
                    Console.WriteLine("   C: " + qArray[R].OptionTHREE);

                    String UserAnswer = Console.ReadLine();
                    UserAnswer = UserAnswer.ToUpper();
                    if (UserAnswer == qArray[R].CorrectAnswer)
                    {
                        Console.WriteLine("Correct");
                        PlayerScore++; // Increase score if answer is Correct 
                        QuestionCounter++; // Increase Question Counter 
                         if (QuestionCounter <= quizLength -1)
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
                        Console.WriteLine("The Correct Answer was: " + qArray[R].CorrectAnswer);
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
                    if(QuestionCounter == quizLength-1)
                    {
                        Console.WriteLine("Press Enter For the next Question");
                    }
                    
                }


                // Display Player Score when # is entered 
                Console.Clear();
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

        private static void QuizOnEverything() // Quiz Method
        {

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
                    
                    double  questionDisplayCount = QuestionCounter + 1;
                    int QuestionAmountShown = quizLength;
                    Console.WriteLine("Question " + questionDisplayCount + " of " + QuestionAmountShown);
                    // return question at array postion i 
                    Console.WriteLine(qArray[i].Question);
                    Console.WriteLine("   A: " + qArray[i].OptionONE);
                    Console.WriteLine("   B: " + qArray[i].OptionTWO);
                    Console.WriteLine("   C: " + qArray[i].OptionTHREE);

                    String UserAnswer = Console.ReadLine().ToUpper();

                    if (UserAnswer == qArray[i].CorrectAnswer)
                    {
                        Console.WriteLine("Correct");
                        PlayerScore++; // Increase score if answer is Correct 
                        QuestionCounter++; // Increase Question Counter 
                        if (QuestionCounter <= quizLength -1)
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
                        if (QuestionCounter <= quizLength -1)
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



    
	public static void DeleteQuestion()
 {
List<QuizQuestion> QuestionList = GetData();
QuizQuestion[] qArray = QuestionList.ToArray(); // Convet List to Array 
ShowQuestions(); // Call ShowQuestions Method
Console.WriteLine("Please Enter The number of the question you wish to delete");  // prompt user for input 
int userInput = int.Parse(Console.ReadLine()); // read in array position 

string qRemove = (qArray[userInput].Question + " | " + qArray[userInput].Subject + " | " +qArray[userInput].OptionONE + " | " + qArray[userInput].OptionTWO + " | " + qArray[userInput].OptionTHREE + " | " + qArray[userInput].CorrectAnswer);
}



        private static void ExitProgram()
        {
            System.Environment.Exit(0);
        }
     
     
     
static void IgnatiusBanner() // Banner Method 
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
       |___/  ";  ///
Console.WriteLine(Banner); // Print to screen 

// Blank lines after printing ignatius banner 
Console.WriteLine(); 
Console.WriteLine();
}

}
  }

