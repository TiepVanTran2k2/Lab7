using Lab7.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Lab7.AppDbContext
{
    public class DbContextApp : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextApp(DbContextOptions<DbContextApp> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Product> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextApp).Assembly);
        }
    }
}
