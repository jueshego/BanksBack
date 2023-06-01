using CuentasBanco.Aplicacion.Contratos;
using CuentasBanco.Aplicacion.DTO.Request;
using CuentasBanco.Aplicacion.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuentasBanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly IServicioListar<DTOCuenta> _servicioListar;

        private readonly IServicioCrear<DTOGuardarCuenta> _servicioCrear;

        private readonly IServicioEditar<DTOGuardarCuenta> _servicioEditar;

        private readonly IServicioBorrar _servicioBorrar;

        public CuentasController(IServicioListar<DTOCuenta> servicioListar,
            IServicioCrear<DTOGuardarCuenta> servicioCrear,
            IServicioEditar<DTOGuardarCuenta> servicioEditar,
            IServicioBorrar servicioBorrar)
        {
            _servicioListar = servicioListar;
            _servicioCrear = servicioCrear;
            _servicioEditar = servicioEditar;
            _servicioBorrar = servicioBorrar;
        }

        // GET: api/<CuentaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOCuenta>>> Get()
        {
            return Ok(await _servicioListar.ListarTodo());
        }

        // POST api/<CuentaController>
        [HttpPost]
        public async Task<ActionResult<DTOGuardarCuenta>> Post([FromBody] DTOGuardarCuenta dtoCuenta)
        {
            var cuenta = await _servicioCrear.Insertar(dtoCuenta);
            return Created($"~api/cuentas/{cuenta.Numero}", cuenta);
        }

        // PUT api/<CuentaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DTOGuardarCliente>> Put(Guid id, [FromBody] DTOGuardarCuenta dtoCuenta)
        {
            return Ok(await _servicioEditar.Editar(dtoCuenta, id));
        }

        // DELETE api/<CuentaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servicioBorrar.Eliminar(id);
            return new EmptyResult();
        }
    }
}
