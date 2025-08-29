using Microsoft.EntityFrameworkCore;
using SistemaGestionPagos.Domain.Entities;

namespace SistemaGestionPagos.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Planilla> Planillas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set;}
        public DbSet<MovimientoProyecto> MovimientosProyecto { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pago>().ToTable("Pagos");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Planilla>().ToTable("Planillas");
            modelBuilder.Entity<Proyecto>().ToTable("Proyectos");
            modelBuilder.Entity<MovimientoProyecto>().ToTable("MovimientosProyecto");
        }
    }
}
