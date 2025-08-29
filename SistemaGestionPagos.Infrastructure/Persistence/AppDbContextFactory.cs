using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SistemaGestionPagos.Infrastructure.Persistence;

namespace SistemaGestionPagos.Infrastructure.Persistence
{
    // Se usa por dotnet-ef en tiempo de diseño para crear el DbContext
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Usa tu conexión de Render (o una variable de entorno PGCONN para no hardcodear)
            var connString =
                Environment.GetEnvironmentVariable("PGCONN") ??
                "Host=dpg-d2ohg5ffte5s738adtv0-a.oregon-postgres.render.com;" +
                "Database=db_sistemagestionpagos;" +
                "Username=db_sistemagestionpagos_user;" +
                "Password=pWpaXN9eGWNcVXk19r9c85Y62ZDqsy3W;" +
                "SSL Mode=Require;Trust Server Certificate=true";

            optionsBuilder.UseNpgsql(connString, npg =>
            {
                // opcional, pero ordenado:
                npg.MigrationsHistoryTable("__EFMigrationsHistory", "public");
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
