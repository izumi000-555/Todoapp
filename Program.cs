using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Problems 1
        // Display a menu until 4 is entered.
        string choice = string.Empty;

        do
        {
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. List tasks");
            Console.WriteLine("3. Mark task as done");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                case "2":
                case "3":
                    Console.WriteLine("Not ready yet.");
                    break;

                case "4":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Please enter 1, 2, 3, or 4.");
                    break;
            }

        } while (choice != "4");
    }
}
