using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class DesiredCapabilitiesCollection : ConfigurationElementCollection
    {
        public new DesiredCapabilityConfigurationElement this[string key]
        {
            get
            {
                return this.BaseGet(key) as DesiredCapabilityConfigurationElement;
            }
        }

        public DesiredCapabilityConfigurationElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as DesiredCapabilityConfigurationElement;
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
            return new DesiredCapabilityConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DesiredCapabilityConfigurationElement)element).Key;
        }
    }
}