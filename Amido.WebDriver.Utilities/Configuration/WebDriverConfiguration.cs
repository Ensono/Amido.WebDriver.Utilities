using System.Configuration;

namespace Amido.WebDriver.Utilities.Configuration
{
    public static class WebDriverConfiguration
    {
        public static WebDriverConfigurationSection Current
        {
            get
            {
                return (WebDriverConfigurationSection)ConfigurationManager.GetSection("webDriverConfiguration/webDriver");
            }
        } 
    }
}