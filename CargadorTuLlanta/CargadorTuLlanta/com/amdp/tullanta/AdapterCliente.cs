using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CargadorTuLlanta.com.amdp.utils;
using System.Collections;
using log4net;
using System.IO;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class AdapterCliente : IAdapterEntity
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(AdapterCliente));

        public IEntity getEntity(String line)
        {
            Cliente cliente = new Cliente();

            cliente.Cod = InfoReader.getLineInfo(line, 0, 15);
            cliente.DigitoVerificacion = InfoReader.getLineInfo(line, 15, 2);
            cliente.Nombre = InfoReader.getLineInfo(line, 17, 40);
            cliente.CodCiudad = InfoReader.getLineInfo(line, 57, 6);
            cliente.Direccion = InfoReader.getLineInfo(line, 63, 30);
            cliente.Barrio = InfoReader.getLineInfo(line, 93, 30);
            cliente.Telefono = InfoReader.getLineInfo(line, 123, 20);
            cliente.Celular = InfoReader.getLineInfo(line, 143, 20);
            cliente.Email = InfoReader.getLineInfo(line, 163, 20);
            cliente.Contacto = InfoReader.getLineInfo(line, 193, 30);
            cliente.UsrWeb = InfoReader.getLineInfo(line, 223, 30);
            cliente.PwdWeb = InfoReader.getLineInfo(line, 253, 30);
            cliente.CodCategoria = InfoReader.getLineInfo(line, 283, 3);
            cliente.Estado = InfoReader.getLineInfo(line, 287, 3);
            return cliente;
        }


        public ArrayList getEntites()
        {
            log.Info("Cargando clientes ");
            DatFileReader datFileReader = new DatFileReader();
            return datFileReader.getEntities(this, Configuracion.Default.Plano_Clientes);
        }
    }
}
