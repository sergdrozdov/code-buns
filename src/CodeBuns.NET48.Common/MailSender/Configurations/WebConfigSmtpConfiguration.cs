using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace CodeBuns.NET48.Common
{
    public class WebConfigSmtpConfiguration : ISmtpConfiguration
    {
        public SmtpSettings GetSettings()
        {
            var defaultProvider = ConfigurationManager.AppSettings["DefaultSmtpProvider"];
            if (string.IsNullOrEmpty(defaultProvider))
            {
                throw new ConfigurationErrorsException("DefaultSmtpProvider is not defined in appSettings.");
            }

            var smtpSection = ConfigurationManager.GetSection("smtpProviders") as SmtpProvidersSection;
            if (smtpSection == null)
            {
                throw new ConfigurationErrorsException("smtpProviders section is missing in the configuration file.");
            }

            // Retrieve configuration section for SMTP providers
            var providers = new List<SmtpProvider>();
            foreach (SmtpProviderElement providerElement in smtpSection.Providers.Cast<SmtpProviderElement>().Where(x => x.Enabled))
            {
                providers.Add(new SmtpProvider
                {
                    Name = providerElement.Name,
                    Host = providerElement.Host,
                    Port = providerElement.Port,
                    EnableSsl = providerElement.EnableSsl,
                    Username = providerElement.Username,
                    Password = providerElement.Password
                });
            }

            return new SmtpSettings
            {
                DefaultSmtpProvider = defaultProvider,
                SmtpProviders = providers
            };
        }
    }
}
