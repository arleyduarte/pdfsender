using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CargadorTuLlanta.com.amdp.utils;
using log4net;
using System.Collections;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class AdapterProducto : IAdapterEntity
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AdapterProducto));

        public IEntity getEntity(String line){
            Producto producto = new Producto();

            producto.Cod = InfoReader.getLineInfo(line, 0, 15);
            producto.Ancho = InfoReader.getLineInfo(line, 15, 10);
            producto.Alto = InfoReader.getLineInfo(line, 25, 5);
            producto.Perfil = InfoReader.getLineInfo(line, 30, 5);
            producto.CodMarca = InfoReader.getLineInfo(line, 35, 6);
            producto.NomMarca = InfoReader.getLineInfo(line, 41, 20);
            producto.CodDiseno = InfoReader.getLineInfo(line, 61, 6);
            producto.NomDiseno = InfoReader.getLineInfo(line, 67, 20);
            producto.Promocion = InfoReader.getLineInfo(line, 87, 2);
            producto.UTQG = InfoReader.getLineInfo(line, 89, 20);
            producto.Velocidad = InfoReader.getLineInfo(line, 109, 2);
            producto.Carga = InfoReader.getLineInfo(line, 111, 3);
            producto.Existencia = InfoReader.getLineInfo(line, 114, 6);

            return producto;
        }

        public ArrayList getEntites()
        {
            log.Info("Cargando Productos ");
            DatFileReader datFileReader = new DatFileReader();
            return datFileReader.getEntities(this, Configuracion.Default.Plano_Productos);
        }
    }
}
