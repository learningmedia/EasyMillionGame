namespace MillionGameEditor
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal sealed class AnswerCollection : ObservableCollection<Answer>
    {
        public static readonly AnswerCollection Empty = new AnswerCollection(new Answer[0]);

        internal AnswerCollection(IEnumerable<Answer> answers) : base(answers) {}
    }
}