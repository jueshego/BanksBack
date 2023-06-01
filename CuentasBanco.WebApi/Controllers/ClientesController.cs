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
    public class ClientesController : ControllerBase
    {
        private readonly IServicioListar<DTOCliente> _servicioListar;
        private readonly IServicioObtenerPorId<DTOCliente> _servicioObtenerPorId;
        private readonly IServicioCrear<DTOGuardarCliente> _servicioCrear;
        private readonly IServicioEditar<DTOGuardarCliente> _servicioEditar;
        private readonly IServicioBorrar _servicioBorrar;

        public ClientesController(IServicioListar<DTOCliente> servicioListar,
            IServicioObtenerPorId<DTOCliente> servicioObtenerPorId,
            IServicioCrear<DTOGuardarCliente> servicioCrear,
            IServicioEditar<DTOGuardarCliente> servicioEditar,
            IServicioBorrar servicioBorrar)
        {
            _servicioListar = servicioListar;
            _servicioObtenerPorId = servicioObtenerPorId;
            _servicioCrear = servicioCrear;
            _servicioEditar = servicioEditar;
            _servicioBorrar = servicioBorrar;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOCliente>>> Get()
        {
            return Ok(await _servicioListar.ListarTodo());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DTOCliente>>> Get(Guid id)
        {
            return Ok(await _servicioObtenerPorId.ObtenerPorId(id));
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<ActionResult<DTOGuardarCliente>> Post([FromBody] DTOGuardarCliente dtoCliente)
        {
            var cliente = await _servicioCrear.Insertar(dtoCliente);
            return Created($"~api/clientes/{cliente.Identificacion}", cliente);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DTOGuardarCliente>> Put(Guid id, [FromBody] DTOGuardarCliente dtoCliente)
        {
            return Ok(await _servicioEditar.Editar(dtoCliente, id));
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servicioBorrar.Eliminar(id);
            return new EmptyResult();
        }
    }
}
