namespace MillionGamePlayer
{
    using System.Windows.Data;

    public static class Converters
    {
        private static readonly IValueConverter indexConverter = new IndexConverter();

        public static IValueConverter IndexConverter
        {
            get
            {
                return indexConverter;
            }
        }
    }
}
