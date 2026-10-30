using System;
using System.Collections.Generic;

class TodoItem
{
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }

    public TodoItem(string task)
    {
        Title = task;
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
            return $"[x] {Title}";
        }
        return $"[ ] {Title}";
    }

    public void Rename(string title)
    {
        Title = title;
    }
}

class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public string Display()
    {
        return $"Title: {Title}  Author: {Author} -  {Year}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<TodoItem> tasks = new List<TodoItem>();
        string choice = string.Empty;
        List<Book> books = new List<Book>();
        BookInfo(books);
        BookDisplay(books);

        do
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. List tasks");
            Console.WriteLine("3. Mark task as done");
            Console.WriteLine("4. Task summary");
            Console.WriteLine("5. rename a task");
            Console.WriteLine("6. Exit");
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
                    RenameTask(tasks);
                    break;

                case "6":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Please enter 1, 2, 3, 4, 5 or 6.");
                    break;
            }

        } while (choice != "6");
    }

    static void AddTask(List<TodoItem> tasks)
    {
        Console.Write("Please enter the task name: ");
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Task name cannot be empty.");
        }
        else
        {
            tasks.Add(new TodoItem(title.Trim()));
            Console.WriteLine("Task added.");
        }
    }

    static void DisplayTask(List<TodoItem> tasks)
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

    static void MarkTask(List<TodoItem> tasks)
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
            Console.WriteLine("Please enter a valid number.");
        }
        else if (number < 1 || number > tasks.Count)
        {
            Console.WriteLine("Task number does not exist.");
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
                Console.WriteLine("Changes have been completed.");
            }
        }
        }
        
    }

    // Problem3
    // Display book information
    static void BookInfo(List<Book> books)
    {
        books.Add(new Book("A Woman", "Takeo Arisima", 1919));
        books.Add(new Book("L'après-midi d'un faune", "Stéphane Mallarmé", 1876));
        books.Add(new Book("Night on the Galactic Railroad", "Kenzi Miyazawa", 1934));
        books.Add(new Book("Chronicles of the Faceless - Twin children", "Yamori Mochiduki", 2009));
        books.Add(new Book("The Great Passage", "Shion Miura", 2011));
    }

    // Problem4
    // A feature to display books published in or after the year 2000.
    static void BookDisplay(List<Book> books)
    {
        string choice = string.Empty;
        Console.WriteLine("---------------------");
        Console.WriteLine("1. Show only items published in or after 2000");
        Console.WriteLine("2. Show all");
        Console.Write("Please select an option: ");
        choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                foreach (Book book in books)
                {
                    if(book.Year > 2000){
                        Console.WriteLine(book.Display());
                    }
                }

                break;
            case "2":
                foreach (Book book in books)
                {
                    Console.WriteLine(book.Display());
                }
                break;

            default:
                Console.WriteLine("Please enter 1 or 2.");
                break;
        }
        
    }
}
