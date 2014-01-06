namespace MillionGameEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Linq;

    internal sealed class Game : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Level> levels = new ObservableCollection<Level>();
        private string name;

        public Game(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            XElement gameElement = XElement.Load(fileName);

            this.name = (string) gameElement.Attribute(XNames.Name);

            int counter = 0;

            foreach (XElement levelElement in gameElement.Elements(XNames.Level))
            {
                string question = (string) levelElement.Element(XNames.Question);
                IEnumerable<Answer> answers = levelElement.Element(XNames.Answers).Elements(XNames.Answer).Select(x => new Answer((string) x, ((bool?) x.Attribute(XNames.IsRight)) ?? false));
                
                this.levels.Add(new Level(question, answers, ++counter));
            }
        }

        public Game()
        {
            this.name = "[Bitte hier den Namen des Spiels eingeben]";

            IEnumerable<Level> lvls = Enumerable.Range(1, 10)
                .Select(x =>
                        new Level(string.Format("[Bitte hier Frage {0} eingeben]", x),
                                  Enumerable.Range(1, 4).Select(y =>
                                                                new Answer(string.Format("[Bitte hier Antwort {0} eingeben]", y), y == 1)),
                                  x));

            foreach (Level lvl in lvls)
            {
                this.levels.Add(lvl);
            }
        }

        public void Save(string fileName)
        {
            new XElement(XNames.Game,
                         new XAttribute(XNames.Name, this.Name),
                         this.Levels
                             .Select(x =>
                                     new XElement(XNames.Level,
                                                  new XElement(XNames.Question, x.Question),
                                                  new XElement(XNames.Answers,
                                                               x.Answers
                                                                   .Select(y =>
                                                                           new XElement(XNames.Answer,
                                                                                        new XAttribute(XNames.IsRight, y.IsRight),
                                                                                        y.Text)
                                                                   )
                                                      )
                                         )
                             )
                )
                .Save(fileName);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<Level> Levels
        {
            get
            {
                return this.levels;
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