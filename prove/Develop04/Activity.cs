using System;
using System.Collections.Generic;
using System.Threading;

abstract class Activity
{
    protected int duration;
    protected string name;
    protected string description;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Get ready to begin...");
        Thread.Sleep(3000);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine($"Congratulations! You have completed the {name} Activity for {duration} seconds.");
        Thread.Sleep(2000);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("-");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("\\");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("|");
            Thread.Sleep(500);
            Console.Write("\b");
        }
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Time left: {i} seconds");
            Thread.Sleep(1000);
        }
    }

    public abstract void Run();
}