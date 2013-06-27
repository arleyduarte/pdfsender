using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using log4net;
using System.Collections;

namespace PDFSender.com.amdp.pdfsender.pdf
{
    public class CustomerReportPDFAdapter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CustomerReportPDFAdapter));
        private CustomerReport customerReport;
        private Font font = Constants.font;
        private PdfReader reader = null;
        private int pageHeight;
        private int finalPositionHeaderY;

        public void buildPDFCustomerReport(CustomerReport customerReport)
        {

            this.customerReport = customerReport;


            String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto +"_"+ System.DateTime.Now.ToString("ssffff") + ".pdf";

            font.Size = getFontSize();


            try
            {
                 reader = new PdfReader(customerReport.ReportInfo.Fondo);
                 using (FileStream fs = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
                 {
                     using (PdfStamper stamper = new PdfStamper(reader, fs))
                     {
                         PdfContentByte cb = stamper.GetOverContent(1);

                         Rectangle pageSize = reader.GetPageSize(1);
                         pageHeight = (int)pageSize.Height;


                         /*
                         ColumnText ct = new ColumnText(cb);


                         ct.SetSimpleColumn(-9, 0,
                                   PageSize.LETTER.Width + 9, PageSize.LETTER.Height - 3,
                                   18, Element.ALIGN_JUSTIFIED);

*/
                         printHeaders(cb);
                         printDetails(cb);



                        // ct.Go();
                         stamper.Close();
                     }
                 }
            }
            catch (IOException io)
            {
                log.Error("No se encontro el archivo template "+io);
            }



            customerReport.AttachFile = destinationFile;



        }


        private void printHeaders(PdfContentByte cb)
        {
            int initHeaderY = pageHeight-getTopMargin();
            int spaceBetweenHeaders = (int)font.CalculatedSize;
            int positionY = initHeaderY;

            ArrayList headers = customerReport.getHeaders();

            foreach (String headerLine in headers)
            {
                Phrase phrase = new Phrase(headerLine, font);



                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, 36, positionY, 0);
                positionY = positionY - spaceBetweenHeaders;
            }

            finalPositionHeaderY = positionY;
        }

        private void printDetails(PdfContentByte cb)
        {
            int initHeaderY = finalPositionHeaderY;
            int spaceBetweenHeaders = (int)font.CalculatedSize;
            int positionY = initHeaderY;

            ArrayList details = customerReport.getDetails();

            foreach (String detailLine in details)
            {

                if (detailLine.Length > 95)
                {
                    String texts = detailLine.Substring(94, 1);
                }


                Phrase phrase = new Phrase(detailLine, font);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, 36, positionY, 0);
                positionY = positionY - spaceBetweenHeaders;
            }
        }



        private int getFontSize()
        {
            int size = Constants.DEFAULT_SIZE;
            try
            {
                if (customerReport.ReportInfo.TamanoLetra.Length != 0)
                {
                    size = Convert.ToInt32(customerReport.ReportInfo.TamanoLetra);
                }


            }catch(Exception e){
                log.Error(e);
            }
            return size;
        }

        private int getTopMargin()
        {
            int size = Constants.DEFAULT_TOP_MARGIN;
            try
            {

                if (customerReport.ReportInfo.TopMargin.Length != 0)
                {
                    size = Convert.ToInt32(customerReport.ReportInfo.TopMargin);
                }


            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return size;
        }
    }
}
