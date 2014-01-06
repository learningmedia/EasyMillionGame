namespace MillionGameEditor
{
    using System.ComponentModel;

    internal sealed class Answer : INotifyPropertyChanged
    {
        private bool isRight;
        private string text;

        internal Answer(string text, bool isRight)
        {
            this.text = text;
            this.isRight = isRight;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        public bool IsRight
        {
            get
            {
                return this.isRight;
            }
            set
            {
                this.isRight = value;
                this.OnPropertyChanged("IsRight");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}