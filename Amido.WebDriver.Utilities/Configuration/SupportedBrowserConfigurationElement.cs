using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class SupportedBrowserConfigurationElement : ConfigurationElement
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

        [ConfigurationProperty("version", IsRequired = false)]
        public string Version
        {
            get
            {
                return this["version"].ToString();
            }
            set
            {
                this["version"] = value;
            }
        }

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

        [ConfigurationProperty("isMobile", IsRequired = false)]
        public bool IsMobile
        {
            get
            {
                return bool.Parse(this["isMobile"].ToString());
            }
            set
            {
                this["isMobile"] = value;
            }
        }

        [ConfigurationProperty("driverLocation", IsRequired = true)]
        public string DriverLocation
        {
            get
            {
                return this["driverLocation"].ToString();
            }
            set
            {
                this["driverLocation"] = value;
            }
        }

        [ConfigurationProperty("implicitTimeoutInSeconds")]
        public int? ImplicitTimeoutInSeconds
        {
            get
            {
                if (this["implicitTimeoutInSeconds"] != null)
                {
                    return int.Parse(this["implicitTimeoutInSeconds"].ToString());
                }
                return null;
            }
            set
            {
                this["implicitTimeoutInSeconds"] = value;
            }
        }

        [ConfigurationProperty("deleteAllCookies")]
        public bool? DeleteAllCookies
        {
            get
            {
                if (this["deleteAllCookies"] != null)
                {
                    return bool.Parse(this["deleteAllCookies"].ToString());
                }
                return null;
            }
            set
            {
                this["deleteAllCookies"] = value;
            }
        }

        [ConfigurationProperty("maximizeWindow")]
        public bool? MaximizeWindow
        {
            get
            {
                if (this["maximizeWindow"] != null)
                {
                    return bool.Parse(this["maximizeWindow"].ToString());
                }
                return null;
            }
            set
            {
                this["maximizeWindow"] = value;
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