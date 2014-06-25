using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public class BrowserConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
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
    }
}