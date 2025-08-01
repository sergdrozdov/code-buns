using System;
using System.Net;
using System.Net.Mail;

namespace CodeBuns.NET48.Common
{
    public class SendGridMailService : IMailService
    {
        private SmtpProvider _smtpProvider;

        public void SetSmtpConfiguration(SmtpProvider smtpProvider)
        {
            _smtpProvider = smtpProvider;
        }

        public bool Send(MailMessage mailMessage)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _smtpProvider.Host;
                    smtpClient.Port = _smtpProvider.Port;
                    smtpClient.EnableSsl = _smtpProvider.EnableSsl;
                    smtpClient.Credentials = new NetworkCredential(_smtpProvider.Username, _smtpProvider.Password);

                    smtpClient.Send(mailMessage);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
