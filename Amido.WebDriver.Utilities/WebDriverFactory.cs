using System;
using System.Text;

using Amido.WebDriver.Utilities.Configuration;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Amido.WebDriver.Utilities
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(string supportedBrowserKey)
        {
            var supportedBrowserConfiguration = WebDriverConfiguration.Current.SupportedBrowsers[supportedBrowserKey];
            var globalBrowserOptions = WebDriverConfiguration.Current.GlobalBrowserOptions;
            var remoteDriverConfiguration = WebDriverConfiguration.Current.RemoteDriver;

            var isRemote = supportedBrowserConfiguration.DriverLocation == "remote";
            var browserOptions = globalBrowserOptions[supportedBrowserConfiguration.Name];

            var desiredCapabilities = new DesiredCapabilities();

            SetupRemoteCapabilities(remoteDriverConfiguration, isRemote, desiredCapabilities);
            SetupBrowserSwitches(supportedBrowserConfiguration, isRemote, browserOptions, desiredCapabilities);
            SetupGlobalBrowserCapabilities(browserOptions, desiredCapabilities);
            SetupTargetCapabilities(supportedBrowserConfiguration, desiredCapabilities);

            var driver = CreateDriver(supportedBrowserConfiguration, remoteDriverConfiguration, isRemote, desiredCapabilities, browserOptions);

            if (driver == null)
            {
                throw new InvalidOperationException("The driver is null");
            }
            
            if (!supportedBrowserConfiguration.DeleteAllCookies.HasValue || supportedBrowserConfiguration.DeleteAllCookies.Value)
            {
                driver.Manage().Cookies.DeleteAllCookies();
            }

            if (!supportedBrowserConfiguration.IsMobile)
            {
                if (!supportedBrowserConfiguration.MaximizeWindow.HasValue || supportedBrowserConfiguration.MaximizeWindow.Value)
                {
                    driver.Manage().Window.Maximize();
                }
            }

            if (!supportedBrowserConfiguration.ImplicitTimeoutInSeconds.HasValue)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));    
            }
            else
            {
                driver.Manage()
                    .Timeouts()
                    .ImplicitlyWait(TimeSpan.FromSeconds(supportedBrowserConfiguration.ImplicitTimeoutInSeconds.Value));
            }
            
            return driver;
        }

        private static IWebDriver CreateDriver(
            SupportedBrowserConfigurationElement supportedBrowserConfiguration,
            RemoteDriverConfigurationElement remoteDriverConfiguration,
            bool isRemote,
            ICapabilities desiredCapabilities,
            GlobalBrowserOptionConfigurationElement browserOptions)
        {
            if (isRemote)
            {
                return new RemoteWebDriver(new Uri(remoteDriverConfiguration.Address), desiredCapabilities);
            }
            switch (supportedBrowserConfiguration.Name)
            {
                case "Chrome":
                    var options = new ChromeOptions();
                    foreach (BrowserSwitchConfigurationElement browserSwitch in browserOptions.BrowserSwitches)
                    {
                        options.AddArguments(browserSwitch.Value);
                    }
                    return new ChromeDriver(options);
                case "Firefox":
                    return new FirefoxDriver(desiredCapabilities);
                default:
                    throw new ArgumentException("Only Chrome and Firefox are supported locally");
            }
        }

        private static void SetupTargetCapabilities(
            SupportedBrowserConfigurationElement supportedBrowserConfiguration,
            DesiredCapabilities desiredCapabilities)
        {
            desiredCapabilities.SetCapability(
                supportedBrowserConfiguration.IsMobile ? "browserName" : "browser",
                supportedBrowserConfiguration.Name);

            if (!string.IsNullOrWhiteSpace(supportedBrowserConfiguration.Version))
            {
                desiredCapabilities.SetCapability("browser_version", supportedBrowserConfiguration.Version);
            }

            foreach (DesiredCapabilityConfigurationElement capability in supportedBrowserConfiguration.DesiredCapabilities)
            {
                desiredCapabilities.SetCapability(capability.Key, capability.Value);
            }
        }

        private static void SetupRemoteCapabilities(
            RemoteDriverConfigurationElement remoteDriverConfiguration,
            bool isRemote,
            DesiredCapabilities desiredCapabilities)
        {
            if (!isRemote)
            {
                return;
            }

            foreach (DesiredCapabilityConfigurationElement capability in remoteDriverConfiguration.DesiredCapabilities)
            {
                desiredCapabilities.SetCapability(capability.Key, capability.Value);
            }
        }

        private static void SetupGlobalBrowserCapabilities(
            GlobalBrowserOptionConfigurationElement browserOptions,
            DesiredCapabilities desiredCapabilities)
        {
            if (browserOptions == null)
            {
                return;
            }

            foreach (DesiredCapabilityConfigurationElement capability in browserOptions.DesiredCapabilities)
            {
                desiredCapabilities.SetCapability(capability.Key, capability.Value);
            }
        }

        private static void SetupBrowserSwitches(
            SupportedBrowserConfigurationElement supportedBrowserConfiguration,
            bool isRemote,
            GlobalBrowserOptionConfigurationElement browserOptions,
            DesiredCapabilities desiredCapabilities)
        {
            switch (supportedBrowserConfiguration.Name)
            {
                case "Chrome":
                    if (isRemote && (browserOptions.BrowserSwitches.Count > 0 || supportedBrowserConfiguration.BrowserSwitches.Count > 0))
                    {
                        desiredCapabilities.SetCapability("args", ConcatChromeSwitches(browserOptions.BrowserSwitches, supportedBrowserConfiguration.BrowserSwitches));
                    }
                    break;
                case "Firefox":
                    if (browserOptions.BrowserSwitches.Count > 0)
                    {
                        throw new NotSupportedException("Not handling firefox switches at the moment");
                    }
                    break;
                default:
                    if (browserOptions != null && browserOptions.BrowserSwitches.Count > 0)
                    {
                        throw new NotSupportedException(
                            "Not handling " + supportedBrowserConfiguration.Name + "switches at the moment");
                    }
                    break;
            }
        }

        private static string ConcatChromeSwitches(BrowserSwitchesCollection browserSwitches, BrowserSwitchesCollection targetBrowserSwtiches)
        {
            var sb = new StringBuilder();
            foreach (BrowserSwitchConfigurationElement browserSwitch in browserSwitches)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.Append(browserSwitch.Value);
            }
            foreach (BrowserSwitchConfigurationElement browserSwitch in targetBrowserSwtiches)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.Append(browserSwitch.Value);
            }
            return sb.ToString();
        }
    }
}