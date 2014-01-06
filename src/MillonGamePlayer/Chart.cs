namespace MillionGamePlayer
{
    public sealed class Chart
    {
        private readonly int[] data;

        public Chart(int[] data)
        {
            this.data = data;
        }

        public int[] Data
        {
            get { return data; }
        }
    }
}