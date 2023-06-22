using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ParcialOptativo.Model;
//using Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace ParcialOptativo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CuentaController : ControllerBase
    {
        private CuentaService cuentaService;
        private IConfiguration configuration;

        public CuentaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.cuentaService = new CuentaService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("ListarCuenta/")]
        public ActionResult<List<CuentaModel>> ListarCuenta()
        {
            var resultado = this.cuentaService.listarCuenta;
            return Ok(resultado);
        }

        [HttpGet("ConsultarCuenta/")]
        public ActionResult<CuentaModel> ConsultarCuenta(int id)
        {
            var resultado = this.cuentaService.consultarCuenta(id);
            return Ok(resultado);
        }

        [HttpPost("InsertarCuenta/")]
        public ActionResult<string> InsertarCuenta(CuentaModel cuenta)
        {
            var resultado = this.cuentaService.insertarCuenta(new Infraestructure.Models.CuentaModel
            {
                nombreCuenta = cuenta.nombreCuenta,
                numeroCuenta = cuenta.numeroCuenta,
                saldo = cuenta.saldo,
                limiteSaldo = cuenta.limiteSaldo,
                limiteTransferencia = cuenta.limiteTransferencia,
                estadocuenta = cuenta.estadocuenta,
                //idPersona = cuenta.idPersona,
            });
            return Ok(resultado);
        }

        [HttpPut("ModificarCuenta/")]
        public ActionResult<string> ModificarCuenta(CuentaModel cuenta, int id)
        {
            var resultado = this.cuentaService.modificarCuenta(new Infraestructure.Models.CuentaModel
            {
                nombreCuenta = cuenta.nombreCuenta,
                numeroCuenta = cuenta.numeroCuenta,
                saldo = cuenta.saldo,
                limiteSaldo = cuenta.limiteSaldo,
                limiteTransferencia = cuenta.limiteTransferencia,
                estadocuenta = cuenta.estadocuenta,
                //idPersona = cuenta.idPersona,
            }, id);
            return Ok(resultado);
        }

        [HttpDelete("EliminarCuenta/")]
        public ActionResult EliminarCuenta(int id)
        {
            var resultado = this.cuentaService.eliminarCuenta(id);
            return Ok(resultado);
        }
    }
}
