namespace MillionGameEditor
{
    using System.Collections.Generic;
    using System.ComponentModel;

    internal sealed class Level : INotifyPropertyChanged
    {
        private readonly AnswerCollection answers;
        private readonly int levelNumber;
        private string question;

        internal Level(string question, IEnumerable<Answer> answers, int levelNumber)
        {
            this.question = question;
            this.levelNumber = levelNumber;
            this.answers = new AnswerCollection(answers);
        }

        public int LevelNumber
        {
            get
            {
                return this.levelNumber;
            }
        }

        public string Question
        {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value;
                this.OnPropertyChanged("Question");
            }
        }

        public AnswerCollection Answers
        {
            get { return this.answers; }
        }

        public override string ToString()
        {
            return string.Format("Level {0}", levelNumber);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}