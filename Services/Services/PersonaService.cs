using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PersonaService
    {
        private PersonaRepository repositoryPersona;

        public PersonaService(string connectionString)
        {
            this.repositoryPersona = new PersonaRepository(connectionString);
        }

        public string insertarPersona(PersonaModel persona)
        {
            return validarDatosPersona(persona) ? repositoryPersona.insertarPersona(persona) : throw new Exception("Error en la validación");
        }

        public string modificarPersona(PersonaModel persona, int id)
        {
            if (repositoryPersona.consultarPersona(id) != null)
            {
                return validarDatosPersona(persona) ? repositoryPersona.modificarPersona(persona, id) : throw new Exception("Error en la validación");
            }
            else
            {
                return "No se encontraron los datos de esta persona";
            }
        }

        public string eliminarPersona(int id)
        {
            return repositoryPersona.eliminarPersona(id);
        }

        public PersonaModel consultarPersona(int id)
        {
            return repositoryPersona.consultarPersona(id);
        }

        public IEnumerable<PersonaModel> listarPersona()
        {
            return repositoryPersona.listarPersona();
        }

        private bool validarDatosPersona(PersonaModel persona)
        {
            if (persona.nombre.Trim().Length < 3)
            {
                return false;
            }
            return true;
        }
    }


}

