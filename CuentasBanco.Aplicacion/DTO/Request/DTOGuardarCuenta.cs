using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CuentasBanco.Aplicacion.DTO.Request
{
    public class DTOGuardarCuenta
    {
        [Required(ErrorMessage = "El Numero es requerido.")]
        [StringLength(10, MinimumLength = 10,
            ErrorMessage = "El numero debe tener 10 digitos")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El tipo es requerido.")]
        [StringLength(10)]
        public string Tipo { get; set; }

        public bool Activa { get; set; }

        public decimal Saldo { get; set; }

        [Required(ErrorMessage = "El Id del cliente es requerido.")]
        public Guid ClienteId { get; set; }
    }
}
