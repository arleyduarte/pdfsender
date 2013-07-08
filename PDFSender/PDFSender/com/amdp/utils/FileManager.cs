﻿using System;
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


        public void deleteFiles(String path, String extensions)
        {


            foreach (String file in getFilesOfInputDirectory(path, extensions))
            {
                deleteSourceFile(file);
            }


        }




        public ArrayList getFilesOfInputDirectory(String inptuFolder, String extension)
        {
            ArrayList filePaths = new ArrayList();
            List<string> _extensions = new List<string> { extension};

            try
            {
                foreach (String ext in _extensions)
                {
                    foreach (string subFile in Directory.GetFiles(inptuFolder, ext))
                    {
                        if (!filePaths.Contains(subFile))
                        {
                            filePaths.Add(subFile);
                        }

                    }

                }


            }
            catch (Exception fe)
            {
                log.Error(fe.Message);
            }


            return filePaths;
        }


        public void deleteSourceFile(String file)
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

        public ArrayList getFilesOfInputDirectory()
        {
            ArrayList filePaths = new ArrayList();
            String inputFolder = Configuracion.Default.SOURCE_FILE;

            try
            {

                    foreach (string subFile in Directory.GetFiles(inputFolder))
                    {
                        if (!filePaths.Contains(subFile))
                        {
                            filePaths.Add(subFile);
                        }

                    }

                


            }
            catch (Exception fe)
            {
                log.Error(fe.Message);
            }


            return filePaths;
        }
    }
}
