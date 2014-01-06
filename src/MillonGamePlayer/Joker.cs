namespace MillionGamePlayer
{
    using System;

    [Serializable]
    public abstract class Joker
    {
        private readonly JokerType type;
        private bool enabled;

        protected Joker(JokerType type)
        {
            this.type = type;
            enabled = true;
        }

        public bool Enabled
        {
            get { return enabled; }
            protected set { enabled = value; }
        }

        public JokerType Type
        {
            get { return type; }
        }

        protected static int GetRightIndex(Game game)
        {
            int isRightIndex = -1;

            for (int i = 0; i < game.Answers.Count && isRightIndex == -1; i++)
            {
                if (game.Answers[i].IsRight)
                {
                    isRightIndex = i;
                }
            }

            return isRightIndex;
        }

        internal abstract JokerResult Execute(Game game);
    }
}