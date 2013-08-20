using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Configuration;
using System.Net.Mail;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;

namespace com.amdp.notificadoremail
{
    public class NotificadorEmail
    {
        SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        private static readonly ILog log = LogManager.GetLogger(typeof(NotificadorEmail));

        private MailMessage msg = null;

        public String Remite { get; set; }



        public NotificadorEmail()
        {
            log4net.Config.BasicConfigurator.Configure();
            log.Warn("Correo de Envio " + smtpSec.From);
        }


        public void enviarMensaje(MailMessage msg)
        {
            this.msg = msg;

            enviarMensajeRun();
        }


        public void enviarMensaje(String sEmail, String sMensaje, String sSubject)
        {

            msg = new MailMessage();

            msg.To.Add(sEmail);
            msg.Body = sMensaje;
            msg.Subject = sSubject;

            Thread thread = new Thread(new ThreadStart(enviarMensajeRun));
            thread.Start();
        }


        private void enviarMensajeRun()
        {

            String from = smtpSec.From;
            String remitente = smtpSec.Network.UserName;
            if (Remite != null)
            {
                if (validarEmail(Remite))
                {
                    from = Remite;
                    remitente = Remite;
                }
                else
                {
                    log.Error("El correo del remitente no es valido: "+Remite);
                }

            }
            msg.From = new MailAddress(from, remitente, System.Text.Encoding.UTF8);

            msg.Priority = MailPriority.High;

            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(from, smtpSec.Network.Password);
            client.Port = smtpSec.Network.Port;
            client.Host = smtpSec.Network.Host;
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail


            try
            {

                client.Send(msg);
                log.Info(msg.To.ToString() + "Body:" + msg.Body.ToString());
               

            }

            catch (Exception ex)
            {

                log.Error("Error al enviar " + ex);

                return;
            }
        }

        public static bool validarEmail(string email)
        {

            if (email == null || email.Length == 0)
            {
                return false;
            }

            bool flag1;



            string s = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool flag2 = !Regex.IsMatch(email, s);
            if (!flag2)
            {
                flag2 = Regex.Replace(email, s, String.Empty).Length != 0;
                if (!flag2)
                    flag1 = true;
                else
                    flag1 = false;
            }
            else
            {
                flag1 = false;
            }
            return flag1;
        }

    }
}
