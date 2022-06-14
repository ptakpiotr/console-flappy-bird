//#define solo


using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleFlappyBird
{
    public class GameAreaCreator
    {
        private readonly Player _p;
        private readonly Obstacle[] _obstacles;
        private readonly Random _soloRand;

        private bool _gameOver;

        public int Length { get; set; } = DefaultValues.Length;
        public int Height { get; set; } = DefaultValues.Height;

        public GameAreaCreator()
        {
            _p = new Player() { X = Length / 2 - 15, Y = Height / 2 - 2 };
            _obstacles = new Obstacle[] { new(DefaultValues.MinimumRandHeightObstacles, DefaultValues.MaxRandHeightObstacles) { X = DefaultValues.XPositionObstacles }, new(DefaultValues.MinimumRandHeightObstacles, DefaultValues.MaxRandHeightObstacles) { X = DefaultValues.XPositionObstacles } };
            _gameOver = false;
            _soloRand = new Random();
            Console.CursorVisible = false;
        }

#if !solo
        // Usage of unmanaged code! Potentially dangerous
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
#endif
        private void GenereateGameScene()
        {

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (i == 0 || i == Height - 1)
                    {
                        Console.Write("*");
                    }

                    else if (i == _p.Y && j == _p.X)
                    {
                        Console.Write("*");
                    }
                    else if (i >= 0 && i < _obstacles[0].Height && j == _obstacles[0].X)
                    {
                        Console.Write("*");
                    }
                    else if (i >= Height - 1 - _obstacles[1].Height && i <= Height - 1 && j == _obstacles[1].X)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
        }

        private void DetectCollision()
        {
            if (_p.Y >= 0 && _p.Y < _obstacles[0].Height && _p.X == _obstacles[0].X || _p.Y >= Height - 1 - _obstacles[1].Height && _p.Y <= Height - 1 && _p.X == _obstacles[1].X)
            {
                _gameOver = true;
            }

            if (_p.Y >= Height || _p.Y <= 0)
            {
                _gameOver = true;
            }
        }

#if !solo
        private void MovePlayer(Point defPnt)
        {
            _p.Y++;

            if (defPnt.Y < 300)
            {
                _p.Y -= 2;
            }
        }
#endif

#if solo
        private void MovePlayerSolo()
        {
            _p.Y += (_soloRand.Next(10) % 2 == 0) ? 2 : 0;
        }
#endif
        private void MoveObstacles()
        {
            foreach (Obstacle ob in _obstacles)
            {
                if (ob.X < _p.X)
                {
                    ob.X = DefaultValues.XPositionObstacles + 10;
                    ob.Height = ob._rnd.Next(DefaultValues.MinimumRandHeightObstacles, DefaultValues.MaxRandHeightObstacles);
                }
                else
                {
                    ob.X -= 1;
                }
            }
        }

        public void GenerateArea()
        {
            while (_gameOver == false)
            {
#if !solo
                Point defPnt = new Point();

                GetCursorPos(ref defPnt);
#endif

                GenereateGameScene();

                DetectCollision();

#if !solo

                MovePlayer(defPnt);
#else
                MovePlayerSolo();
#endif
                MoveObstacles();

                Thread.Sleep(100);
                Console.Clear();
            }

            Console.Clear();
            Console.WriteLine("GAME OVER!!");
            Console.WriteLine("Thanks for playing! Don\'t forget to star the project :)");
        }
    }
}
