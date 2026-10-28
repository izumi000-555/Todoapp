using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Problems 4
        // Check empty task names and invalid task numbers.
        List<string> tasks = new List<string>();
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
                    Console.Write("Please enter the task name: ");
                    string task = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(task))
                    {
                        Console.WriteLine("Task name cannot be empty.");
                    }
                    else
                    {
                        tasks.Add(task.Trim());
                        Console.WriteLine("Task added.");
                    }
                    break;

                case "2":
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks yet.");
                    }
                    else
                    {
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i]}");
                        }
                    }
                    break;

                case "3":
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks yet.");
                    }
                    else
                    {
                        Console.Write("Please enter the task number: ");
                        string input = Console.ReadLine();
                        int number;
                        if (!int.TryParse(input, out number))
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }
                        else if (number < 1 || number > tasks.Count)
                        {
                            Console.WriteLine("Task number does not exist.");
                        }
                        else
                        {
                            tasks.RemoveAt(number - 1);
                            Console.WriteLine("Task removed.");
                        }
                    }
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
