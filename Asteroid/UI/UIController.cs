using System;
using System.Windows.Forms;

namespace Asteroid
{
    public class UIController
    {
        private readonly Form form;
        public event EventHandler<PositionChangeEventArgs> ChangePositon;
        public event EventHandler Fire;
        public UIController(Form currentForm)
        {
            form = currentForm ?? throw new ArgumentNullException(nameof(form));
            form.KeyDown += Form_KeyDown;          
        }        

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Fire?.Invoke(this, EventArgs.Empty);
                    break;
                case Keys.Up:
                    ChangePositon?.Invoke(this, new PositionChangeEventArgs(Direction.Up));
                    break;
                case Keys.Down:
                    ChangePositon?.Invoke(this, new PositionChangeEventArgs(Direction.Down));
                    break;
                case Keys.Left:
                    ChangePositon?.Invoke(this, new PositionChangeEventArgs(Direction.Left));
                    break;
                case Keys.Right:
                    ChangePositon?.Invoke(this, new PositionChangeEventArgs(Direction.Right));
                    break;
                default:
                    break;
            }
        }
    }

    public class PositionChangeEventArgs : EventArgs
    {
        public Direction Direction { get; }
        public PositionChangeEventArgs(Direction direction)
        {
            Direction = direction;
        }

    }
}