using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class WebDriverConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("globalBrowserOptions", IsRequired = true)]
        [ConfigurationCollection(typeof(GlobalBrowserOptionsCollection), AddItemName = "browser")]
        public GlobalBrowserOptionsCollection GlobalBrowserOptions
        {
            get
            {
                return this["globalBrowserOptions"] as GlobalBrowserOptionsCollection;
            }
        }

        [ConfigurationProperty("remoteDriverAddress", IsKey = true, IsRequired = true)]
        public string RemoteDriverAddress
        {
            get
            {
                return this["remoteDriverAddress"].ToString();
            }
            set
            {
                this["remoteDriverAddress"] = value;
            }
        }
    }
}