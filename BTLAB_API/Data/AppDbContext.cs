using BTLAB_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BTLAB_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Publishers> Pulishers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasKey(bc => new { bc.BookID, bc.AuthorID });

            modelBuilder.Entity<Book_Author>()
                .HasOne(bc => bc.Books)
                .WithMany(b => b.Book_Authors)
                .HasForeignKey(bc => bc.BookID);

            modelBuilder.Entity<Book_Author>()
                .HasOne(bc => bc.Author)
                .WithMany(a => a.Book_Authors)
                .HasForeignKey(bc => bc.AuthorID);

        }  
    }
}
