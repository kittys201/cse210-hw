using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

//Operating requirements you must install the Newtonsoft.Json library to make it easier to use nuget gallery puging within VsCode
//Download the script file to the address C:\ lds-scriptures-json.txt). 
//If you place it somewhere else, please enter the correct address where the lds-scriptures-json.txt file will be located. 
//This is defaulted to capture max 5 random verses and will display once per screen. /

// Tomar hasta 5 entradas aleatorias

class Program
{
    static void Main(string[] args)
    {
        List<ScriptureData> scriptures = ReadScripturesFromFile("c:\\lds-scriptures-json.txt");
        
        // Tomar hasta 5 entradas aleatorias
        Random random = new Random();
        int entriesToTake = Math.Min(5, scriptures.Count);
        List<ScriptureData> randomEntries = new List<ScriptureData>();
        for (int i = 0; i < entriesToTake; i++)
        {
            int randomIndex = random.Next(0, scriptures.Count);
            randomEntries.Add(scriptures[randomIndex]);
            scriptures.RemoveAt(randomIndex);
        }
        
        // Crear instancias de la clase Reference y Scripture para las entradas aleatorias
        foreach (ScriptureData data in randomEntries)
        {
            Reference reference = new Reference(data.book_short_title, data.chapter_number, data.verse_number);
            Scripture scripture = new Scripture(reference, data.scripture_text);

            // Mostrar la Escritura completa al inicio
            Console.WriteLine(scripture.GetDisplayText());

            // Ciclo para ocultar palabras hasta que la Escritura esté completamente oculta
            bool quitRequested = false;
            while (!scripture.IsCompletelyHidden() && !quitRequested)
            {
                Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    quitRequested = true;
                else
                {
                    // Ocultar algunas palabras al presionar Enter
                    scripture.HideRandomWords(2);
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    if (quitRequested)
                break;
                }
            }

            if (quitRequested)
                break; // Salir del bucle foreach si se solicitó salir
        }

        Console.WriteLine("Program completed");
    }

    static List<ScriptureData> ReadScripturesFromFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ScriptureData>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return new List<ScriptureData>();
        }
    }
}

