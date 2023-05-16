using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Answer
    {
        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }

        // Do we need the tostring methode
        public override string ToString()
        {
            return $"{Text}";
        }

        public string Text { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}
