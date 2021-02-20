using System;

namespace Moody.Common.Base
{
    public class UpdateableProperty<T>
    {
        private T _value;

        public event EventHandler<ValueChangedEventArgs<T>> ValueUpdated;
        
        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value))
                    return;

                T oldValue = _value;
                _value = value;
                var valueChangedEventArgs = new ValueChangedEventArgs<T>(value, oldValue);
                ValueUpdated?.Invoke(this, valueChangedEventArgs);
            }
        }
    }
}