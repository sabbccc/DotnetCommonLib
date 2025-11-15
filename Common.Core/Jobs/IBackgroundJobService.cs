using System.Linq.Expressions;

namespace Common.Core.Jobs
{
    public interface IBackgroundJobService
    {
        void EnqueueJob(Expression<Action> methodCall);
        void ScheduleJob(Expression<Action> methodCall, TimeSpan delay);
    }
}
