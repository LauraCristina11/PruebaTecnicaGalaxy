
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGalaxy.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.EntityFrameworkCore
{
    public class AplicationDbContext: IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options):
            base(options)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<TipoIdentidad> TipoIdentidad { get; set; }
        
    }
}
