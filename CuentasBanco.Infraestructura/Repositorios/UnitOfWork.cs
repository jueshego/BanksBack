using CuentasBanco.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Infraestructura.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _contexto;

        public UnitOfWork(DbContext contexto) => _contexto = contexto;

        public Task<int> GuardarCambios()
        {
            return _contexto.SaveChangesAsync();
        }
    }
}
