using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Dominio.Contratos
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IList<T>> ListarTodo();

        Task<T> ListarPorId(Guid id);

        void Insertar(T entidad);

        void Editar(T entidad);

        Task Eliminar(Guid id);
    }
}
