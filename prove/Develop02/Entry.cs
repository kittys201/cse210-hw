public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public void GenerateDate()
    {
        Date = DateTime.Now.ToShortDateString();
    }

    public void GeneratePrompt()
    {
        List<string> prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            // Agregue sus propias preguntas aquí
        };

        Random rand = new Random();
        int randomIndex = rand.Next(prompts.Count);
        Prompt = prompts[randomIndex];
        Console.WriteLine(Prompt);
    }

    public void GetResponse()
    {
        Console.Write("> ");
        Response = Console.ReadLine();
    }
}
