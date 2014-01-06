namespace MillionGamePlayer
{
    internal sealed class TelephoneJokerResult : JokerResult
    {
        private readonly string message;

        public TelephoneJokerResult(string message)
        {
            this.message = message;
        }

        public override JokerType Type
        {
            get { return JokerType.Telephone; }
        }

        public override bool HasMessage
        {
            get { return true; }
        }

        public override string Message
        {
            get { return message; }
        }
    }
}