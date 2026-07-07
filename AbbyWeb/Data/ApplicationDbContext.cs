using AbbyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable(tb => tb.UseSqlOutputClause(false));
                entity.ToTable("alumno");
                entity.HasKey(e => e.id_alumno);
                entity.Property(e => e.id_alumno).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.no_control).IsUnique();
                entity.HasOne(e => e.Carrera)
                      .WithMany()
                      .HasForeignKey(e => e.id_carrera);
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.ToTable("carrera");
                entity.HasKey(e => e.id_carrera);
            });
        }
    }
}
