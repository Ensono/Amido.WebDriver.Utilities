using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class SupportedBrowsersCollection : ConfigurationElementCollection
    {
        public new SupportedBrowserConfigurationElement this[string key]
        {
            get
            {
                return this.BaseGet(key) as SupportedBrowserConfigurationElement;
            }
        }

        public SupportedBrowserConfigurationElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as SupportedBrowserConfigurationElement;
            }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SupportedBrowserConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SupportedBrowserConfigurationElement)element).Key;
        }
    }
}