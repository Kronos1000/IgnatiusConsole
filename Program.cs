using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace QuizME
{
    public class Program
    {

        static void Main(string[] args)
        {

            MainMenu();
           
        }

  
        public static List<QuizQuestion> GetData()
        {
            List<QuizQuestion> QuestionList = new List<QuizQuestion>();

            using (StreamReader reader = new StreamReader("quiz.txt"))
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

        private static void MainMenu() // Main Menu Method 
        {

            Console.Clear(); // Clear Screen wihen showing menu 
                             // Main Menu 
            Console.WriteLine("Welcome To the Quiz Room");
            Console.WriteLine("Enter the Number of the task you want to perform");
            Console.WriteLine("1. Start Quiz");
            Console.WriteLine("2. Add Question");
            Console.WriteLine("3. Show All Questions");
            Console.WriteLine("4. Exit Program");

            int MenuChoice = int.Parse(Console.ReadLine());
            if (MenuChoice == 1)
            {
                StartQuiz();
            }

            if (MenuChoice == 2)
            {
                AddQuestion();
            }

            if (MenuChoice == 3)
            {
                ShowQuestions();
            }

            if (MenuChoice == 4)
            {
                ExitProgram();
            }
        }

        private static void ShowQuestions()
        {
            Console.Clear();
            List<QuizQuestion> QuestionList = GetData(); // Read in Data
            Console.WriteLine("The Following Questions are in the quiz bank: ");
            // Insert 2 lines before returning questions 
            Console.WriteLine();
            Console.WriteLine();

            foreach (QuizQuestion question in QuestionList)
            {
                Console.WriteLine(question.Question);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Enter To return to the Main Menu");
            Console.ReadLine(); // pause for user input 

            MainMenu();

        }

        private static void AddQuestion() // Add Question Method 
        {
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

            using (StreamWriter writer = File.AppendText("quiz.txt"))
            {
                string QuestionToADD = (q + " | " + qSubject + " | " + A + " | " + B + " | " + C + " | " + CorrectANS);

                writer.WriteLine(QuestionToADD);
            }
            MainMenu();

        }
        private static void StartQuiz() // Quiz Method
        {

            List<QuizQuestion> QuestionList = GetData(); // Take data from Second Method 
            double PlayerScore = 0; // Define Player Score counter Variable
            double QuestionCounter = 0; // Count have many questions to been shown to player

            QuizQuestion[] qArray = QuestionList.ToArray(); // Convet List to Array 

            // Generate Random Number to use later 
            Random RandQuestion = new Random();
            int R = RandQuestion.Next(0, qArray.Length);

            int quizLength = 3;
            if (QuestionCounter + 1 <= quizLength)
            {

                for (int i = 0; i < quizLength; i++)
                {
                    Console.Clear();
                    R = RandQuestion.Next(0, qArray.Length);
                    R = RandQuestion.Next(0, qArray.Length);
                    // return question at array postion R 
                    Console.WriteLine(qArray[R].Question);
                    Console.WriteLine("   A: " + qArray[R].OptionONE);
                    Console.WriteLine("   B: " + qArray[R].OptionTWO);
                    Console.WriteLine("   C: " + qArray[R].OptionTHREE);

                    String UserAnswer = Console.ReadLine();

                    if (UserAnswer == qArray[R].CorrectAnswer)
                    {
                        Console.WriteLine("Correct");
                        PlayerScore++; // Increase score if answer is Correct 
                        QuestionCounter++; // Increase Question Counter 
                    }
                    else
                    {
                        Console.WriteLine("Sorry The Answer is Incorrect");
                        Console.WriteLine("The Correct Answer was: " + qArray[R].CorrectAnswer);
                        QuestionCounter++; // Increase Question Counter 
                    }

                    UserAnswer = Console.ReadLine();
                    Console.WriteLine("Press Enter For the next Question");
                    R = RandQuestion.Next(0, qArray.Length);

                }
            }

            // Display Player Score when # is entered 
            double percentage = PlayerScore / QuestionCounter * 100;
            Console.WriteLine("You have Answered " + PlayerScore + " out of  " + QuestionCounter + " Questions Correctly");
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
        private static void ExitProgram()
        {
            System.Environment.Exit(0);
        }

    }
}