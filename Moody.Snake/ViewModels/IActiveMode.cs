using System;

namespace Moody.Snake.ViewModels
{
    internal interface IActiveMode
    {
        Mode Value { get; }
        void SetValue(Mode mode);
        event EventHandler<EventArgs> ModeChanged;
    }
}