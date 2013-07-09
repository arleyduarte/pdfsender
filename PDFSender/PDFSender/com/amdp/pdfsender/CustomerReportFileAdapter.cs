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

        public bool fillCustomerReport(CustomerReport customerReport, String filePath)
        {

            log.Info("Reading the file: " + filePath);
            ReportInfo reportInfo = new ReportInfo();
            StreamReader re = null;

            try
            {
                re = File.OpenText(filePath);
                customerReport.SourceFile = filePath;
                string line = null;




                while ((line = re.ReadLine()) != null)
                {
                    if (line.Length != 0)
                    {
                        if (line.Contains(Constants.FONDO) && reportInfo.Fondo == null)
                        {
                            reportInfo.Fondo = getSettingValue(line, Constants.FONDO);

                            if (!File.Exists(reportInfo.Fondo))
                            {
                                reportInfo.Fondo = Directory.GetCurrentDirectory() + "\\" + Constants.DEFAULT_TEMPLATE;
                            }

                        }

                        else if (line.Contains(Constants.FORMATO) && reportInfo.Formato == null)
                        {
                            reportInfo.Formato = getSettingValue(line, Constants.FORMATO);
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



                        else if (reportInfo.TamanoLetra == null && line.Contains(Constants.TAMANOLET))
                        {
                            reportInfo.TamanoLetra = getSettingValue(line, Constants.TAMANOLET);
                        }


                        else if (reportInfo.TopMargin == null && line.Contains(Constants.MARG_SUP))
                        {
                            reportInfo.TopMargin = getSettingValue(line, Constants.MARG_SUP);
                        }

                        else if (reportInfo.BottomMargin == null && line.Contains(Constants.MARG_INF))
                        {
                            reportInfo.BottomMargin = getSettingValue(line, Constants.MARG_INF);
                        }

                        else if (reportInfo.LeftMargin == null && line.Contains(Constants.MARG_IZQ))
                        {
                            reportInfo.LeftMargin = getSettingValue(line, Constants.MARG_IZQ);
                        }


                        else if (reportInfo.Orientacion == null && line.Contains(Constants.ORIENT))
                        {
                            reportInfo.Orientacion = getSettingValue(line, Constants.ORIENT);
                        }

                        else if (reportInfo.NombreAdjunto == null && line.Contains(Constants.NOM_ADJ))
                        {
                            reportInfo.NombreAdjunto = getSettingValue(line, Constants.NOM_ADJ);
                        }



                        else if (line.StartsWith(Constants.HEADER_INDICATOR))
                        {
                            customerReport.addHeaderLine(line);
                        }

                        else
                        {
                            customerReport.addDetailLine(line);
                        }

                    }
                }

                customerReport.ReportInfo = reportInfo;
                customerReport.fillTextBody();
                return true;

            }
            catch (Exception exd)
            {
                log.Info("File no found: " + filePath+" "+exd.Message);
                return false;
            }



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
