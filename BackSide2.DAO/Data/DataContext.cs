using System.Threading;
using System.Threading.Tasks;
using Auga.DAO.Entities;
using Auga.DAO.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Auga.DAO.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasOne(l => l.Seller).WithMany(u => u.Items);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ChatConnectedUser> ChatConnectedUsers { get; set; }
        public DbSet<GameWaitingUser> GameWaitingUsers { get; set; }
    }
}