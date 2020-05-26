using System;
using System.Net.Mail;

namespace Pinger.Services
{
    public static class MailService
    {
        public static string EmailHostName;
        public static int EmailPort = 80;
        public static bool EmailUseSsl = false;
        public static string EmailUsername;
        public static string EmailPassword;

        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtp = new SmtpClient(EmailHostName, EmailPort)
                {
                    EnableSsl = EmailUseSsl,
                    Credentials = new System.Net.NetworkCredential(EmailUsername, EmailPassword),
                };

                MailAddress fromAddress = new MailAddress("pinger@jaksic.net", "Site Pinger");
                MailAddress toAddress = new MailAddress(toEmail);
                var email = new MailMessage(fromAddress, toAddress);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;
                smtp.Send(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }      
    }
}
