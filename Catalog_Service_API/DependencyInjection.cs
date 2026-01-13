namespace Catalog_Service_API
{
    public static class DependencyInjection
    {
        public static void AddPresentation(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = "ExternalAuth";
                 options.DefaultChallengeScheme = "ExternalAuth";
                 options.DefaultForbidScheme = "ExternalAuth";
             })
            .AddCookie("ExternalAuth", options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });

        }
    }
}
