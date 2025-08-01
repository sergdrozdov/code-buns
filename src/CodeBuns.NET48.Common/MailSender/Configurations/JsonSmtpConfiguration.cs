using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace CodeBuns.NET48.Common
{
    public class JsonSmtpConfiguration : ISmtpConfiguration
    {
        private readonly string _filePath;

        public JsonSmtpConfiguration(string filePath)
        {
            _filePath = filePath;
        }

        public SmtpSettings GetSettings()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {_filePath}");
            }

            var json = File.ReadAllText(_filePath);
            SmtpSettings smtpSettings;
            try
            {
                smtpSettings = JsonConvert.DeserializeObject<SmtpSettings>(json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse JSON configuration.", ex);
            }

            if (string.IsNullOrEmpty(smtpSettings.DefaultSmtpProvider))
            {
                throw new InvalidOperationException("DefaultSmtpProvider is not defined in the JSON configuration.");
            }

            if (smtpSettings.SmtpProviders == null || !smtpSettings.SmtpProviders.Any())
            {
                throw new InvalidOperationException("No SMTP providers are defined in the JSON configuration.");
            }

            smtpSettings.SmtpProviders = smtpSettings.SmtpProviders.Where(p => p.Enabled).ToList();

            return smtpSettings;
        }
    }
}
