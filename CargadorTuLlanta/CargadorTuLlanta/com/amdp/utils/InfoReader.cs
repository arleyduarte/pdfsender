using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace CargadorTuLlanta.com.amdp.utils
{
    class InfoReader
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InfoReader));


        public static String getLineInfo(String line, int startIndex, int lenght)
        {
            String info = "";
            try
            {



                int maxLenght = line.Length;
                int currentLenght = startIndex + lenght;

                if (currentLenght <= maxLenght)
                {
                    info = line.Substring(startIndex, lenght);
                }
                else
                {
                    int value = line.Length - startIndex;
                    info = line.Substring(startIndex, value);
                }

               


                if (info.StartsWith("   \0\0\0\0\0\0\0"))
                {
                    info = "";
                }

            }
            catch (Exception)
            {
                log.Error("ArgumentOutOfRange: Possition:" + startIndex + " Lenght: " + lenght + ":" + line);
            }
            return info.Trim();
        }

    }
}
