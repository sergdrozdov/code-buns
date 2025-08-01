using System.Configuration;

namespace CodeBuns.NET48.Common
{
    public class SmtpProviderElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => (string)this["name"];

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host => (string)this["host"];

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port => (int)this["port"];

        [ConfigurationProperty("enableSsl", IsRequired = true)]
        public bool EnableSsl => (bool)this["enableSsl"];

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => (string)this["username"];

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => (string)this["password"];

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        public bool Enabled => (bool)this["enabled"];
    }
}
