using System.Security.Claims;

namespace Catalog_Service_API.Middlewares
{

    public class CreateClaimIdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateClaimIdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Request.Headers["X-User-Id"].ToString();
            var permissions = context.Request.Headers["X-Permissions"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim("permissions", permissions)
                };

                var identity = new ClaimsIdentity(claims);
                context.User = new ClaimsPrincipal(identity);
            }

            await _next(context);
        }

    }
}
