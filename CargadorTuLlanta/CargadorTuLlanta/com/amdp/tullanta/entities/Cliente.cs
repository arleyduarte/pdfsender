using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class Cliente : IEntity
    {
        public String Cod { get; set; }
        public String DigitoVerificacion { get; set; }
        public String Nombre { get; set; }
        public String CodCiudad { get; set; }
        public String Direccion { get; set; }
        public String Barrio { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String Email { get; set; }
        public String Contacto { get; set; }
        public String UsrWeb { get; set; }
        public String PwdWeb { get; set; }
        public String CodCategoria { get; set; }
        public String Estado { get; set; }

    }
}
