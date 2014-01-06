namespace MillionGamePlayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class MillionGameModel : INotifyPropertyChanged 
    {
        private Game game;

        readonly Random random = new Random();

        readonly string[] commendationMessages = new[]
                               {
                                   "Die Antwort ist richtig. Gratulation!",
                                   "Wunderbar, richtig. Weiter so!", 
                                   "Prima, das ist genau die richtige Antwort.",
                                   "Bestens, besten. Das war spitze!"
                               };

        readonly string[] reproachMessages = new[]
                        {
                            "Leider, leider falsch. Nicht traurig sein!",
                            "Schade, dass war nicht so doll. Die Antwort ist falsch!",
                            "Schlechte Wahl! Das war leider nicht richtig :-(",
                            "O je, das war leider falsch! Schade, schade, schade...!"
                        };

        private AnswerCollection originalAnswers;

        public MillionGameModel(Game game)
        {
            this.Game = game;
            this.Answers = this.game.Answers;
            game.LevelChanged += this.OnLevelChanged;
        }

        public Game Game
        {
            get
            {
                return this.game;
            }
            private set
            {
                this.OnChanged("Game");
                this.game = value;
            }
        }

        public AnswerCollection Answers
        {
            get
            {
                this.originalAnswers = this.game.Answers;
                if (this.originalAnswers.Count != 4)
                {
                    return this.originalAnswers;
                }

                return new AnswerCollection(
                    new List<Answer>
                        {
                            new Answer(
                                string.Concat("a) ", this.originalAnswers[0].Text),
                                this.originalAnswers[0].IsRight,
                                this.originalAnswers[0].IsVisible),
                            new Answer(
                                string.Concat("b) ", this.originalAnswers[1].Text),
                                this.originalAnswers[1].IsRight,
                                this.originalAnswers[1].IsVisible),
                            new Answer(
                                string.Concat("c) ", this.originalAnswers[2].Text),
                                this.originalAnswers[2].IsRight,
                                this.originalAnswers[2].IsVisible),
                            new Answer(
                                string.Concat("d) ", this.originalAnswers[3].Text),
                                this.originalAnswers[3].IsRight,
                                this.originalAnswers[3].IsVisible)
                        });
            }

            set
            {
                this.originalAnswers = value;
                this.OnChanged("Answers");
            }
        }

        public string GetCommendationMessage(Answer answer)
        {
            if (answer.IsRight)
            {
                return this.commendationMessages[this.random.Next(0, 3)];
            }

            return this.reproachMessages[this.random.Next(0, 3)];
        }

        private void OnLevelChanged(object sender, EventArgs e)
        {
            this.OnChanged("Answers");
            this.OnChanged("GetNextLevelButtonText");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
