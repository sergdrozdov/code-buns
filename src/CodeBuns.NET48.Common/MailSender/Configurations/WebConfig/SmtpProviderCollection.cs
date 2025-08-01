using System.Configuration;

namespace CodeBuns.NET48.Common
{
    [ConfigurationCollection(typeof(SmtpProviderElement), AddItemName = "provider")]
    public class SmtpProviderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SmtpProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SmtpProviderElement)element).Name;
        }

        public SmtpProviderElement this[int index] => (SmtpProviderElement)BaseGet(index);
    }
}
