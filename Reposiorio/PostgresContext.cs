using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class PostgresContext: DbContext
    {
        public DbSet<Persona> PersonaContext { get; set; }
        public DbSet<Cuenta> CuentaContext { get; set; }
        public DbSet<Tipo> TipoContext { get; set; }
        public DbSet<Categoria> CategoriaContext { get; set; }
        public DbSet<Transaccion> TransaccionContext { get; set; }
        public DbSet<Estado> EstadoContext { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found in environment variables.");
            }

            // Configurar el DbContext para usar Npgsql con la cadena de conexión desde variables de entorno
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>()
                .Property(t => t.CuentaId)
                .HasColumnName("cuentaId");
             modelBuilder.Entity<Transaccion>()
                .HasOne(t => t.cuenta)
                .WithMany(c => c.Transaccions)
                .HasForeignKey(t => t.CuentaId);
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.Persona)
                .WithMany(p => p.Categorias) 
                .HasForeignKey(c => c.PersonaId);

        }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
        }
        public PostgresContext() { }
    }
}
