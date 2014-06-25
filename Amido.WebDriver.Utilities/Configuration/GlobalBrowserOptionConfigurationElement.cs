using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class GlobalBrowserOptionConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("desiredCapabilities", IsRequired = false)]
        [ConfigurationCollection(typeof(DesiredCapabilitiesCollection), AddItemName = "capability")]
        public DesiredCapabilitiesCollection DesiredCapabilities
        {
            get
            {
                return this["desiredCapabilities"] as DesiredCapabilitiesCollection;
            }
        }

        [ConfigurationProperty("browserSwitches", IsRequired = false)]
        [ConfigurationCollection(typeof(BrowserSwitchesCollection), AddItemName = "switch")]
        public BrowserSwitchesCollection BrowserSwitches
        {
            get
            {
                return this["browserSwitches"] as BrowserSwitchesCollection;
            }
        }
    }
}