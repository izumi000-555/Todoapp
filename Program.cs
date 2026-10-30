using System;
using System.Collections.Generic;

class Item
{
    public string Task { get; set; } = "";
    public bool IsDone { get; set; }

    public Item(string task)
    {
        Task = task;
        IsDone = false;
    }

    public void MarkDone()
    {
        IsDone = true;
    }

    public void MarkNotDone()
    {
        IsDone = false;
    }

    public string Display()
    {
        if (IsDone)
        {
            return $"[x] {Task}";
        }
        return $"[ ] {Task}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Item> tasks = new List<Item>();
        string choice = string.Empty;

        do
        {
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. List tasks");
            Console.WriteLine("3. Mark task as done");
            Console.WriteLine("4. Task summary");
            Console.WriteLine("5. Exit");
            Console.Write("Please select an option: ");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddTask(tasks);
                    break;

                case "2":
                    DisplayTask(tasks);
                    break;

                case "3":
                    MarkTask(tasks);
                    break;

                case "4":
                    CountTasks(tasks);
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Please enter 1, 2, 3, 4, or 5.");
                    break;
            }

        } while (choice != "5");
    }

    static void AddTask(List<Item> tasks)
    {
        Console.Write("Please enter the task name: ");
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Task name cannot be empty.");
        }
        else
        {
            tasks.Add(new Item(title.Trim()));
            Console.WriteLine("Task added.");
        }
    }

    static void DisplayTask(List<Item> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Display()}");
            }
        }
    }

    static void MarkTask(List<Item> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet.");
        }
        else
        {
            Console.Write("Please enter the task number: ");
            string input = Console.ReadLine() ?? "";
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
                tasks[number - 1].MarkDone();
                Console.WriteLine("Task marked as done.");
            }
        }
    }

    // Problem 1
    // Count finished and unfinished tasks.
    static void CountTasks(List<Item> tasks)
    {
        int finished = 0;
        int unfinished = 0;

        foreach (Item task in tasks)
        {
            if (task.IsDone)
            {
                finished++;
            }
            else
            {
                unfinished++;
            }
        }

        Console.WriteLine($"Finished tasks: {finished}");
        Console.WriteLine($"Unfinished tasks: {unfinished}");
    }
}
