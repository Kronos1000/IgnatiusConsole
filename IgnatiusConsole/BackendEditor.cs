using System;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;


class BackendEditor
{

    static string repoPath =
        @"C:\Github\IgnatiusQuestionsCSV";


    static string questionPath =
        @"C:\Github\IgnatiusQuestionsCSV\Questions.csv";


    static string subjectPath =
        @"C:\Github\IgnatiusQuestionsCSV\Subjects.csv";



    static void Start()
    {

        while (true)
        {

            Console.WriteLine();
            IgnatiusBanner();
            Console.WriteLine("1) Add Subject");
            Console.WriteLine("2) Add Questions");
            Console.WriteLine("3) View Questions");
            Console.WriteLine("4) Quiz Me");
            Console.WriteLine("5) Exit");

            Console.Write("Choice: ");


            string choice = Console.ReadLine();



            switch (choice)
            {

                case "1":

                    AddSubject();

                    break;



                case "2":

                    AddQuestions();

                    CommitAndPushQuestions();

                    break;



                case "3":
                    ViewQuestions(); break;
                case "4":

                    return;


                default:

                    Console.WriteLine(
                        "Invalid option"
                    );

                    break;

            }

        }

    }






    static void AddSubject()
    {

        CreateSubjectFile();


        var lines =
            File.ReadAllLines(subjectPath);



        int nextId = 1;



        if (lines.Length > 1)
        {

            nextId =
                lines
                .Skip(1)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                {

                    int id = 0;

                    int.TryParse(
                        x.Split(',')[0],
                        out id
                    );

                    return id;

                })
                .Max() + 1;

        }



        Console.Write(
            "Enter new subject: "
        );


        string subject =
            Console.ReadLine()
            .Trim();



        if (string.IsNullOrWhiteSpace(subject))
        {
            Console.WriteLine(
                "❌ Subject cannot be empty"
            );

            return;
        }



        bool exists =
            lines
            .Skip(1)
            .Any(x =>
            {

                string existing =
                    x.Split(',')
                    .Skip(1)
                    .FirstOrDefault();


                return existing != null &&
                    existing.Equals(
                        subject,
                        StringComparison.OrdinalIgnoreCase
                    );

            });



        if (exists)
        {

            Console.WriteLine(
                "⚠️ Subject already exists"
            );

            return;

        }



        using (StreamWriter sw =
            new StreamWriter(
                subjectPath,
                true,
                Encoding.UTF8))
        {

            sw.WriteLine(
                $"{nextId},{Clean(subject)}"
            );

        }



        Console.WriteLine(
            $"✅ Subject added: {subject}"
        );

    }








    static string SelectSubject()
    {

        CreateSubjectFile();



        var subjects =
            File.ReadAllLines(subjectPath)
            .Skip(1)
            .Where(x =>
                !string.IsNullOrWhiteSpace(x))
            .ToList();



        if (subjects.Count == 0)
        {

            Console.WriteLine(
                "❌ No subjects available. Add one first."
            );

            return null;

        }



        Console.WriteLine();

        Console.WriteLine(
            "Select Subject:"
        );



        for (int i = 0; i < subjects.Count; i++)
        {

            string name =
                subjects[i]
                .Split(',')
                [1];


            Console.WriteLine(
                $"{i + 1}) {name}"
            );

        }



        Console.Write(
            "Choice: "
        );



        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            return null;
        }



        if (choice < 1 || choice > subjects.Count)
        {
            return null;
        }



        return subjects[choice - 1]
            .Split(',')[1];

    }








   public static void AddQuestions()
    {

        CreateQuestionFile();



        Console.Write(
            "How many questions to add: "
        );


        int count =
            int.Parse(
                Console.ReadLine()
            );



        int nextId =
            GetNextQuestionId();




        using (StreamWriter sw =
            new StreamWriter(
                questionPath,
                true,
                Encoding.UTF8))
        {



            for (int i = 0; i < count; i++)
            {


                Console.WriteLine();

                Console.WriteLine(
                    $"--- Question {i + 1} ---"
                );



                string subject =
                    SelectSubject();



                if (subject == null)
                {

                    Console.WriteLine(
                        "❌ Invalid subject"
                    );

                    return;

                }



                Console.Write(
                    "Question: "
                );

                string question =
                    Console.ReadLine();



                Console.Write(
                    "Option 1: "
                );

                string option1 =
                    Console.ReadLine();



                Console.Write(
                    "Option 2: "
                );

                string option2 =
                    Console.ReadLine();



                Console.Write(
                    "Option 3: "
                );

                string option3 =
                    Console.ReadLine();




                Console.WriteLine();

                Console.WriteLine(
                    "Correct Answer"
                );

                Console.WriteLine(
                    "1) Option 1"
                );

                Console.WriteLine(
                    "2) Option 2"
                );

                Console.WriteLine(
                    "3) Option 3"
                );


                Console.Write(
                    "Choice: "
                );


                string choice =
                    Console.ReadLine();



                string answer;

                if (choice == "1")
                {
                    answer = option1;
                }
                else if (choice == "2")
                {
                    answer = option2;
                }
                else if (choice == "3")
                {
                    answer = option3;
                }
                else
                {
                    answer = option1;
                }


                sw.WriteLine(
                    $"{nextId}," +
                    $"{Clean(question)}," +
                    $"{Clean(subject)}," +
                    $"{Clean(option1)}," +
                    $"{Clean(option2)}," +
                    $"{Clean(option3)}," +
                    $"{Clean(answer)}"
                );



                Console.WriteLine(
                    $"✅ Added Question ID {nextId}"
                );



                nextId++;

            }


        }

    }








    static int GetNextQuestionId()
    {

        var lines =
            File.ReadAllLines(questionPath);



        if (lines.Length <= 1)
            return 1;



        return
            lines
            .Skip(1)
            .Where(x =>
                !string.IsNullOrWhiteSpace(x))
            .Select(x =>
            {

                int id = 0;

                int.TryParse(
                    x.Split(',')[0],
                    out id
                );

                return id;

            })
            .Max() + 1;

    }








    static void CreateQuestionFile()
    {

        if (!File.Exists(questionPath))
        {

            File.WriteAllText(
                questionPath,
                "ID,Question,Subject,Option1,Option2,Option3,Answer"
                + Environment.NewLine
            );

        }

    }





    static void CreateSubjectFile()
    {

        if (!File.Exists(subjectPath))
        {

            File.WriteAllText(
                subjectPath,
                "ID,Subject"
                + Environment.NewLine
            );

        }

    }







    static string Clean(string text)
    {

        if (string.IsNullOrWhiteSpace(text))
            return "";


        return text
            .Replace(",", " ")
            .Replace("\r", " ")
            .Replace("\n", " ");

    }








   public static void CommitAndPushQuestions()
    {

        string tokenPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Token.txt"
            );



        if (!File.Exists(tokenPath))
        {

            Console.WriteLine(
                "❌ Token.txt missing"
            );

            return;

        }



        string token =
            File.ReadAllText(tokenPath)
            .Trim();



        string repoUrl =
            "https://github.com/Kronos1000/IgnatiusQuestionsCSV.git";



        try
        {

            using (Repository repo =
                new Repository(repoPath))
            {


                Commands.Stage(
                    repo,
                    "*.csv"
                );



                Signature author =
                    new Signature(
                        "Kronos1000",
                        "Patrick.ware1998@outlook.com",
                        DateTimeOffset.Now
                    );



                repo.Commit(
                    $"📚 Added quiz batch {DateTime.Now:dd/MM/yyyy}",
                    author,
                    author
                );



                Remote remote =
                    repo.Network.Remotes["questions-origin"];



                if (remote == null)
                {

                    remote =
                        repo.Network.Remotes.Add(
                            "questions-origin",
                            repoUrl
                        );

                }



                PushOptions options =
                    new PushOptions
                    {

                        CredentialsProvider =
                        (_url, _user, _cred) =>
                        new UsernamePasswordCredentials
                        {

                            Username =
                                "x-access-token",

                            Password =
                                token

                        }

                    };



                repo.Network.Push(
                    remote,
                    "refs/heads/main",
                    options
                );



                Console.WriteLine(
                    "✅ Pushed to GitHub"
                );


            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(
                $"❌ Git error: {ex.Message}"
            );

        }

    }

    static void ViewQuestions()
    {

        CreateQuestionFile();


        var questions =
            File.ReadAllLines(questionPath)
            .Skip(1)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();



        if (questions.Count == 0)
        {

            Console.WriteLine(
                "❌ No questions found."
            );

            return;

        }



        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine("          QUESTIONS");
        Console.WriteLine("==============================");
        Console.WriteLine();



        // Table header
        Console.WriteLine(
            "{0,-5} | {1,-35} | {2,-15} | {3,-20}",
            "ID",
            "Question",
            "Subject",
            "Answer"
        );


        Console.WriteLine(
            new string('-', 85)
        );



        foreach (string line in questions)
        {

            string[] data =
                line.Split(',');



            if (data.Length < 7)
                continue;



            string question =
                data[1].Length > 35
                ? data[1].Substring(0, 32) + "..."
                : data[1];


            string subject =
                data[2].Length > 15
                ? data[2].Substring(0, 12) + "..."
                : data[2];


            string answer =
                data[6].Length > 20
                ? data[6].Substring(0, 17) + "..."
                : data[6];



            Console.WriteLine(
                "{0,-5} | {1,-35} | {2,-15} | {3,-20}",
                data[0],
                question,
                subject,
                answer
            );

        }



        Console.WriteLine();

        Console.WriteLine(
            $"Total Questions: {questions.Count}"
        );


    }


    // Banner 
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
