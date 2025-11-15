using Hangfire;
using System.Linq.Expressions;

namespace Common.Core.Jobs
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void EnqueueJob(Expression<Action> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        public void ScheduleJob(Expression<Action> methodCall, TimeSpan delay)
        {
            BackgroundJob.Schedule(methodCall, delay);
        }
    }
}
