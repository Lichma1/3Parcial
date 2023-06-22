using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class PersonaModel
    {
        public int Id { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string tipodoc { get; set; }

        public string documento { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }

        public string mail { get; set; }

        public bool estado { get; set; }

    }
}
