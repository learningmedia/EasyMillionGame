namespace MillionGamePlayer
{
    using System;

    public abstract class JokerResult
    {
        public abstract JokerType Type
        {
            get;
        }

        public virtual bool HasMessage
        {
            get
            {
                return false;
            }
        }

        public virtual string Message
        {
            get
            {
                throw new NotSupportedException("No message available.");
            }
        }

        public virtual bool HasChart
        {
            get
            {
                return false;
            }
        }

        public virtual Chart Chart
        {
            get
            {
                throw new NotSupportedException("No chart available.");
            }
        }
    }
}