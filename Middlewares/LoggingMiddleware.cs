

using CarsShop.Services.Auth;
using System.Diagnostics;
using System.Security.Claims;

namespace CarsShop.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            Dictionary<string, object> scopeValues = GetSopeValues(context);

            using (_logger.BeginScope(scopeValues))
            {
                await next(context);

                stopwatch.Stop();

                _logger.LogInformation(
                    "HTTP responded {StatusCode} in {ElapsedMs} ms",
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds
                );
            }
        }

        private static Dictionary<string, object> GetSopeValues(HttpContext context)
        {
            var requestId = Guid.NewGuid();
            var user = context.User;
            var scopeValues = new Dictionary<string, object>
            {
               ["RequestId"] = requestId,
                ["RequestPath"] = context.Request.Path.ToString(),
                ["Method"] = context.Request.Method
            };

            if (user.Identity.IsAuthenticated)
            {
                var email = GetClaim(user, ClaimTypes.Email);
                var userId = GetClaim(user, AuthService.ClaimIdKey);
                scopeValues.Add("Email", email);
                scopeValues.Add("UserId", userId);
            }

           
            return scopeValues;
        }

        private static string GetClaim(ClaimsPrincipal user, string type)
        {
                var value = user.FindFirst(type)?.Value;
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            return "$Claim is not found by the key ${type}";
        }
    }
}