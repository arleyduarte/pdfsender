using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using System.Collections;

namespace PDFSender.com.amdp.utils
{
    class FileManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FileManager));


        public void deleteFiles(ArrayList filePaths)
        {


            foreach (String file in filePaths)
            {
                deleteSourceFile(file);
            }


        }

        private void deleteSourceFile(String file)
        {
            try
            {
                File.Delete(file);
            }
            catch (IOException fe)
            {
                log.Error("Error borrando" + file+" "+fe.Message);
            }

        }
    }
}
