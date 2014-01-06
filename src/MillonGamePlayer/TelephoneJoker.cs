namespace MillionGamePlayer
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    internal sealed class TelephoneJoker : Joker
    {
        public TelephoneJoker() : base(JokerType.Telephone) {}

        internal override JokerResult Execute(Game game)
        {
            //Joker deaktivieren
            Enabled = false;

            //Nach Zufallsprinzip wird in die lokale Variable 'answerText' die richtige oder eine falsche der
            //sichtbaren Antworten gespeichert.
            string answerText;
            Random random = new Random();
            int randomNumber = random.Next(100);
            if (randomNumber >= 40)
            {
                answerText = string.Format("Ich bin mir ziemlich sicher, die Antwort lautet:{0}{1}",
                                           Environment.NewLine,
                                           game.Answers[GetRightIndex(game)].Text);
            }
            else if (randomNumber >= 15)
            {
                List<int> remainingAnswerIndices = new List<int>();
                for (int i = 0; i < game.Answers.Count; i++)
                {
                    if (game.Answers[i].IsVisible)
                        remainingAnswerIndices.Add(i);
                }
                answerText = string.Format("Ich weiß es nicht genau, ich glaube die Antwort müsste lauten:{0}{1}",
                                           Environment.NewLine,
                                           game.Answers[remainingAnswerIndices[random.Next(remainingAnswerIndices.Count)]].Text);
            }
            else
            {
                answerText = "Sorry, ich habe wirklich keine Ahnung!";
            }

            //Rückgabe der Telefonantwort.
            return new TelephoneJokerResult(answerText);
        }
    }
}