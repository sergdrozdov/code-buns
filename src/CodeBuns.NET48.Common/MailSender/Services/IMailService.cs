using System.Net.Mail;

namespace CodeBuns.NET48.Common
{
    /// <summary>
    /// Interface for mail services. Designed for future extensions where specific logic
    /// for sending emails via external services (e.g., SendGrid API, Mailgun API) may be required.
    /// </summary>
    public interface IMailService
    {
        void SetSmtpConfiguration(SmtpProvider smtpProvider);
        bool Send(MailMessage mailMessage);
    }
}
