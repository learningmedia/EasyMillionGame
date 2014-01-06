namespace MillionGamePlayer
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class JokerCollection : ReadOnlyCollection<Joker>
    {
        public JokerCollection(IList<Joker> jokerList) : base(jokerList) {}
    }
}