using System;

namespace Moody.Common.Contracts
{
    public interface ILogManager
    {
        void Error(Exception ex, string message = null);
    }
}