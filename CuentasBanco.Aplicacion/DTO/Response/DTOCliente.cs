using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasBanco.Aplicacion.DTO.Response
{
    public class DTOCliente
    {
        public Guid ClienteId { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
