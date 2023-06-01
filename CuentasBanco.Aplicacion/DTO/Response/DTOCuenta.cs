using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasBanco.Aplicacion.DTO.Response
{
    public class DTOCuenta
    {
        public Guid CuentaId { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public bool Activa { get; set; }
        public decimal Saldo { get; set; }
        public DTOCliente Cliente { get; set; }
    }
}
