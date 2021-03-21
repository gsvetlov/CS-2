using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Asteroid.Properties;

namespace Asteroid
{
    public static class Game
    {
        private static ILog logger;
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static UIController _controller;
        private static Timer timer;

        private static List<SpaceBody> _spaceObjs;
        private static Ship ship;

        private static int InitAsteroidCount { get; set; } = 10;

        internal static int Score { get; set; } = 0;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BufferedGraphics Buffer => _buffer;

        static Game() { }

        public static void Init(Form form)
        {
            logger = new ConsoleLogger();
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            if (Height < 200 || Height > 1080) throw new ArgumentOutOfRangeException(nameof(Height));
            if (Width < 300 || Width > 1920) throw new ArgumentOutOfRangeException(nameof(Width));

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            _controller = new UIController(form);
            _controller.Fire += BeamStart;

            Load();

            timer = new Timer
            {
                Interval = 50
            };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            _buffer.Graphics.DrawImage(new Bitmap(Resources.milky_way_galaxy, new Size(Width, Height)), 0, 0);
            _buffer.Graphics.DrawString($"Score: {Score}",
                                        new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold),
                                        Brushes.YellowGreen,
                                        10,
                                        10);

            foreach (var body in _spaceObjs)
                body.Draw();

            _buffer.Render();
        }

        public static void Update()
        {
            int count = 0;
            foreach (var body in _spaceObjs)
            {
                body.Update();
                if (body is Asteroid) count++;
            }

            Collide();
            DropDead();
            if (ship.IsTerminated)
                GameOver();

            if (count == 0)
                SpawnAsteroids(++InitAsteroidCount);
        }



        private static void Collide()
        {
            foreach (var body in _spaceObjs)
            {
                if (body.IsTerminated) continue;
                foreach (var otherBody in _spaceObjs)
                {
                    if (body != otherBody && body.HasCollision(otherBody))
                        body.Collide(otherBody);
                }
            }
        }

        private static void DropDead()
        {
            int i = -1;
            while (++i < _spaceObjs.Count)
            {
                if (_spaceObjs[i].IsTerminated)
                    _spaceObjs.RemoveAt(i--);
            }
        }

        public static void Load()
        {
            _spaceObjs = new List<SpaceBody>();

            SpawnAsteroids(InitAsteroidCount);

            int planets = 1;
            SpawnPlanets(planets);

            int medikits = 4;
            SpawnMedikits(medikits);

            ship = new Ship(
                new Point(1, Height / 2),
                new Point(3, 3),
                new Size(41, 25),
                logger.Log,
                _controller);
            _spaceObjs.Add(ship);

        }

        private static void SpawnMedikits(int medikits)
        {
            for (int i = 0; i < medikits; i++)
            {
                var size = 20;
                _spaceObjs.Add(new Medikit(
                    Utils.RandomPos(Width - size, Height - size),
                    Point.Empty,
                    new Size(size, size),
                    logger.Log));
            }
        }

        private static void SpawnPlanets(int planets)
        {
            for (int i = 0; i < planets; i++)
            {
                var size = Utils.Random(60, 90);
                _spaceObjs.Add(new Planet(
                    Utils.RandomPos(Width - size, Height - size),
                    Point.Empty,
                    new Size(size, size),
                    logger.Log));
            }
        }

        private static void SpawnAsteroids(int count)
        {

            for (int i = 0; i < count; i++)
            {
                var size = Utils.Random(10, 40);
                _spaceObjs.Add(new Asteroid(
                    Utils.RandomPos(Width - size, Height - size),
                    Utils.RandomDir(1, 5, true),
                    new Size(size, size),
                    logger.Log)); ;
            }
        }

        private static void BeamStart(object sender, EventArgs e)
        {
            Debug.WriteLine("beam fired!");
            Point origin = ship.GetFiringPosition();
            Beam beam = new Beam(origin, new Point(8, 0), new Size(20, 3), logger.Log);
            _spaceObjs.Add(beam);
        }
        private static void GameOver()
        {
            timer.Stop();
            _buffer.Graphics.Clear(Color.Black);
            _buffer.Graphics.DrawString("Game Over!",
                                   new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold),
                                   Brushes.Green,
                                   Width / 2 - 100,
                                   Height / 2 - 50);
            _buffer.Graphics.DrawString($"Your score: {Score}",
                                   new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold),
                                   Brushes.GreenYellow,
                                   Width / 2 - 80,
                                   Height / 2);
            _buffer.Render();
        }
    }
}

