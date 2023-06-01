using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasBanco.Dominio.Entidades
{
    public class Cuenta
    {
        public Cuenta()
        {
            CuentaId = Guid.NewGuid();
        }

        public Guid CuentaId { get; private set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public bool Activa { get; set; }
        public decimal Saldo { get; set; }
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
