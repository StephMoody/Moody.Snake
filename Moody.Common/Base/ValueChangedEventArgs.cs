using System;
using System.Threading;

namespace Moody.Common.Base
{
    public class ValueChangedEventArgs<T> : System.EventArgs
    {
        public ValueChangedEventArgs(T newValue, T oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        public T NewValue { get; }

        public T OldValue { get; }
    }
}