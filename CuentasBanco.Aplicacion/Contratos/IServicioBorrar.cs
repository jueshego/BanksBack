using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Aplicacion.Contratos
{
    public interface IServicioBorrar
    {
        Task Eliminar(Guid id);
    }
}
