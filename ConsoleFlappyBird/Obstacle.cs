namespace ConsoleFlappyBird
{
    internal class Obstacle
    {
        internal readonly Random _rnd;

        public int X { get; set; }

        public int Height { get; set; }

        public Obstacle(int minRand, int maxRand)
        {
            _rnd = new Random();
            Height = _rnd.Next(minRand, maxRand);
        }

    }
}
