namespace MillionGamePlayer
{
    using System.ComponentModel;

    public sealed class Answer : INotifyPropertyChanged
    {
        private readonly bool isRight;

        private readonly string text;

        private bool isVisible;

        public Answer(string text, bool isRight, bool isVisible)
        {
            this.text = text;
            this.isRight = isRight;
            this.isVisible = isVisible;
        }

        public string Text
        {
            get { return text; }
        }

        public bool IsRight
        {
            get { return isRight; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}