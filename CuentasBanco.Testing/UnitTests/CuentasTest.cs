using Castle.Core.Configuration;
using CuentasBanco.Aplicacion.CasosDeUso;
using CuentasBanco.Aplicacion.DTO.Request;
using CuentasBanco.Dominio.Contratos;
using CuentasBanco.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CuentasBanco.Testing.UnitTests
{
    public class CuentasTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRepositorioGenerico<Cuenta>> _mockRepoCuenta;
        private DTOGuardarCuenta DtoCuentaGuardar;

        public CuentasTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _mockRepoCuenta = new Mock<IRepositorioGenerico<Cuenta>>(MockBehavior.Strict);

            DtoCuentaGuardar = new DTOGuardarCuenta
            {
                Activa = true,
                Numero = "0123456789",
                Tipo = "Ahorros",
                Saldo = 500000,
                ClienteId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task CuandoSeEnviaUnaCuentaConUnIdQueNoExisteGeneraExcepcion()
        {
            //Arrange
            Guid cuentaIdError = new Guid();
            const string msgExepcionCuentaNoExiste = "La cuenta no existe.";

            Cuenta cuentaNull = null;

            _mockRepoCuenta.Setup(r => r.ListarPorId(cuentaIdError))
                .Returns(Task.FromResult<Cuenta>(cuentaNull));

            var cuentaServicio = new CuentaServicio(_mockRepoCuenta.Object, _mockUnitOfWork.Object);

            //Act
            var exception = await Record.ExceptionAsync(() => cuentaServicio.Editar(DtoCuentaGuardar, cuentaIdError));

            //Assert
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal(msgExepcionCuentaNoExiste, exception.Message);
        }
    }
}
