using System.Security.Claims;

namespace Projects.Api.Middlewares
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            context.Items["UserId"] = userId ?? "Anonymous";

            await _next(context);
        }
    }
}
