using System;
using System.Collections.Generic;
using System.IO;
public class Journal
{
    public List<Entry> Entries { get; set; } = new List<Entry>();

    public void NewEntry()
    {
        Entry userEntry = new Entry();
        userEntry.GenerateDate();
        userEntry.GeneratePrompt();
        userEntry.GetResponse();
        Entries.Add(userEntry);
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in Entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"{entry.Response}");
            Console.WriteLine();
        }
    }

    public void LoadEntries(string filename)
    {
        Entries.Clear();

        if (filename.EndsWith(".json"))
        {
            string jsonData = File.ReadAllText(filename);
            Entries = JsonConvert.DeserializeObject<List<Entry>>(jsonData);
        }
        else
        {
            // Load from another format if needed
        }
    }

    public void SaveEntries(string filename)
    {
        if (filename.EndsWith(".json"))
        {
            string jsonData = JsonConvert.SerializeObject(Entries, Formatting.Indented);
            File.WriteAllText(filename, jsonData);
        }
        else
        {
            // Save in another format if needed
        }
    }
}