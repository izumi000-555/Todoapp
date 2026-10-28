using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> tasks = new List<string>();
        List<bool> done = new List<bool>();
        string choice = string.Empty;

        // Problems 1
        // Display a menu that repeats until 4 is entered.
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
                // Problems 2
                // Add a task to the list.
                case "1":
                    Console.Write("Please enter the task name: ");
                    string task = Console.ReadLine();

                    // Problems 4
                    // Do not add empty task names.
                    if (string.IsNullOrWhiteSpace(task))
                    {
                        Console.WriteLine("Task name cannot be empty.");
                    }
                    else
                    {
                        tasks.Add(task.Trim());
                        done.Add(false);
                        Console.WriteLine("Task added.");
                    }
                    break;

                // Problems 2
                // Display all tasks with numbers.
                case "2":
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks yet.");
                    }
                    else
                    {
                        // Problems 5
                        // Display finished and unfinished tasks.
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            if (done[i] == true)
                            {
                                Console.WriteLine($"{i + 1}. [x] {tasks[i]}");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. [ ] {tasks[i]}");
                            }
                        }
                    }
                    break;

                // Problems 3
                // Mark a task as done by its number.
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

                        // Problems 4
                        // Check if the task number is valid.
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
                            done[number - 1] = true;
                            Console.WriteLine("Task marked as done.");
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