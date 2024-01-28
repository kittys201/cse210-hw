using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Por defecto, las palabras están visibles al inicio
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
     {
         if (_isHidden)
              return new string('_', _text.Length); // Devuelve una cadena de guiones bajos del mismo tamaño que la palabra original
         else
           return _text;
      }
}