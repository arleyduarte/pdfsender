using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFSender.com.amdp.utils;
using PDFSender.com.amdp.pdfsender;
using PDFSender.com.amdp.pdfsender.pdf;
using com.amdp.notificadoremail;
using System.Collections;

namespace PDFSender
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
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

            if (fileAdapter.fillCustomerReport(cr, fileSource))
            {
                if (pdfAdapter.buildPDF(cr))
                {
                    notificadorEmail.enviarMensaje(mailAdapter.getMailMessage(cr));
                    return true;
                }
            }

            return false;

        }


    }
}
