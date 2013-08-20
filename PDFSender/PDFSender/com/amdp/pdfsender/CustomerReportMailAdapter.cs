using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Collections;
using com.amdp.notificadoremail;
using log4net;
using System.Net.Mime;
using System.IO;

namespace PDFSender.com.amdp.pdfsender
{
    class CustomerReportMailAdapter
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(CustomerReportMailAdapter));

        public MailMessage getMailMessage(CustomerReport customerReport)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = customerReport.ReportInfo.Asunto;

       
            String imagenAdjunta = customerReport.ReportInfo.ImagenAdjunta;

            if (imagenAdjunta != null && imagenAdjunta.Length > 0 && File.Exists(imagenAdjunta))
            {
                string html = customerReport.Body +
                  "<br/><img src='cid:imagen' />";

                AlternateView htmlView =
                    AlternateView.CreateAlternateViewFromString(html,
                                            Encoding.UTF8,
                                            MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(imagenAdjunta, MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";
                htmlView.LinkedResources.Add(img);
                mail.AlternateViews.Add(htmlView);
            }
            else
            {
                mail.Body = customerReport.Body;
            }



            // Lo incrustamos en la vista HTML...

            asignarDestinatarios(customerReport, mail);


            if (File.Exists(customerReport.AttachFile))
            {

                mail.Attachments.Add(new Attachment(customerReport.AttachFile));
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
