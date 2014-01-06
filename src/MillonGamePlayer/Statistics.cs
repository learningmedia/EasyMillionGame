namespace MillionGamePlayer
{
    using System;

    [Serializable]
    public sealed class Statistics
    {
        private int jokersUsed;
        private int rightAnswers;
        private int wrongAnswers;

        public int RightAnswers
        {
            get { return rightAnswers; }
            internal set { rightAnswers = value; }
        }

        public int WrongAnswers
        {
            get { return wrongAnswers; }
            internal set { wrongAnswers = value; }
        }

        public int Answers
        {
            get { return rightAnswers + wrongAnswers; }
        }

        public int JokersUsed
        {
            get { return jokersUsed; }
            internal set { jokersUsed = value; }
        }
    }
}