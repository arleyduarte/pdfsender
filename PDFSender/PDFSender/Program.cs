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
            
            foreach (String fileSource in fileManager.getFilesOfInputDirectory())
            {
                if (processReport(fileSource))
                {
                    fileManager.deleteSourceFile(fileSource);
                }
            }

            Environment.Exit(0);
        }

        static bool processReport(String fileSource)
        {
            CustomerReport cr = new CustomerReport();
            CustomerReportFileAdapter fileAdapter = new CustomerReportFileAdapter();
            CustomerReportPDFAdapter pdfAdapter = new CustomerReportPDFAdapter();
            CustomerReportMailAdapter mailAdapter = new CustomerReportMailAdapter();
            NotificadorEmail notificadorEmail = new NotificadorEmail();
            
            log.Info(Constants.TEXT_INDICATOR);

            if (fileAdapter.fillCustomerReport(cr, fileSource))
            {

                log.Info("El archivo PMAIL fue leido exitosamente");
                log.Info(Constants.TEXT_INDICATOR);

                if (pdfAdapter.buildPDF(cr))
                {
                    log.Info("El Reporte fue construido exitosamente");
                    log.Info(Constants.TEXT_INDICATOR);

                    notificadorEmail.Remite = cr.ReportInfo.Remite;
                    notificadorEmail.enviarMensaje(mailAdapter.getMailMessage(cr));
                    return true;
                }
            }

            return false;

        }


    }
}
