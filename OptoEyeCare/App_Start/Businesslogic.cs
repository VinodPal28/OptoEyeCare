using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace OptoEyeCare.App_Start
{
    public class Businesslogic
    {
        public bool sendmail(string subject, string body, string attachment, string attachment2, string mailfrom, string mailto, string mailcc, string mailbcc)
        {

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConfigurationManager.AppSettings["fromMail"].ToString(),"OptoEyeCare");//your mail id                                                                                                   //  mail.To.Add(mailto);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                SmtpClient smc = new SmtpClient();
                smc.EnableSsl = true;
                smc.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smc.Credentials = new System.Net.NetworkCredential("pal.vinod807@gmail.com", "Vinod2812");
                smc.Host = "smtp.gmail.com";
                smc.Port = 587;
                //With authentication
                mail.From = new MailAddress(mailfrom);
                if (!string.IsNullOrEmpty(mailto))
                {
                    mail.To.Add(mailto);
                }

                if (!string.IsNullOrEmpty(mailcc))
                {
                    mail.CC.Add(mailcc);
                }
                if (!string.IsNullOrEmpty(mailbcc))
                {
                    mail.Bcc.Add(mailbcc);
                }
                mail.Subject = subject;
                mail.Body = body;
                if (!string.IsNullOrEmpty(attachment))
                {
                    Attachment attach = new Attachment(attachment);
                    mail.Attachments.Add(attach);
                }
                if (!string.IsNullOrEmpty(attachment2))
                {
                    Attachment attach2 = new Attachment(attachment2);
                    mail.Attachments.Add(attach2);
                }
                smc.Send(mail);
                return true;
            }
            catch (Exception ex)
            {              
                return false;
            }

        }

    }
}