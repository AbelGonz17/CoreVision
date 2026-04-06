using CoreVision.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreVision.Infrastructure
{
    public static  class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructure ( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreVisionDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });

            return services;
        }
    }
}