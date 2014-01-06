namespace MillionGamePlayer
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class AnswerCollection : ReadOnlyCollection<Answer>
    {
        public static readonly AnswerCollection Empty = new AnswerCollection(new Answer[] { });

        public AnswerCollection(IList<Answer> answers) : base(answers)
        {
        }
    }
}