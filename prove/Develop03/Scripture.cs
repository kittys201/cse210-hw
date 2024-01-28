using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        // Separar el texto de la Escritura en palabras
        string[] words = text.Split(' ');
        foreach (string word in words)
        {
            _words.Add(new Word(word));
        }
    }
    

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        for (int i = 0; i < numberToHide; i++)
        {
            int index = random.Next(0, _words.Count);
            _words[index].Hide();
        }
    }

    public string GetDisplayText()
    {
        string displayText = $"{_reference.GetDisplayText()}\n";
        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        return displayText;
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }
}
class ScriptureData
{
    public string volume_title { get; set; }
    public string book_title { get; set; }
    public string book_short_title { get; set; }
    public int chapter_number { get; set; }
    public int verse_number { get; set; }
    public string verse_title { get; set; }
    public string verse_short_title { get; set; }
    public string scripture_text { get; set; }
}