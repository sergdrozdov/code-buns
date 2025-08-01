using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CodeBuns.NET48.Common
{
    public class MailAgent
    {
        private readonly List<SmtpProvider> _smtpProviders;
        private readonly SmtpProvider _defaultProvider;

        public MailAgent(ISmtpConfiguration smtpConfiguration)
        {
            var smtpSettings = smtpConfiguration.GetSettings();
            if (smtpSettings.SmtpProviders == null || !smtpSettings.SmtpProviders.Any())
            {
                throw new InvalidOperationException("No SMTP providers are defined in the configuration.");
            }

            _smtpProviders = smtpSettings.SmtpProviders;
            _defaultProvider = _smtpProviders.FirstOrDefault(p => p.Name == smtpSettings.DefaultSmtpProvider);
            if (_defaultProvider == null)
            {
                throw new InvalidOperationException($"Default SMTP provider '{smtpSettings.DefaultSmtpProvider}' is not found in the configuration.");
            }
        }

        public void Send(MailMessage mailMessage)
        {
            foreach (var provider in _smtpProviders)
            {
                try
                {
                    var smtpClient = new SmtpClient
                    {
                        Host = provider.Host,
                        Port = provider.Port,
                        EnableSsl = provider.EnableSsl,
                        Credentials = new NetworkCredential(provider.Username, provider.Password)
                    };

                    smtpClient.Send(mailMessage);
                    return;
                }
                catch (Exception ex)
                {
                    // Log the exception (optional)
                }
            }

            // If none of the SMTP providers succeeded
            throw new Exception("All SMTP providers failed to send the email.");
        }
    }
}