using CuentasBanco.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Infraestructura.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private DbContext _contexto;

        public RepositorioGenerico(DbContext contexto) => _contexto = contexto;

        public async Task<T> ListarPorId(Guid id) =>
            await _contexto.Set<T>().FindAsync(id);


        public async Task<IList<T>> ListarTodo() =>
            await _contexto.Set<T>().ToListAsync();

        public void Insertar(T entidad) =>
            _contexto.Set<T>().Add(entidad);


        public void Editar(T entidad) =>
            _contexto.Entry(entidad).State = EntityState.Modified;

        public async Task Eliminar(Guid id)
        {
            T entidad = await _contexto.Set<T>().FindAsync(id);
            _contexto.Set<T>().Remove(entidad);
        }
    }
}
