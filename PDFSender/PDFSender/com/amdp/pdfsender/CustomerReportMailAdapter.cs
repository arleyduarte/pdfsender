using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Collections;
using com.amdp.notificadoremail;
using log4net;

namespace PDFSender.com.amdp.pdfsender
{
    class CustomerReportMailAdapter
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(CustomerReportMailAdapter));

        public MailMessage getMailMessage(CustomerReport customerReport)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = customerReport.ReportInfo.Asunto;
            mail.Body = customerReport.Body;
            asignarDestinatarios(customerReport, mail);

            if (customerReport.ReportInfo.Formato == Constants.PDF_FORMATO)
            {
                mail.Attachments.Add(new Attachment(customerReport.AttachFile));
            }
            else
            {
                mail.Attachments.Add(new Attachment(customerReport.SourceFile));
            }




            log.Info("Destino: " + customerReport.ReportInfo.Destino);
            log.Info("CC: " + customerReport.ReportInfo.CC);
            log.Info("Adjunto: " + customerReport.AttachFile);

            return mail;
        }

        private void asignarDestinatarios(CustomerReport customerReport, MailMessage mail)
        {
            foreach (String direccion in getDireccionCorreo(customerReport.ReportInfo.Destino))
            {

                if (NotificadorEmail.validarEmail(direccion.Trim()))
                {
                    mail.To.Add(direccion.Trim());
                }
                else
                {
                    log.Info("Dirección no valida: " + direccion);
                }
            }

            foreach (String direccion in getDireccionCorreo(customerReport.ReportInfo.CC))
            {

                if (NotificadorEmail.validarEmail(direccion.Trim()))
                {
                    mail.CC.Add(direccion.Trim());
                }

                else
                {
                    log.Info("Dirección no valida: " + direccion);
                }
            }


        }

        private ArrayList getDireccionCorreo(String direcciones)
        {
            ArrayList direccioneEmail = new ArrayList();
            if (direcciones != null)
            {
                string[] words = direcciones.Split(',');

                foreach (string word in words)
                {
                    Console.WriteLine(word);
                    direccioneEmail.Add(word);

                }
            }
           return direccioneEmail;
        }
    }
}
