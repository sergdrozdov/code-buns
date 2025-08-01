using System.Configuration;

namespace CodeBuns.NET48.Common
{
    public class SmtpProvidersSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public SmtpProviderCollection Providers => (SmtpProviderCollection)this[""];
    }
}
