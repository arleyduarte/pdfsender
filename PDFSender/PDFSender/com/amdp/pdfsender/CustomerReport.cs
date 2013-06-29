﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PDFSender.com.amdp.pdfsender
{
    public class CustomerReport
    {
        public ReportInfo ReportInfo { get; set; }
        public String AttachFile { get; set; }
        private ArrayList headers = new ArrayList();
        private ArrayList details = new ArrayList();

        public void addDetailLine(String detail)
        {
            details.Add(detail);
        }

        public void addHeaderLine(String headerLine)
        {
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
    }
}
