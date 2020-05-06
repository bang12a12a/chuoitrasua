using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Common
{
    class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAdrress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword= ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost= ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPost= ConfigurationManager.AppSettings["SMTPPost"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;

            MailMessage message = new MailMessage(new MailAddress(fromEmailAdrress, fromEmailDisplayName), new MailAddress(toEmailAddress));


            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAdrress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPost) ? Convert.ToInt32(smtpPost) : 0;
            client.Send(message);
        }
    }
}
