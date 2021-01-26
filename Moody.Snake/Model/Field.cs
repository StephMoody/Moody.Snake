using System;

namespace Moody.Snake.Model
{
    internal class Field
    {
        private FieldContent _content;

        public void Initialize(int x, int y)
        {
            Row = x;
            Column = y;
        }
        
        public int Row { get; private set; }

        public int Column { get; private set; }

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