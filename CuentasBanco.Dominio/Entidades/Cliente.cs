using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasBanco.Dominio.Entidades
{
    public class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
            ClienteId = Guid.NewGuid();
        }

        public Guid ClienteId { get; private set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
