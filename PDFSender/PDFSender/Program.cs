using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFSender.com.amdp.utils;
using PDFSender.com.amdp.pdfsender;
using PDFSender.com.amdp.pdfsender.pdf;

namespace PDFSender
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerReportFileAdapter df = new CustomerReportFileAdapter();
            String filePath = Configuracion.Default.SOURCE_FILE;
            CustomerReportPDFAdapter pdfA = new CustomerReportPDFAdapter();
            CustomerReport cr = new CustomerReport();
            df.fillCustomerReport(cr, filePath);

            pdfA.buildPDFCustomerReport(cr);
        }
    }
}
