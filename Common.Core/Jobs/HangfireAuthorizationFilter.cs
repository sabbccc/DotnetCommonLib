using Hangfire.Dashboard;

namespace Common.Core.Jobs
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Example: allow all for simplicity
            // Replace with proper authentication logic
            return true;
        }
    }
}
