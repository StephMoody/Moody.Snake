using System;

namespace Moody.Snake.Model
{
    internal class Field
    {
        private FieldContent _content;

        public void Initialize(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
        
        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public FieldContent Content
        {
            get => _content;
            set
            {
                _content = value;
                ContentUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ContentUpdated;
    }
}