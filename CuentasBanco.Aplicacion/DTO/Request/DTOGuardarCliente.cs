using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CuentasBanco.Aplicacion.DTO.Request
{
    public class DTOGuardarCliente
    {
        [Required(ErrorMessage = "La identificacion es requerida.")]
        [StringLength(20)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Fecha de Nacimiento es requerida.")]
        public DateTime FechaNacimiento { get; set; }
    }
}
