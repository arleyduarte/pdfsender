using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using PDFSender.com.amdp.pdfsender;
using System.IO;

namespace PDFSender.com.amdp.utils
{


    public class CustomerReportFileAdapter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CustomerReportFileAdapter));

        public void fillCustomerReport(CustomerReport customerReport, String filePath)
        {

            log.Info("Reading the file: " + filePath);
            ReportInfo reportInfo = new ReportInfo();


            StreamReader re = File.OpenText(filePath);
            string line = null;

            while ((line = re.ReadLine()) != null)
            {
                if (line.Length != 0)
                {
                    if (line.Contains(Constants.FONDO) && reportInfo.Fondo == null)
                    {
                        reportInfo.Fondo = getSettingValue(line, Constants.FONDO);
                    }

                    else if (line.Contains(Constants.DESTINO) && reportInfo.Destino == null)
                    {
                        reportInfo.Destino = getSettingValue(line, Constants.DESTINO);
                    }

                    else if (line.Contains(Constants.CCMail) && reportInfo.CC == null)
                    {
                        reportInfo.CC = getSettingValue(line, Constants.CCMail);
                    }

                    else if (line.Contains(Constants.REMITE) && reportInfo.Remite == null)
                    {
                        reportInfo.Remite = getSettingValue(line, Constants.REMITE);
                    }

                    else if (reportInfo.Asunto == null && line.Contains(Constants.ASUNTO))
                    {
                        reportInfo.Asunto = getSettingValue(line, Constants.ASUNTO);
                    }

                    else if (reportInfo.Texto == null && line.Contains(Constants.TEXTO))
                    {
                        reportInfo.Texto = getSettingValue(line, Constants.TEXTO);
                    }

                    else if (reportInfo.TamanoLetra == null && line.Contains(Constants.TAMANOLET))
                    {
                        reportInfo.TamanoLetra = getSettingValue(line, Constants.TAMANOLET);
                    }

                    else if (reportInfo.Orientacion == null && line.Contains(Constants.ORIENT))
                    {
                        reportInfo.Orientacion = getSettingValue(line, Constants.ORIENT);
                    }

                    else if (line.StartsWith(Constants.HEADER_INDICATOR))
                    {
                        customerReport.addHeaderLine(line);
                    }

                }
            }


            customerReport.ReportInfo = reportInfo;


        }


        private String getSettingValue(String line, String name)
        {
            String value = "";

            if (line.Contains(name))
            {
                value = line.Replace(name, "");
            }

            return value.Trim();
        }
    }
}
