namespace MillionGamePlayer
{
    internal sealed class AudienceJokerResult : JokerResult
    {
        private readonly Chart chart;

        public AudienceJokerResult(Chart chart)
        {
            this.chart = chart;
        }

        public override JokerType Type
        {
            get { return JokerType.Audience; }
        }

        public override bool HasChart
        {
            get { return true; }
        }

        public override Chart Chart
        {
            get { return chart; }
        }
    }
}