using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class WebDriverConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("supportedBrowsers", IsRequired = true)]
        [ConfigurationCollection(typeof(SupportedBrowsersCollection), AddItemName = "supportedBrowser")]
        public SupportedBrowsersCollection SupportedBrowsers
        {
            get
            {
                return this["supportedBrowsers"] as SupportedBrowsersCollection;
            }
        }

        [ConfigurationProperty("remoteDriver", IsRequired = true)]
        public RemoteDriverConfigurationElement RemoteDriver
        {
            get
            {
                return this["remoteDriver"] as RemoteDriverConfigurationElement;
            }
        }

        [ConfigurationProperty("globalBrowserOptions", IsRequired = true)]
        [ConfigurationCollection(typeof(GlobalBrowserOptionConfigurationElement), AddItemName = "browser")]
        public GlobalBrowserOptionsCollection GlobalBrowserOptions
        {
            get
            {
                return this["globalBrowserOptions"] as GlobalBrowserOptionsCollection;
            }
        }
    }
}