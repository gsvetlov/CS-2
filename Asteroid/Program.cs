using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new Form();
            form.MinimumSize = new System.Drawing.Size(800, 600);
            form.MaximumSize = new System.Drawing.Size(800, 600);
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Asteroids";

            Game.Init(form);
            form.Show();
            //Game.Draw();
            Application.Run(form);
        }
    }
}
