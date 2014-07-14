using NUnit.Framework;

using OpenQA.Selenium;

namespace Amido.WebDriver.Utilities.Tests
{
    [TestFixture]
    public class ScreenShotServiceTests
    {
        private IWebDriver webDriver;

        [Test]
        public void TestScreenShot()
        {
            webDriver = WebDriverFactory.Create("firefox");

            ScreenShotService.Init(@"c:\temp\screenshottests\", GetWebDriver, GetFeatureTitle, GetScenarioTitle);

            webDriver.Navigate().GoToUrl("http://www.google.com");
            var screenShotService = new ScreenShotService();
            screenShotService.Capture("This is my new name", "This is a message");
            webDriver.Quit();
        }

        private IWebDriver GetWebDriver()
        {
            return webDriver;
        }

        private string GetScenarioTitle()
        {
            return "This is the scenario";
        }

        private string GetFeatureTitle()
        {
            return "This is the feature";
        }
    }
}