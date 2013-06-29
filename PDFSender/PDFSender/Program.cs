using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFSender.com.amdp.utils;
using PDFSender.com.amdp.pdfsender;
using PDFSender.com.amdp.pdfsender.pdf;
using com.amdp.notificadoremail;

namespace PDFSender
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerReport cr = new CustomerReport();
            CustomerReportFileAdapter fileAdapter = new CustomerReportFileAdapter();
            CustomerReportPDFAdapter pdfAdapter = new CustomerReportPDFAdapter();
            CustomerReportMailAdapter mailAdapter = new CustomerReportMailAdapter();
            NotificadorEmail notificadorEmail = new NotificadorEmail();
            
            if(fileAdapter.fillCustomerReport(cr, Configuracion.Default.SOURCE_FILE)){
                if (pdfAdapter.buildPDF(cr))
                {
                    notificadorEmail.enviarMensaje(mailAdapter.getMailMessage(cr));
                }
            }
     
            Environment.Exit(0);
        }


    }
}
