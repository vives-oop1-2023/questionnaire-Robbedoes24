namespace QuestionnaireLibrary
{
    public class Answer
    {
        public Answer(string text, bool isCorrect)
        { 
            Text = text;
            IsCorrect = isCorrect;
        }

        public override string ToString()
        {
            return $"{Text}";
        }

        public string Text
        {
            get { return text; }
            private set { text = value;}
        }

        public bool IsCorrect
        {
            get { return isCorrect; }
            private  set { isCorrect = value; }

        }

        private string text = "";
        private bool isCorrect = false;
    }
}
