using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class BrowserSwitchesCollection : ConfigurationElementCollection
    {
        public new BrowserSwitchConfigurationElement this[string key]
        {
            get
            {
                return this.BaseGet(key) as BrowserSwitchConfigurationElement;
            }
        }

        public BrowserSwitchConfigurationElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as BrowserSwitchConfigurationElement;
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
            return new BrowserSwitchConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BrowserSwitchConfigurationElement)element).Key;
        }
    }
}