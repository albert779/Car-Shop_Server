using CarsShop.Controllers;
using CarsShop.Services.Auth;
using System.Security.Claims;

namespace CarsShop.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var user = context.User!;
            var email = user.Claims.Single(x => x.Type.Equals(ClaimTypes.Email)).Value;
            var id = user.Claims.Single(x => x.Type.Equals(AuthService.ClaimIdKey)).Value;


            var logsFormatterValues = new Dictionary<string, object>()
            {
                {
                    ClaimTypes.Email.ToString().Split('/').Last(), email
                },
                {
                    AuthService.ClaimIdKey, id
                },
                {
                     "RequestId", Guid.NewGuid()
                }
            };
            using (_logger.BeginScope(logsFormatterValues))
            {

                await next(context);
            }
                
        }
    }
}
