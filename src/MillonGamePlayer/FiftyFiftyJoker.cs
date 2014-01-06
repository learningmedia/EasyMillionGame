namespace MillionGamePlayer
{
    using System;

    [Serializable]
    internal sealed class FiftyFiftyJoker : Joker
    {
        public FiftyFiftyJoker() : base(JokerType.FiftyFifty) {}

        internal override JokerResult Execute(Game game)
        {
            Enabled = false;

            int isRightIndex = GetRightIndex(game);

            Random rnd = new Random();
            int counter = 0;
            do
            {
                int rndNumber = rnd.Next(4);
                if (rndNumber != isRightIndex)
                {
                    if (game.Answers[rndNumber].IsVisible)
                    {
                        game.Answers[rndNumber].IsVisible = false;
                        counter++;
                    }
                }
            } while (counter < 2);

            return new FiftyFiftyJokerResult();
        }
    }
}