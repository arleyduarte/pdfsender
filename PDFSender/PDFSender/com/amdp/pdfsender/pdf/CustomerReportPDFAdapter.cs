using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace PDFSender.com.amdp.pdfsender.pdf
{
    public class CustomerReportPDFAdapter
    {
        public void buildPDFCustomerReport(CustomerReport customerReport)
        {
            PdfReader reader = new PdfReader(customerReport.ReportInfo.Fondo);

            string destinationFile =  Configuracion.Default.DESTINATION_FOLDER + "Report_" + System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".pdf";

            customerReport.AttachFile = destinationFile;
            Font font = Constants.font;


            using (FileStream fs = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (PdfStamper stamper = new PdfStamper(reader, fs))
                {
                    PdfContentByte cb = stamper.GetOverContent(1);
                    ColumnText ct = new ColumnText(cb);


                    ct.SetSimpleColumn(-9, 0,
                              PageSize.LETTER.Width + 9, PageSize.LETTER.Height - 3,
                              18, Element.ALIGN_JUSTIFIED);

                    int initHeaderY = 720;
                    int spaceBetweenHeaders = (int) font.CalculatedSize;
                    int positionY = initHeaderY;
                    foreach (String headerLine in customerReport.getHeaders())
                    {

                        Phrase phrase = new Phrase(headerLine, font);
                        ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, 36, positionY, 0);

                        positionY = positionY + spaceBetweenHeaders;
                    }


                   

                    ct.Go();
                    stamper.Close();
                }
            }
        }
    }
}
