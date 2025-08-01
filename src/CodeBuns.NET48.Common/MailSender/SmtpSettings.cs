using System.Collections.Generic;

namespace CodeBuns.NET48.Common
{
    public class SmtpSettings
    {
        public string DefaultSmtpProvider { get; set; }
        public List<SmtpProvider> SmtpProviders { get; set; }
    }
}
