namespace MillionGamePlayer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Xml.XPath;

    public sealed class Game : INotifyPropertyChanged
    {
        private const string isOverErrorMessage = "Der Zugriff auf diese Funktion ist nicht möglich, da das Spiel bereits beendet wurde.";

        private readonly Joker[] jokers;
        private readonly Statistics statistics;
        private readonly string uri;
        private bool cancelOnFalseAnswer;
        private bool isOver;
        private int level;
        private int levelCount;
        private LevelInfo levelInfo;
        private string name;

        public ReadOnlyCollection<Joker> Jokers
        {
            get
            {
                return new ReadOnlyCollection<Joker>(jokers);
            }
        }

        public Game(string uri) : this(uri, false)
        {
        }

        public Game(string uri, bool cancelOnFalseAnswer)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("uri", "Die URI darf nicht NULL oder ein Leerstring sein.");
            }
                
            this.uri = uri;
            this.cancelOnFalseAnswer = cancelOnFalseAnswer;

            level = 0;
            isOver = false;

            // Anzahl der Levels aus der Datei einlesen:
            Initialize();

            // Erstes Level aus der Datei einlesen:
            ReadLevelInfo(0);

            // Joker initialisieren:
            jokers = new Joker[] { new FiftyFiftyJoker(), new AudienceJoker(), new TelephoneJoker() };

            // Statistik-Klasse initialisieren:
            statistics = new Statistics();
        }

        public Game(GameMemento memento)
        {
            // Felder wiederherstellen:
            uri = memento.Uri;
            name = memento.Name;
            levelCount = memento.LevelCount;
            level = memento.Level;
            isOver = memento.IsOver;
            cancelOnFalseAnswer = memento.CancelOnFalseAnswer;
            jokers = memento.Jokers;
            statistics = memento.Statistics;

            // Aktuelles Level aus der Datei einlesen:
            ReadLevelInfo(level);

            // Sichtbarkeit der Antworten einstellen:
            for (int i = 0; i < memento.AnswerVisibility.Length; i++)
            {
                Answers[i].IsVisible = memento.AnswerVisibility[i];
            }
        }

        public string Name
        {
            get { return name; }
        }

        public int LevelCount
        {
            get { return levelCount; }
        }

        public string Question
        {
            get { return levelInfo.Question; }
        }

        public int Level
        {
            get { return level; }
        }

        public bool IsOver
        {
            get { return isOver; }
        }

        public bool HasNextLevel
        {
            get
            {
                return (level < levelCount - 1);
            }
        }

        public AnswerCollection Answers
        {
            get
            {
                return levelInfo.Answers;
            }
        }

        public bool CancelOnFalseAnswer
        {
            get
            {
                return cancelOnFalseAnswer;
            }
            set
            {
                cancelOnFalseAnswer = value;
            }
        }

        public Statistics Statistics
        {
            get
            {
                return statistics;
            }
        }

        public event EventHandler LevelChanged;

        public event EventHandler GameOver;

        private void OnLevelChanged()
        {
            if (LevelChanged != null)
            {
                LevelChanged(this, EventArgs.Empty);
            }
        }

        private void OnGameOver()
        {
            if (GameOver != null)
            {
                GameOver(this, EventArgs.Empty);
            }
        }

        public bool Answer(Answer answer)
        {
            if (isOver)
            {
                 throw new InvalidOperationException(isOverErrorMessage);
            }

            bool isRight = answer.IsRight;
            if (isRight)
            {
                statistics.RightAnswers++;
            }
            else
            {
                statistics.WrongAnswers++;
            }

            if ((isRight || !cancelOnFalseAnswer) && HasNextLevel)
            {
                ReadLevelInfo(++level);
                OnLevelChanged();
                OnPropertyChanged("Level");
                OnPropertyChanged("Answers");
                OnPropertyChanged("Question");
            }
            else
            {
                Cancel();
            }

            return isRight;
        }

        public JokerResult UseJoker(Joker joker)
        {
            if (isOver)
            {
                throw new InvalidOperationException(isOverErrorMessage);
            }

            if (Array.IndexOf(jokers, joker) == -1)
            {
                throw new NotSupportedException("Der Joker gehört nicht zu diesem Spiel!");
            }

            if (!joker.Enabled)
            {
                throw new NotSupportedException("Der Joker wurde bereits eingesetzt!");
            }

            statistics.JokersUsed++;
            return joker.Execute(this);
        }

        public void Cancel()
        {
            if (isOver)
                throw new InvalidOperationException(isOverErrorMessage);

            isOver = true;
            OnGameOver();
        }

        public GameMemento ToMemento()
        {
            bool[] answerVisibility = new bool[Answers.Count];
            for (int i = 0; i < Answers.Count; i++)
            {
                answerVisibility[i] = Answers[i].IsVisible;
            }
            return new GameMemento(uri, name, levelCount, level, cancelOnFalseAnswer, isOver, answerVisibility, jokers, statistics);
        }

        private void Initialize()
        {
            XPathNavigator navi = new XPathDocument(uri).CreateNavigator();

            levelCount = Convert.ToInt32(navi.Evaluate("count(Game/Level)"));
            name = navi.SelectSingleNode("Game/@Name").Value;
        }

        private void ReadLevelInfo(int index)
        {
            if (index >= levelCount)
                throw new ArgumentOutOfRangeException("index", "Der Index muss kleiner als die Anzahl der Levels sein.");

            List<Answer> answers = new List<Answer>();

            XPathNavigator xpn = new XPathDocument(uri).CreateNavigator().SelectSingleNode(string.Format("Game/Level[{0}]", index + 1));
            string question = xpn.SelectSingleNode("Question").Value;
            foreach (XPathNavigator x in xpn.Select("Answers/Answer"))
            {
                string value = x.Value;
                bool isRight = x.GetAttribute("IsRight", x.NamespaceURI).ToLower() == "true";
                answers.Add(new Answer(value, isRight, true));
            }

            levelInfo = new LevelInfo(question, answers);
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