using CuentasBanco.Aplicacion.Contratos;
using CuentasBanco.Aplicacion.DTO.Request;
using CuentasBanco.Aplicacion.DTO.Response;
using CuentasBanco.Dominio.Contratos;
using CuentasBanco.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Aplicacion.CasosDeUso
{
    public class CuentaServicio : IServicioListar<DTOCuenta>,
        IServicioCrear<DTOGuardarCuenta>,
        IServicioEditar<DTOGuardarCuenta>,
        IServicioBorrar
    {

        private readonly IRepositorioGenerico<Cuenta> _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public CuentaServicio(IRepositorioGenerico<Cuenta> repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DTOCuenta>> ListarTodo()
        {
            var res = await _repositorio.ListarTodo();
            var cuentas = res.ToList().Select(c => new DTOCuenta
            {
                CuentaId = c.CuentaId,
                Numero = c.Numero,
                Tipo = c.Tipo,
                Saldo = c.Saldo,
                Activa = c.Activa,
                Cliente = new DTOCliente
                {
                    ClienteId = c.Cliente.ClienteId,
                    Identificacion = c.Cliente.Identificacion,
                    Nombre = c.Cliente.Nombre,
                    FechaNacimiento = c.Cliente.FechaNacimiento
                }
            }).ToList();

            return cuentas;
        }

        public async Task<DTOGuardarCuenta> Insertar(DTOGuardarCuenta dtoCuenta)
        {
            ValidarDto(dtoCuenta);

            Cuenta cuenta = new Cuenta
            {
                Numero = dtoCuenta.Numero,
                Tipo = dtoCuenta.Tipo,
                Saldo = dtoCuenta.Saldo,
                ClienteId = dtoCuenta.ClienteId,
                Activa = dtoCuenta.Activa
            };

            _repositorio.Insertar(cuenta);
            await _unitOfWork.GuardarCambios();
            return dtoCuenta;
        }

        public async Task<DTOGuardarCuenta> Editar(DTOGuardarCuenta dtoCuenta, Guid id)
        {
            ValidarDto(dtoCuenta);

            var cuenta = await BuscarCuentaEditar(id);

            cuenta.Numero = dtoCuenta.Numero;
            cuenta.Tipo = dtoCuenta.Tipo;
            cuenta.Activa = dtoCuenta.Activa;
            cuenta.Saldo = dtoCuenta.Saldo;

            _repositorio.Editar(cuenta);
            await _unitOfWork.GuardarCambios();
            return dtoCuenta;
        }

        public async Task Eliminar(Guid id)
        {
            await _repositorio.Eliminar(id);
            await _unitOfWork.GuardarCambios();
        }

        public void ValidarDto(object dtoCuenta)
        {
            if (dtoCuenta == null)
            {
                throw new ArgumentNullException("La Cuenta es requerida.");
            }
        }

        public async Task<Cuenta> BuscarCuentaEditar(Guid id)
        {
            var cuenta = await _repositorio.ListarPorId(id);

            if (cuenta == null)
            {
                throw new KeyNotFoundException("La cuenta no existe.");
            }

            return cuenta;
        }
    }
}
