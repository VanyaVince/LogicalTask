﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercise1();
            //Exercise2();
            //Exercise3();
            //Exercise4();
            //Exercise5();
        }
        /*Write a program and ask the user to enter a few numbers separated by a hyphen.
         Work out if the numbers are consecutive. 
         For example, if the input is "5-6-7-8-9" or "20-19-18-17-16", display a message: "Consecutive"; otherwise, display "Not Consecutive".*/
        private static void Exercise1()
        {
            Console.WriteLine("Please, write a few numbers separated by a hyphen");

            while (true)
            {
                var input = Console.ReadLine();
                /*If string is null, start over*/
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Sorry but the sequence of numbers is entered incorrectly, re-try again using a hyphen sign ");
                    continue;
                }
                
                var ifOnlyNumbers = string.Join("", input.Split('-'));

                if (int.TryParse(ifOnlyNumbers, out int c))
                {
                    var result = DefineSequence(input);
                    Console.WriteLine(result);
                    break;
                }
                /*if userString has of any letter*/
                Console.WriteLine("Sorry but the sequence of numbers is entered incorrectly, re-try again using a hyphen sign");
            }
        }

        private static string DefineSequence(string input)
        {
            var numbers = input.Split('-').Select(int.Parse).ToList();
            var isConsecutive = true;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (Math.Abs(numbers[i] - numbers[i + 1]) != 1)
                {
                    isConsecutive = false;
                    break;
                }
            }
            return (isConsecutive) ? $"The sequence of numbers '{input}' is 'Consecutive'" : $"The sequence of numbers {input} is 'Not Consecutive'";
        }

        /*Write a program and ask the user to enter a few numbers separated by a hyphen.
         If the user simply presses Enter, without supplying an input, exit immediately; otherwise, check to see if there are duplicates. 
         If so, display "Duplicate" on the console.*/
        private static void Exercise2()
        {
            Console.WriteLine("Please, write a few numbers separated by a hyphen");

            var input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                var numbers = input.Split('-').Select(int.Parse).ToList();
                List<int> duplicateNumbers = new List<int>();
                for (int i = 0; i < numbers.Count - 1; i++)
                {
                    for (int j = i + 1; j < numbers.Count; j++)
                    {
                        /*checking if the list has duplicate and if we haven't already checked the same number*/
                        if (numbers[i] == numbers[j] && !duplicateNumbers.Contains(numbers[i])) 
                            duplicateNumbers.Add(numbers[i]);
                    }
                }
                var result = string.Join(",", duplicateNumbers);
                Console.WriteLine($"The duplicate number(s): {result}");
            }
        }

        private static void Exercise3()
        {
            Console.WriteLine("Enter a time value in the 24 time format (e.g 19:00)");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid time");
                return;
            }
            
            var timeElements = input.Split(':');
            var hours = Convert.ToInt32(timeElements[0]);
            var minutes = Convert.ToInt32(timeElements[1]);

            /*input consists of letters or any another invalid symbol*/
            if (!int.TryParse(string.Join("", timeElements), out int i))
            {
                Console.WriteLine("Invalid time");
                return;
            }

            /*if the entered date are not out of world time range*/
            if (timeElements.Length != 2 || hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                Console.WriteLine("Invalid time");
                return;
            }
            
            DateTime time = DateTime.Parse(string.Join(":", timeElements));
            var result = (time.ToString("HH:mm") == input) ? $"The time {input} is valid" : $"The time {input} is invalid";
            Console.WriteLine(result);
        }

        /*The variable name with PascalName*/
        private static void Exercise4()
        {
            Console.WriteLine("Enter some words separated by a space");
            var input = Console.ReadLine();
            var result = "";


            if (string.IsNullOrEmpty(input))
                return;
            var words = input.ToLower().Trim().Split(' ');
            foreach (var word in words)
                result += word.Replace(word[0], char.ToUpper(word[0]));
            Console.WriteLine($"The variable name with PascalName: {result}");
        }

        /*count all vowels sounds in a word*/
        private static void Exercise5()
        {
            Console.WriteLine("Enter a word and the program count all its vowels");
            var input = Console.ReadLine();
            var count = 0;

            if (string.IsNullOrEmpty(input))
                return;

            var charOfInput = input.Replace(" ", "").ToCharArray();
            List<char> vowels = new List<char>(){'a', 'e', 'i', 'o', 'u'};

            foreach (var ch in charOfInput)
            {
                if (vowels.Contains(ch))
                    count += 1;
            }

            Console.WriteLine($"The word includes {count} vowels");
        }
    }
}
