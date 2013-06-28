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
        private int topMargin;
        private int buttonMargin;
        private int leftMargin = 36;
        private int fontHeigth;
        private ArrayList tempFiles = new ArrayList();
        private ArrayList details  = new ArrayList();

        public void buildPDFCustomerReport(CustomerReport customerReport)
        {
            this.customerReport = customerReport;
            loadTemplate();

            int numOfRequiredPages = getNumberOfRequiredPages();

            for (int i = 0; i < numOfRequiredPages; i++)
            {
                buildPage();
            }

        }


        public void loadTemplate()
        {
                reader = new PdfReader(customerReport.ReportInfo.Fondo);
                Rectangle pageSize = reader.GetPageSize(1);
                pageHeight = (int)pageSize.Height;
                loadData();
        }

        public void buildPage()
        {

           String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto +"_tmp_"+ System.DateTime.Now.ToString("ssffff") + ".pdf";

            try
            {
                 reader = new PdfReader(customerReport.ReportInfo.Fondo);
                 using (FileStream fs = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
                 {
                     using (PdfStamper stamper = new PdfStamper(reader, fs))
                     {

                         Rectangle pageSize = reader.GetPageSize(1);
                         pageHeight = (int)pageSize.Height;
                         loadData();


                         PdfContentByte cb = stamper.GetOverContent(1);
                         printHeaders(cb);
                         printDetails(cb);
                         stamper.Close();
                     }
                 }
            }
            catch (IOException io)
            {
                log.Error("No se encontro el archivo template "+io);
            }

            tempFiles.Add(destinationFile);

        }


        private void printHeaders(PdfContentByte cb)
        {
            int initHeaderY = pageHeight-topMargin;
            int spaceBetweenHeaders = (int)font.CalculatedSize;
            int positionY = initHeaderY;

            ArrayList headers = customerReport.getHeaders();

            foreach (String headerLine in headers)
            {
                Phrase phrase = new Phrase(headerLine, font);

                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, leftMargin, positionY, 0);
                positionY = positionY - spaceBetweenHeaders;
            }

            finalPositionHeaderY = positionY;
        }

        private void printDetails(PdfContentByte cb)
        {
            int initHeaderY = finalPositionHeaderY;
            int positionY = initHeaderY;
            int numOfRequiredPages = getNumberOfRequiredPages();
            int maxRowsForPage = getMaxNumOfRowsPerPage();

            ArrayList removeDetails = new ArrayList();

            int counter = 1;

            foreach (String detailLine in details)
            {

                if (counter > maxRowsForPage)
                {
                    break;
                }

                Phrase phrase = new Phrase(detailLine, font);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, leftMargin, positionY, 0);
                positionY = positionY - fontHeigth;

                counter++;

                removeDetails.Add(detailLine);
            }

            updateDetails(removeDetails);

        }

        private void updateDetails(ArrayList removeDetails)
        {
            foreach (String detailLine in removeDetails)
            {
                details.Remove(detailLine);
            }

            details.Insert(0, "");
        }

        private int getNumberOfRequiredPages()
        {

            int numberOfRows = customerReport.getDetails().Count;
            int requiredHeigth = fontHeigth * numberOfRows;

            int maxRowsPerPage = (pageHeight - (topMargin + buttonMargin)) * fontHeigth;
            int availableHeight = getAvailableHeigth();

            int numPages = 1;

            if (requiredHeigth > availableHeight)
            {
                double dNumPages = Convert.ToDouble(requiredHeigth) / Convert.ToDouble(availableHeight);
                if ((requiredHeigth % availableHeight) > 1)
                {
                    dNumPages++;
                }

                numPages = (int)dNumPages;
            }

            return numPages;
        }

        private int getAvailableHeigth() {
            return (pageHeight - (topMargin + buttonMargin));
        }

        private int getMaxNumOfRowsPerPage()
        {
            double maxRowsForPage = Convert.ToDouble(getAvailableHeigth()) / Convert.ToDouble(fontHeigth);
            if ((getAvailableHeigth() % fontHeigth) > 1)
            {
                maxRowsForPage++;
            }

            return (int)maxRowsForPage;
        }

        private void loadData()
        {

            font.Size = getFontSize();
            fontHeigth = (int)font.CalculatedSize;
            topMargin = getTopMargin();
            buttonMargin = getButtomMargin();
            details = customerReport.getDetails();
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

        private int getButtomMargin()
        {
            int size = Constants.DEFAULT_BUTTOM_MARGIN;
            try
            {

                if (customerReport.ReportInfo.BottomMargin.Length != 0)
                {
                    size = Convert.ToInt32(customerReport.ReportInfo.BottomMargin);
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
