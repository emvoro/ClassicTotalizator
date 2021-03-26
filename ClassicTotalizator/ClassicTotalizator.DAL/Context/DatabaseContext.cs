using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Bet> Bets { get; set; }
        
        public DbSet<BetPool> BetPools { get; set; }
        
        public DbSet<Event> Events { get; set; }
        
        public DbSet<Participant> Participants { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Transaction> Transactions { get; set; }
        
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>().HasKey(s => s.Id);

            builder.Entity<Account>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Account>()
                .HasMany(s => s.BetsHistory)
                .WithOne(s => s.Account)
                .HasForeignKey(s => s.Account_Id);

            builder.Entity<Account>()
                .HasMany(s => s.Messages)
                .WithOne(s => s.Account)
                .HasForeignKey(s => s.Account_Id);

            builder.Entity<Bet>().HasKey(s => s.Id);

            builder.Entity<Bet>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<BetPool>().HasKey(s => s.Event_Id);

            builder.Entity<BetPool>()
                .HasMany(s => s.Bets)
                .WithOne(s => s.BetPool)
                .HasForeignKey(s => s.Event_Id);

            builder.Entity<Event>().HasKey(s => s.Id);

            builder.Entity<Event>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Participant>().HasKey(s => s.Id);

            builder.Entity<Participant>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Participant>()
                .HasMany(s => s.Parameters)
                .WithOne(s => s.Participant)
                .HasForeignKey(s => s.Participant_Id);

            builder.Entity<Participant>()
                .HasMany(s => s.Players)
                .WithOne(s => s.Participant)
                .HasForeignKey(s => s.Participant_Id);

            builder.Entity<Player>().HasKey(s => s.Id);

            builder.Entity<Player>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Transaction>().HasKey(s => s.Id);

            builder.Entity<Transaction>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Wallet>().HasKey(s => s.Account_Id);

            builder.Entity<Wallet>()
                .HasMany(s => s.TransactionsHistory)
                .WithOne(s => s.Wallet)
                .HasForeignKey(s => s.Wallet_Id);

            builder.Entity<Parameter>().HasKey(s => s.Id);

            builder.Entity<Parameter>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Sport>().HasKey(s => s.Id);

            builder.Entity<Sport>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<Message>().HasKey(s => s.Id);

            builder.Entity<Message>().HasIndex(s => s.Id).IsUnique();
        }
    }
}
