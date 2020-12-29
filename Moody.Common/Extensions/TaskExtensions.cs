using System;
using System.Threading;
using System.Threading.Tasks;
using Moody.Common.Contracts;

namespace Moody.Common.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetAsync(this Task task, ILogManager logManager)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                logManager.Error(e);
            }
        }

        public static void ExecuteWithoutWaiting(this Task task, ILogManager logManager)
        {
            try
            {
                SynchronizationContext.Current.Post(async (o) => { await task;},null);
            }
            catch (Exception e)
            {
                logManager.Error(e);
            }
        }
    }
}