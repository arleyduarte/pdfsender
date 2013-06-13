using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CargadorTuLlanta.com.amdp.utils;
using System.Collections;
using log4net;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    class AdapterPrecio : IAdapterEntity
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AdapterPrecio));

        public IEntity getEntity(String line)
        {
            Precios precios = new Precios();
            precios.CodProducto = InfoReader.getLineInfo(line, 0, 15);
            precios.PrecioPublico = InfoReader.getLineInfo(line, 15, 8);

            fillPrecios(precios, line.Substring(23, line.Length - 23));

            return precios;
        }


        public ArrayList getEntites()
        {
            log.Info("Cargando AdapterPrecio ");
            DatFileReader datFileReader = new DatFileReader();
            return datFileReader.getEntities(this, Configuracion.Default.Plano_Precios);
        }

        private  void fillPrecios(Precios precios, String line)
        {
            String codCategoria = "";
            String precio = "";

            int contador  = 0;
            int LENGTH_COD_CATEGORIA = 3;
            int contadorCodCategoria = 0;

            int LENGTH_PRECIO = 8;
            int contadorPrecio = 0;

            while (contador < line.Length)
            {
                codCategoria = InfoReader.getLineInfo(line, contadorCodCategoria, LENGTH_COD_CATEGORIA);

                contadorPrecio = contadorCodCategoria + LENGTH_COD_CATEGORIA;

                precio = InfoReader.getLineInfo(line, contadorPrecio, LENGTH_PRECIO);


                contadorCodCategoria = contadorPrecio + LENGTH_PRECIO;
                if (codCategoria.Length != 0 && precio.Length != 0)
                {
                    precios.addPrecio(codCategoria, precio);
                }
                else
                {
                    break;
                }

                contador++;
            }


        }
    }
}
