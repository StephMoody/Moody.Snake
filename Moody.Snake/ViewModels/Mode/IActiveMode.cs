using System;
using Moody.Common.Base;

namespace Moody.Snake.ViewModels.Mode
{
    internal interface IActiveMode
    {
        ContentModes Value { get; }
        void SetValue(ContentModes contentModes);
        event EventHandler<ValueChangedEventArgs<ContentModes>> ModeChanged;
    }
}