using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFSender.com.amdp.utils;
using PDFSender.com.amdp.pdfsender;
using PDFSender.com.amdp.pdfsender.pdf;
using com.amdp.notificadoremail;
using System.Collections;
using log4net;

namespace PDFSender
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        private static FileManager fileManager = new FileManager();


        static void Main(string[] args)
        {
            log.Info(Constants.TEXT_INDICATOR);
            cleanDirectory();
            foreach (String fileSource in fileManager.getFilesOfInputDirectory())
            {
                if (processReport(fileSource))
                {
                    fileManager.deleteSourceFile(fileSource);
                }
            }

            log.Info(Constants.TEXT_INDICATOR);
            Environment.Exit(0);
        }

        static void cleanDirectory()
        {
            fileManager.deleteFiles(Configuracion.Default.DESTINATION_FOLDER, "*.pdf");
        }

        static bool processReport(String fileSource)
        {
            CustomerReport cr = new CustomerReport();
            CustomerReportFileAdapter fileAdapter = new CustomerReportFileAdapter();
            CustomerReportPDFAdapter pdfAdapter = new CustomerReportPDFAdapter();
            CustomerReportMailAdapter mailAdapter = new CustomerReportMailAdapter();
            NotificadorEmail notificadorEmail = new NotificadorEmail();
 
            if (fileAdapter.fillCustomerReport(cr, fileSource))
            {
                log.Info("El archivo PMAIL fue leido exitosamente");
                if (cr.ReportInfo.Formato == Constants.PDF_FORMATO)
                {
                    if (pdfAdapter.buildReport(cr))
                    {
                        log.Info("El Reporte fue construido exitosamente");
                    }
                }
                else 
                {
                    CustomerReportTextAdapter textAdapter = new CustomerReportTextAdapter();

                    if (textAdapter.buildReport(cr))
                    {
                        log.Info("El Reporte fue construido exitosamente");
                    }
                }
                
                if (NotificadorEmail.validarEmail(cr.ReportInfo.Remite))
                {
                    notificadorEmail.Remite = cr.ReportInfo.Remite;
                }
   
                notificadorEmail.enviarMensaje(mailAdapter.getMailMessage(cr));
                return true;

            }

            return false;

        }
    }
}
