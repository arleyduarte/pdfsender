using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OfficeOpenXml;
using System.IO;



namespace PDFSender.com.amdp.pdfsender
{
    class CustomerReportExcelAdapter
    {

        private static char SEPARADOR_COLUMNAS_EXCEL = ';';

        public bool buildReport(CustomerReport customerReport)
        {

            String destinationFile = Configuracion.Default.DESTINATION_FOLDER + customerReport.ReportInfo.NombreAdjunto + "_" + System.DateTime.Now.ToString("ssffff") + ".xlsx";

            FileInfo newFile = new FileInfo(destinationFile); 
            using (ExcelPackage xlPackage = new ExcelPackage(newFile)) {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Reporte");
                fillCells(worksheet, customerReport.getDetails());
                xlPackage.Save();
            }


            customerReport.AttachFile = destinationFile;
            return true;
        }

        private void fillCells(ExcelWorksheet worksheet, ArrayList details)
        {

            int rowNumber = 1;
            foreach (string line in details)
            {
                int columnNumber = 1;
                foreach (string cellValue in getColums(line))
                {
                    worksheet.Cell(rowNumber, columnNumber).Value = cellValue;
                    columnNumber++;
                }

                rowNumber++;
            }
           
        }

        private ArrayList getColums(String line)
        {
            ArrayList columns = new ArrayList();
            string[] sColumns = line.Split(SEPARADOR_COLUMNAS_EXCEL);

            foreach (string column in sColumns)
            {
                String auxColumn = column.Trim();

                if (auxColumn.Length != 0)
                {
                    columns.Add(auxColumn);
                }

            }

            return columns;
        }



    }
}
