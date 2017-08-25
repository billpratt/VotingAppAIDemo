using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.AspNetCore.Http;

namespace VotingApp.TelemetryInitializers
{
    public class UserIdTelemetryInitializer : ITelemetryInitializer
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserIdTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var context = _httpContextAccessor.HttpContext;
            var cookies = context.Request.Cookies;

            context.Request.Cookies.TryGetValue(Constants.UserCookieKey, out var userId);
            if(userId != null)
            {
                telemetry.Context.User.Id = userId;
            }
        }
    }
}
