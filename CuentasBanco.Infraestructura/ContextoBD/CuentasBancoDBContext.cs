using CuentasBanco.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasBanco.Infraestructura.ContextoBD
{
    public class CuentasBancoDBContext : DbContext
    {
        public CuentasBancoDBContext(DbContextOptions<CuentasBancoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CuentasBancoDB;Trusted_Connection=True;");
        }
    }
}
