namespace MillionGamePlayer
{
    using System;

    [Serializable]
    public sealed class GameMemento
    {
        private bool[] answerVisibility;
        private bool cancelOnFalseAnswer;
        private bool isOver;
        private Joker[] jokers;
        private int level;
        private int levelCount;
        private string name;
        private Statistics statistics;
        private string uri;

        internal GameMemento(string uri,
                             string name,
                             int levelCount,
                             int level,
                             bool cancelOnFalseAnswer,
                             bool isOver,
                             bool[] answerVisibility,
                             Joker[] jokers,
                             Statistics statistics)
        {
            this.uri = uri;
            this.name = name;
            this.levelCount = levelCount;
            this.isOver = isOver;
            this.level = level;
            this.cancelOnFalseAnswer = cancelOnFalseAnswer;
            this.jokers = jokers;
            this.statistics = statistics;
            this.answerVisibility = answerVisibility;
        }

        internal Joker[] Jokers
        {
            get { return jokers; }
            set { jokers = value; }
        }

        internal bool IsOver
        {
            get { return isOver; }
            set { isOver = value; }
        }

        internal int Level
        {
            get { return level; }
            set { level = value; }
        }

        internal bool CancelOnFalseAnswer
        {
            get { return cancelOnFalseAnswer; }
            set { cancelOnFalseAnswer = value; }
        }

        internal string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        internal int LevelCount
        {
            get { return levelCount; }
            set { levelCount = value; }
        }

        internal string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal Statistics Statistics
        {
            get { return statistics; }
            set { statistics = value; }
        }

        internal bool[] AnswerVisibility
        {
            get { return answerVisibility; }
            set { answerVisibility = value; }
        }
    }
}