using CuentasBanco.Aplicacion.Contratos;
using CuentasBanco.Aplicacion.DTO.Request;
using CuentasBanco.Aplicacion.DTO.Response;
using CuentasBanco.Dominio.Entidades;
using CuentasBanco.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Aplicacion.CasosDeUso
{
    public class ClienteServicio : IServicioListar<DTOCliente>,
        IServicioCrear<DTOGuardarCliente>,
        IServicioEditar<DTOGuardarCliente>,
        IServicioObtenerPorId<DTOCliente>,
        IServicioBorrar
    {
        private readonly IRepositorioGenerico<Cliente> _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteServicio(IRepositorioGenerico<Cliente> repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DTOCliente>> ListarTodo()
        {
            var res = await _repositorio.ListarTodo();
            var clientes = res.ToList().Select(c => new DTOCliente
            {
                ClienteId = c.ClienteId,
                Identificacion = c.Identificacion,
                Nombre = c.Nombre,
                FechaNacimiento = c.FechaNacimiento
            }).ToList();

            return clientes;
        }

        public async Task<DTOCliente> ObtenerPorId(Guid id)
        {
            var res = await _repositorio.ListarPorId(id);
            var cliente = new DTOCliente
            {
                ClienteId = res.ClienteId,
                Identificacion = res.Identificacion,
                Nombre = res.Nombre,
                FechaNacimiento = res.FechaNacimiento
            };

            return cliente;
        }

        public async Task<DTOGuardarCliente> Insertar(DTOGuardarCliente dtoCliente)
        {
            ValidarDto(dtoCliente);

            Cliente cliente = new Cliente
            {
                Identificacion = dtoCliente.Identificacion,
                Nombre = dtoCliente.Nombre,
                FechaNacimiento = dtoCliente.FechaNacimiento
            };

            _repositorio.Insertar(cliente);
            await _unitOfWork.GuardarCambios();
            return dtoCliente;
        }

        public async Task<DTOGuardarCliente> Editar(DTOGuardarCliente dtoCliente, Guid id)
        {
            ValidarDto(dtoCliente);

            Cliente cliente = BuscarClienteExiste(id);

            cliente.Identificacion = dtoCliente.Identificacion;
            cliente.Nombre = dtoCliente.Nombre;
            cliente.FechaNacimiento = dtoCliente.FechaNacimiento;
            
            _repositorio.Editar(cliente);
            await _unitOfWork.GuardarCambios();
            return dtoCliente;
        }

        public async Task Eliminar(Guid id)
        {
            await _repositorio.Eliminar(id);
            await _unitOfWork.GuardarCambios();
        }

        public void ValidarDto(object dtoCliente)
        {
            if (dtoCliente == null)
            {
                throw new ArgumentNullException("El cliente es requerido.");
            }
        }

        public Cliente BuscarClienteExiste(Guid id)
        {
            Cliente cliente = _repositorio.ListarPorId(id).Result;

            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe.");
            }

            return cliente;
        }
    }
}
