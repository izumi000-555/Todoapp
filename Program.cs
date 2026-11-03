using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


class TodoItem
{
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }

    public TodoItem(string title)
    {
        Title = title;
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
        string text = "";

        if (IsDone)
        {
            text = $"[x] {Title}";
        }
        else
        {
            text = $"[ ] {Title}";
        }

        if (DueDate != null)
        {
            text = text + $" (due {DueDate.Value.ToString("yyyy-MM-dd")})";
        }

        return text;
    }

    public void Rename(string title)
    {
        Title = title;
    }
}



class Program
{
    static void Main(string[] args)
    {
        List<TodoItem> tasks = LoadTasks();
        string choice = string.Empty;


        do
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. List tasks");
            Console.WriteLine("3. Mark task as done");
            Console.WriteLine("4. Mark task as not done");
            Console.WriteLine("5. Task summary");
            Console.WriteLine("6. rename a task");
            Console.WriteLine("7. Exit");
            Console.Write("Please select an option: ");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddTask(tasks);
                    break;

                case "2":
                    ListTasks(tasks);
                    break;

                case "3":
                    MarkTaskDone(tasks);
                    break;

                case "4":
                    MarkTaskNotDone(tasks);
                    break;

                case "5":
                    CountTasks(tasks);
                    break;

                case "6":
                    RenameTask(tasks);
                    break;

                case "7":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Please enter 1, 2, 3, 4, 5, 6 or 7.");
                    break;
            }

        } while (choice != "7");
    }

    static void AddTask(List<TodoItem> tasks)
    {
        Console.Write("Please enter the task name: ");
        //Predict: what happens if you remove ?? yet in the?? "" from one line? 
        //My prediction: A warning is issued because the value returned by ReadLine() could potentially be null.
        //Remove it: A warning appears indicating that a value which could be null is being assigned to a non-nullable type.
        //Explanation: Used to prevent errors caused by null values. ?? acts as a substitute for an if statement; if ReadLine() returns null, it provides "" instead.
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Task name cannot be empty.");
        }
        else
        {

            Console.Write("Enter due date (yyyy-MM-dd, or leave empty): ");
            string input = Console.ReadLine() ?? "";

            DateTime? dueDate = null;

            if (!string.IsNullOrWhiteSpace(input))
            {
                if (DateTime.TryParse(input, out DateTime date))
                {
                    dueDate = date.Date;
                }
                else
                {
                    Console.WriteLine("Invalid date.");
                    return;
                }
            }

            TodoItem item = new TodoItem(title.Trim());
            item.DueDate = dueDate;

            tasks.Add(item);
            SaveTasks(tasks);
            Console.WriteLine("Task added.");
        }
    }

    static void ListTasks(List<TodoItem> tasks)
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

    static void MarkTaskDone(List<TodoItem> tasks)
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
                return;
            }
            bool judgement = IsValidTaskNumber(tasks, number);
            if (judgement == true)
            {
                Console.WriteLine("Task number does not exist.");
            }
            else
            {
                tasks[number - 1].MarkDone();
                SaveTasks(tasks);
                Console.WriteLine("Task marked as done.");
            }
        }
    }

    static void MarkTaskNotDone(List<TodoItem> tasks)
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
                return;
            }
            bool judgement = IsValidTaskNumber(tasks, number);
            if (judgement == true)
            {
                Console.WriteLine("Task number does not exist.");
            }
            else
            {
                tasks[number - 1].MarkNotDone();
                SaveTasks(tasks);
                Console.WriteLine("Task marked as done.");
            }
        }
    }

    // Problem 1
    // A feature for counting completed and incomplete items
    static void CountTasks(List<TodoItem> tasks)
    {
        int finished = 0;
        int unfinished = 0;

        foreach (TodoItem task in tasks)
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


    // Problem 2
    // A feature to rename tasks.
    static void RenameTask(List<TodoItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet.");
        }
        else
        {
            Console.WriteLine("Please enter the number of the task you would like to rename. ");
            string input = Console.ReadLine() ?? "";
            int number;
            if (!int.TryParse(input, out number))
            {
                Console.Write("Please enter a valid number.");
                return;
            }
            bool judgement = IsValidTaskNumber(tasks, number);
            if (judgement == true)
            {
                Console.WriteLine("Task number does not exist. ");
            }
            else
            {
                Console.WriteLine("Please enter the new name. ");
                string title = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Task name cannot be empty.");
                }
                else
                {
                    tasks[number - 1].Rename(title.Trim());
                    SaveTasks(tasks);
                    Console.WriteLine("Changes have been completed.");
                }
            }
        }
    }

    static bool IsValidTaskNumber(List<TodoItem> tasks, int number)
    {
        if (number < 1 || number > tasks.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Problem 5
    // Load tasks from a JSON file
    static List<TodoItem> LoadTasks()
    {
        if (!File.Exists("tasks.json"))
        {
            return new List<TodoItem>();
        }

        string json = File.ReadAllText("tasks.json");
        List<TodoItem>? tasks = JsonSerializer.Deserialize<List<TodoItem>>(json);
        if (tasks == null)
        {
            return new List<TodoItem>();
        }

        return tasks;
    }

    static void SaveTasks(List<TodoItem> tasks)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        string json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText("tasks.json", json);
    }
}
