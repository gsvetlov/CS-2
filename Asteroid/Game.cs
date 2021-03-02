using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Asteroid.Properties;

namespace Asteroid
{
    public static class Game
    {
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static Asteroid[] _asteroids;
        private static SpaceBody[] _planets;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        static Game() { }


        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

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

            foreach (var asteroid in _asteroids)
                asteroid.Draw();

            foreach (var star in _planets)
                star.Draw(); // Переопределенный метод Draw

            _buffer.Render();
        }

        public static void Update()
        {
            foreach (var asteroid in _asteroids)
                asteroid.Update();

            foreach (var planet in _planets)
                planet.Update(); // Переопределенный метод Update
        }

        public static void Load()
        {
            var random = new Random();
            _asteroids = new Asteroid[6];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                var size = random.Next(10, 40);
                _asteroids[i] = new Asteroid(
                    new Point(random.Next(Width), random.Next(Height)),
                    new Point(random.Next(-5,5), random.Next(-5, 5)),
                    new Size(size, size));
            }
            _planets = new SpaceBody[3];
            for (int i = 0; i < _planets.Length; i++)
            {
                var size = random.Next(40,60);
                _planets[i] = new Planet(
                    new Point(random.Next(Width), random.Next(Height)),
                    new Point(random.Next(-3, 3), random.Next(-3,3)),
                    new Size(size, size));
            }

        }

    }
}
