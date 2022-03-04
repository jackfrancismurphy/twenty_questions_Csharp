using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace ported_twenty_questions
{
    internal class twenty_questions
    {
        //global variables
        private static string fileName;
        private static string Json;
        private static List<Dictionary<string, string>> people;
        private static bool guessed;
        private static int question_count;
        private static string answer;
        private static Dictionary<string, string> answer_dict;

        public static void variable_declarer()
        {

            fileName = @"C:\Users\lilma\Coding\twenty_questions\people_data.json";
            Json = File.ReadAllText(fileName);
            people = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(Json);
            guessed = false;
            question_count = 1;
            answer = "";
            answer_dict = new Dictionary<string, string> { };

        }


        public static string name_randomiser(List<Dictionary<string, string>> lis_of_dics) 
        {
            var name_list = new List<string>();


            foreach (var dict_object in lis_of_dics)
            {
                name_list.Add(dict_object["name"]);
            }

            var random = new Random();
            int random_index = random.Next(name_list.Count);
            return name_list[random_index];
        }
        public static void Main(string[] args)

        {
            Console.WriteLine(@"
                    Welcome to 20 questions!
                    Guess the people according to their properties.
                    The properties in this game are $eT, and those properties are:
                    Fictional, alive, male, female, historic, adult,
                    Child, white, POC(person of colour), ruler,
                    Entertainer, singer, actor, American,
                    English, wealthy, humanitarian *, older than 50,
                    and if the character is fictional then:
                    Superhero, magic, **movie, TV, Book, folk tale, video game

                    *Humanitarian here broadly means that they helped humanity
                    ** Is x mainly known from a movie/ video game etc.. ? Not : do they star in one ?");

            variable_declarer();

            answer = name_randomiser(people);

            answer_dict = GetPeople(people, answer);

            while (guessed == false && question_count < 21)
            {
                string player_answer = asking_phase(question_count);

                guessed = check_answer(player_answer, answer, answer_dict);
                question_count += 1;
                if (guessed == true)
                {
                    Console.WriteLine($"You guessed it! It was {answer}");
                }
                if (question_count >20) 
                {
                    Console.WriteLine($"Bad Luck :( \nThe answer was {answer}. Try again?");
                }
            }

        }

        public static Dictionary<string, string> GetPeople(List<Dictionary<string, string>> people, string answer)
        {
            foreach (Dictionary<string, string> dict_object in people)
            {
                if (answer == dict_object["name"])
                {
                    return dict_object;
                }
            }
            return null;
        }
        public static string asking_phase(int question_count)
        {
            Console.WriteLine($"Question {question_count}");
            string player_answer = Console.ReadLine().Replace("?", "").ToLower().Trim();

            return player_answer;
        }

        public static bool check_answer(string player_answer, string answer, Dictionary<string, string> answer_dict)
        {

            if (player_answer == answer.ToLower())
            {
                return true;
            }
            foreach (string word in player_answer.Split(' '))
            {
                if (answer_dict.ContainsKey(word))
                {
                    System.Console.WriteLine(answer_dict[word]);
                    return false;
                }
            }
            System.Console.WriteLine("Guess again! (and state your guess, i.e: \"Jack Murphy\".)\n");
            System.Console.WriteLine("Neither yes nor no ? Check the properties!");
            return false;
        }
    }
}