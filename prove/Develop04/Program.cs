using System;
using System.Collections.Generic;
using System.Threading;
/*
This program is a mindfulness program that offers three different activities to help users relax, reflect, and create lists. The activities are represented by the classes BreathingActivity, ReflectionActivity, and ListingActivity, all inheriting from the abstract class Activity.

Each activity has a starting message that prompts the user for the activity duration in seconds. Then, specific actions are executed for each activity.

- BreathingActivity guides the user through a breathing exercise, alternating between inhaling and exhaling for the specified duration.
- ReflectionActivity prompts the user to reflect on a past experience, displaying a random question followed by a series of additional questions.
- ListingActivity encourages the user to make a list on a specific topic, showing a random prompt and then collecting a list of items entered by the user.

The main program (Program) provides a menu of options for the user to choose a specific activity, creates an instance of the selected activity, and runs it. The main loop allows the user to repeat the experience or exit the program.
*/
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            Activity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
                    break;
                case 2:
                    activity = new ReflectionActivity("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                    break;
                case 3:
                    activity = new ListingActivity("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (activity != null)
            {
                activity.Run();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
