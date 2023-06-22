using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using ParcialOptativo.Model;
using Infraestructure.Models;
using Services.Services;

namespace ParcialOptativo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonaController : ControllerBase
    {
        private PersonaService personaService;
        private IConfiguration configuration;

        public PersonaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.personaService = new PersonaService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("ListarPersona/")]
        public ActionResult<List<PersonaModel>> ListarPersona()
        {
            var resultado = this.personaService.listarPersona();
            return Ok(resultado);
        }

        [HttpGet("ConsultarPersona/")]
        public ActionResult<PersonaModel> ConsultarPersona(int id)
        {
            var resultado = this.personaService.consultarPersona(id);
            return Ok(resultado);
        }

        [HttpPost("InsertarPersona/")]
        public ActionResult<string> InsertarPersona(PersonaModel persona)
        {
            var resultado = this.personaService.insertarPersona(new Infraestructure.Models.PersonaModel
            {
                nombre = persona.nombre,
                apellido = persona.apellido,
                mail = persona.mail,
                telefono = persona.telefono,
                direccion = persona.direccion,
                tipodoc = persona.tipodoc,
                documento = persona.documento,
                estado = persona.estado,
            });
            return Ok(resultado);
        }

        [HttpPut("ModificarPersona/")]
        public ActionResult<string> ModificarPersona(PersonaModel persona, int id)
        {
            var resultado = this.personaService.modificarPersona(new Infraestructure.Models.PersonaModel
            {
                nombre = persona.nombre,
                apellido = persona.apellido,
                mail = persona.mail,
                telefono = persona.telefono,
                direccion = persona.direccion,
                tipodoc = persona.tipodoc,
                documento = persona.documento,
                estado = persona.estado,
            }, id);
            return Ok(resultado);
        }

        [HttpDelete("EliminarPersona/")]
        public ActionResult EliminarPersona(int id)
        {
            var resultado = this.personaService.eliminarPersona(id);
            return Ok(resultado);
        }
    }
}
