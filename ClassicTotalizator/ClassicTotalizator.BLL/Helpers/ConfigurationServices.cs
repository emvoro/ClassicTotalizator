using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using ClassicTotalizator.DAL.Repositories.Impl;
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

            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<ISportRepository, SportRepository>();
            services.AddTransient<IRepository<BetPool>, Repository<BetPool>>();
            services.AddTransient<IRepository<Wallet>, Repository<Wallet>>();
            services.AddTransient<IRepository<Participant>, Repository<Participant>>();
            services.AddTransient<IBetRepository, BetRepository>();
            services.AddTransient<IRepository<Event>, Repository<Event>>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IParameterRepository, ParameterRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
