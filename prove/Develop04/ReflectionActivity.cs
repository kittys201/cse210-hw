using System;
using System.Collections.Generic;
using System.Threading;


class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's begin the breathing exercise...");
        ShowCountDown(duration);

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000);
        }

        DisplayEndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's reflect on a past experience...");
        ShowCountDown(duration);

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);

        DisplayQuestions();

        DisplayEndingMessage();
    }

    private void DisplayQuestions()
    {
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for reflection
            ShowSpinner(3);
        }
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's list some things...");
        ShowCountDown(duration);

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);

        GetListFromUser();

        DisplayEndingMessage();
    }

    private void GetListFromUser()
    {
        List<string> items = new List<string>();
        Console.WriteLine("Enter items (one per line), press enter twice to finish:");

        string input;
        while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
        {
            items.Add(input);
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}
