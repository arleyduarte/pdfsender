using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using log4net;
using System.Collections;
using PDFSender.com.amdp.utils;

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
        private int leftMargin;
        private int fontHeigth;
        private ArrayList tempFiles = new ArrayList();
        private ArrayList details = new ArrayList();

        public bool buildReport(CustomerReport customerReport)
        {
            this.customerReport = customerReport;

            if (loadTemplate())
            {
                int numOfRequiredPages = getNumberOfRequiredPages();

                for (int i = 0; i < numOfRequiredPages; i++)
                {
                    buildPage();
                }

                mergePages();

                FileManager fileManager = new FileManager();
                fileManager.deleteFiles(tempFiles);

                return true;
            }
            else
            {
                return false;
            }

            
        }

        private void mergePages()
        {
            String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto + "_" + System.DateTime.Now.ToString("ssffff") + ".pdf";
            PdfMerge.MergeFiles(destinationFile, getTempSubFiles());
            customerReport.AttachFile = destinationFile;
        }


        private bool loadTemplate()
        {
            try
            {

                reader = new PdfReader(customerReport.ReportInfo.Fondo);
                Rectangle pageSize = reader.GetPageSize(1);
                pageHeight = (int)pageSize.Height;
                loadData();

                return true;
            }
            catch (Exception ioe)
            {
                log.Error("No se econtro el archivo de FONDO:" +customerReport.ReportInfo.Fondo+" "+ioe);
                return false;
            }

        }

        private void buildPage()
        {

            String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto + "_tmp_" + System.DateTime.Now.ToString("ssffff") + ".pdf";

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
                log.Error("No se encontro el archivo template " + io);
            }

            tempFiles.Add(destinationFile);

        }


        private void printHeaders(PdfContentByte cb)
        {
            int initHeaderY = pageHeight - topMargin;
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

        private int getAvailableHeigth()
        {
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
            font.Size = getParameterValue(customerReport.ReportInfo.TamanoLetra, Constants.DEFAULT_FONT_SIZE);
            fontHeigth = (int)font.CalculatedSize;
            topMargin = getParameterValue(customerReport.ReportInfo.TopMargin, Constants.DEFAULT_TOP_MARGIN);
            buttonMargin = getParameterValue(customerReport.ReportInfo.BottomMargin, Constants.DEFAULT_TOP_MARGIN);
            leftMargin = getParameterValue(customerReport.ReportInfo.LeftMargin, Constants.DEFAULT_LEFT_MARGIN);
            details = customerReport.getDetails();
        }



        private int getParameterValue(String parameter, int defaultValue)
        {
            int size = defaultValue;
            try
            {
                if (parameter.Length != 0)
                {
                    size = Convert.ToInt32(parameter);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return size;
        }


        private String[] getTempSubFiles()
        {
            String[] invoiceFiles = new String[tempFiles.Count];
            for (int i = 0; i < tempFiles.Count; i++)
            {
                invoiceFiles[i] = tempFiles[i].ToString();
            }
            return invoiceFiles;
        }

    }
}
