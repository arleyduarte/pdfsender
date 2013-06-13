using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class TuLLantaAdapter
    {
        public void updateData() {
            updateClientes();
        }

        private void updateClientes()
        {
            AdapterCliente adapterCliente = new AdapterCliente();
            adapterCliente.getEntites();

            AdapterProducto adapterProducto = new AdapterProducto();
            adapterProducto.getEntites();

            AdapterPrecio adapterPrecio = new AdapterPrecio();
            adapterPrecio.getEntites();
        }
    }
}
