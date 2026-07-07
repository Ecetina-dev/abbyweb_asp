using AbbyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("Estudiante");
                entity.HasKey(e => e.Id_Estudiante);
                entity.Property(e => e.Id_Estudiante).ValueGeneratedOnAdd();
            });
        }
    }
}
