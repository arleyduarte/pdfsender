using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class Precios : IEntity
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Precios));

        public String CodProducto { get; set; }
        public String PrecioPublico { get; set; }

        Dictionary<string, string> precios = new Dictionary<string, string>();

        public void addPrecio(String codCategoria, String precio)
        {

            precios.Add(codCategoria, precio);

        }

        public String getPrecio(String codCategoria)
        {
            String precio = "";
            if (precios.ContainsKey(codCategoria))
            {
                precio = precios[codCategoria];
                log.Info("Precios getPrecio:" + codCategoria + " precio: " + precio);
            }

            return precio;
        }

    }
}
