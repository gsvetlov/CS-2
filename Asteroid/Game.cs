using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Asteroid.Properties;

namespace Asteroid
{
    public static class Game
    {        
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static List<SpaceBody> _spaceObjs;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static BufferedGraphics Buffer => _buffer;

        static Game() { }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if (Height < 200 || Height > 1080) throw new ArgumentOutOfRangeException(nameof(Height));
            if (Width < 300 || Width > 1920) throw new ArgumentOutOfRangeException(nameof(Width));

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer();
            timer.Interval = 50;
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

            foreach (var body in _spaceObjs)
                body.Draw();

            _buffer.Render();
        }

        public static void Update()
        {
            foreach (var body in _spaceObjs)
                body.Update();

            Collide();
            DropDead();
        }

        private static void Collide()
        {
            foreach (var body in _spaceObjs)
            {
                if (body.isTerminated) continue;
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
                if (_spaceObjs[i].isTerminated)
                    _spaceObjs.RemoveAt(i--);
            }
        }

        public static void Load()
        {
            _spaceObjs = new List<SpaceBody>();

            int asteroids = 10;
            for (int i = 0; i < asteroids; i++)
            {
                var size = Utils.Random(10, 40);
                _spaceObjs.Add(new Asteroid(
                    Utils.RandomPos(Width, Height),
                    Utils.RandomDir(1, 5, true),
                    new Size(size, size))); ;
            }
            int planets = 2;
            for (int i = 0; i < planets; i++)
            {
                var size = Utils.Random(40, 60);
                _spaceObjs.Add(new Planet(
                    Utils.RandomPos(Width, Height),
                    Utils.RandomDir(1, 4, true),
                    new Size(size, size)));
            }

            int beams = 4;
            for (int i = 0; i < beams; i++)
            {
                _spaceObjs.Add(
               new Beam(
               new Point(0, Utils.Random(10, Height - 10)),
               new Point(Utils.Random(5, 10), 0),
               new Size(20, 4)));
            }
        }
    }
}

