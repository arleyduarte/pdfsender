using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PDFSender.com.amdp.pdfsender
{
    class CustomerReportTextAdapter
    {
        public bool buildReport(CustomerReport customerReport)
        {

            ArrayList headers = customerReport.getHeaders();
            ArrayList details = customerReport.getDetails();

            ArrayList lines = new ArrayList();
            lines.AddRange(headers);
            lines.AddRange(details);

            String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto + "_" + System.DateTime.Now.ToString("ssffff") + "."+customerReport.ReportInfo.Formato;


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destinationFile))
            {
                foreach (string line in lines)
                {

                        file.WriteLine(line);
                }
            }

            customerReport.AttachFile = destinationFile;

            return true;
        }
    }
}
