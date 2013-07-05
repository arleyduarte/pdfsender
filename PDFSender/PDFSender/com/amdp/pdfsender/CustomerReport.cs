using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using log4net;

namespace PDFSender.com.amdp.pdfsender
{
    public class CustomerReport
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CustomerReport));
        public ReportInfo ReportInfo { get; set; }
        public String AttachFile { get; set; }
        public String Body { get; set; }
        private ArrayList headers = new ArrayList();
        private ArrayList details = new ArrayList();
        private ArrayList auxDetailsForTextBody = new ArrayList();

        public void addDetailLine(String detail)
        {
            details.Add(detail.Trim());
        }

        public void addHeaderLine(String headerLine)
        {
            headerLine = headerLine.Replace(Constants.HEADER_INDICATOR, "");
            headers.Add(headerLine);
        }

        public ArrayList getHeaders()
        {
            return headers;
        }

        public ArrayList getDetails()
        {
            return details;
        }



        internal void fillTextBody()
        {

            try
            {
                ArrayList auxDetails = new ArrayList();

                if (details.Contains(Constants.TEXT_INDICATOR))
                {
                    int firstTextIndicator = details.IndexOf(Constants.TEXT_INDICATOR);
                    int secondTextIndicator = details.LastIndexOf(Constants.TEXT_INDICATOR);
                    int cutIndex = firstTextIndicator + 1;
                    int count = secondTextIndicator - firstTextIndicator - 1;

                    auxDetails = details.GetRange(cutIndex, count);

                    foreach (String auxLine in auxDetails)
                    {
                        Body = Body + "<br />" + auxLine;
                    }

                    details.RemoveRange(cutIndex - 1, count + 2);
                    details.Remove(Constants.TEXT_INDICATOR);

                }
            }
            catch (Exception e)
            {
                Body = "";
                log.Error("fillTextBody: " + e);
            }


        }
    }
}
