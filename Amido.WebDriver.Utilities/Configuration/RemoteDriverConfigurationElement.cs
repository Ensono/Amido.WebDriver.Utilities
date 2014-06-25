using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class RemoteDriverConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("address", IsKey = true, IsRequired = true)]
        public string Address
        {
            get
            {
                return this["address"].ToString();
            }
            set
            {
                this["address"] = value;
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
    }
}