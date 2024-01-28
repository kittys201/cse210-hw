using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse; // Añadir el atributo _endVerse

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    public string GetDisplayText()
    {
        if (_endVerse == 0)
            return $"{_book} {_chapter}:{_verse}";
        else
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}