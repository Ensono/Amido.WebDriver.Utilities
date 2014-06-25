using NUnit.Framework;

using Should;

namespace Amido.WebDriver.Utilities.Tests
{
    [TestFixture]
    public class WebDriverFactoryTests
    {
        [Test]
        public void Should_Launch_Local_Firefox()
        {
            var driver = WebDriverFactory.Create("firefox");
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.Url.ShouldContain("google");
            driver.Quit();
        }

        [Test]
        public void Should_Launch_Local_Chrome()
        {
            var driver = WebDriverFactory.Create("chrome");
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.Url.ShouldContain("google");
            driver.Quit();
        }

        [Test]
        public void Should_Launch_Remote_BrowserStack()
        {
            var driver = WebDriverFactory.Create("bs-IE8-win7");
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.Url.ShouldContain("google");
            driver.Quit();
        }
    }
}