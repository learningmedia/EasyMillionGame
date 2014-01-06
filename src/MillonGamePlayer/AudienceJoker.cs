namespace MillionGamePlayer
{
    using System;

    [Serializable]
    internal sealed class AudienceJoker : Joker
    {
        public AudienceJoker() : base(JokerType.Audience) {}

        internal override JokerResult Execute(Game game)
        {
            Enabled = false;
            return new AudienceJokerResult(new Chart(GenerateValues(game)));
        }

        private static int[] GenerateValues(Game game)
        {
            int visibleAnswersCount = 0;
            int[] intArr = new int[game.Answers.Count];

            //Hier wird die Anzahl der sichtbaren Fragen in einer Hilfsvariable gespeichert, das Rückgabewert-IntArray 
            //erzeugt und im Falle sichbarer Fragen mit den Indexwert der jeweils sichtbaren Frage gefüllt, 
            //andernfalls wird -1 gespeichert
            for (int i = 0; i < intArr.Length; i++)
            {
                if (game.Answers[i].IsVisible)
                {
                    visibleAnswersCount++;
                }

                if (game.Answers[i].IsVisible)
                {
                    intArr[i] = i;
                }
                else
                {
                    intArr[i] = -1;
                }    
            }

            //Hier werden die Zufallszahlen errechnet und diese in jene Arrayplätze kopiert, 
            //die den Indexwert einer sichtbaren Frage enthalten 
            //An den Arrayplätze mit -1 wird für die Anzeige der Wert 0 gespeichert
            Random random = new Random();
            int isRightValueIndex = GetRightIndex(game);
            intArr[isRightValueIndex] = random.Next(30, 80);
            int rest = 100 - intArr[isRightValueIndex];
            int counter = 1;
            for (int i = 0; i < game.Answers.Count; i++)
            {
                if (i != isRightValueIndex && intArr[i] != -1)
                {
                    if (++counter < visibleAnswersCount)
                        intArr[i] = rest > 0 ? random.Next(rest) : 0;
                    else
                        intArr[i] = rest;

                    rest -= intArr[i];
                }

                if (intArr[i] == -1)
                    intArr[i] = 0;
            }

            //Rückgabe-Array mit 2 oder 4 Zufallszahlen. An den Arrayplätzen, die unsichtbare Fragen repräsentieren, ist
            //der Wert 0 gespeichert. Also z.B. {0, 79, 0, 21} oder {61, 9, 19, 11}
            return intArr;
        }
    }
}