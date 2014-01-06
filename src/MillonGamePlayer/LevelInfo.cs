namespace MillionGamePlayer
{
    using System.Collections.Generic;

    internal sealed class LevelInfo
    {
        private readonly AnswerCollection answers;

        private readonly string question;

        internal LevelInfo(string question, IList<Answer> answers)
        {
            this.question = question;
            this.answers = new AnswerCollection(answers);
        }

        internal string Question
        {
            get { return question; }
        }

        internal AnswerCollection Answers
        {
            get { return answers; }
        }
    }
}