using ClassicTotalizator.DAL.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ClassicTotalizator.BLL.Helpers
{
    public static class ConfigurationServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
        }
    }
}
