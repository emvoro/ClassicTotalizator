using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClassicTotalizator.BLL.Helpers
{
    public static class ConfigurationServices
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("DatabaseContext")),
                ServiceLifetime.Transient);
        }
    }
}
