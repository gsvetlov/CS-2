using System;
using System.Diagnostics;

namespace Asteroid
{
    class ConsoleLogger : ILog
    {
        public void Log(string msg) => Debug.WriteLine(msg ?? throw new ArgumentNullException(nameof(msg)));
        //public void Log(string msg) => Console.WriteLine(msg ?? throw new ArgumentNullException(nameof(msg)));
    }
}
