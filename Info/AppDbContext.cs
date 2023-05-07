using Data;
using Microsoft.EntityFrameworkCore;

namespace Info
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OutboxMessage>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .Metadata
                .IsPrimaryKey();
            
            modelBuilder.Entity<Product>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .Metadata
                .IsPrimaryKey();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .Metadata
                .IsUniqueIndex();

        }
    }
}