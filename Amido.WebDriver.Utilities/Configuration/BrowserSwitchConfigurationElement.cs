using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class BrowserSwitchConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return this["key"].ToString();
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = false)]
        public string Value
        {
            get
            {
                return this["value"].ToString();
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}