using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicTotalizator.BLL.Helpers
{
    public static class ConfigurationServices
    {
        public static void ConfigureServices(IServiceCollection services, string connectionDbString)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionDbString)
            );
        }
    }
}
