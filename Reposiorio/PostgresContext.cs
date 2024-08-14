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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<PostgresContext>().Build();
            optionsBuilder.UseNpgsql(config["ConnectionString"]);
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
