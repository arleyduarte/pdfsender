using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Collections;
using System.IO;
using CargadorTuLlanta.com.amdp.tullanta;

namespace CargadorTuLlanta.com.amdp.utils
{
    class DatFileReader
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DatFileReader));

        public ArrayList getEntities(IAdapterEntity adapterEntity, String path)
        {
            log.Info("Reading the file: " + path);

            ArrayList entities = new ArrayList();
            StreamReader re = File.OpenText(path);
            string line = null;

            while ((line = re.ReadLine()) != null)
            {
                if (line.Length != 0)
                {
                    entities.Add(adapterEntity.getEntity(line));
                }
            }
            return entities;
        }
    }
}
