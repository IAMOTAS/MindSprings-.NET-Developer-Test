using Microsoft.EntityFrameworkCore;

namespace MindSprings_.NET_Developer_Test.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<TranslationContents> Translations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TranslationContents>().ToTable("Translations");
            // Add any additional configurations here
        }
    }
}
