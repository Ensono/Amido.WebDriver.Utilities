using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class GlobalBrowserOptionsCollection : ConfigurationElementCollection
    {
        public new GlobalBrowserOptionConfigurationElement this[string key]
        {
            get
            {
                return this.BaseGet(key) as GlobalBrowserOptionConfigurationElement;
            }
        }

        public GlobalBrowserOptionConfigurationElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as GlobalBrowserOptionConfigurationElement;
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
            return new GlobalBrowserOptionConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GlobalBrowserOptionConfigurationElement)element).Name;
        }
    }
}