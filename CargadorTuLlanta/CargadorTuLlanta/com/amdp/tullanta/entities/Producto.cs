using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class Producto : IEntity
    {

        public String Cod { get; set; }
        public String Ancho { get; set; }
        public String Alto { get; set; }
        public String Perfil { get; set; }
        public String CodMarca { get; set; }
        public String NomMarca { get; set; }
        public String CodDiseno { get; set; }
        public String NomDiseno { get; set; }
        public String Promocion { get; set; }
        public String UTQG { get; set; }
        public String Velocidad { get; set; }
        public String Carga { get; set; }
        public String Existencia { get; set; }


    }
}
