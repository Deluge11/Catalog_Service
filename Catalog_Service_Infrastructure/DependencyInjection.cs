using Catalog_Service_Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog_Service_Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                connectionString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
    }
}
