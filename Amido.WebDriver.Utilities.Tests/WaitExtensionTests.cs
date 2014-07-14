using Amido.WebDriver.Utilities.Extensions;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Amido.WebDriver.Utilities.Tests
{
    [TestFixture]
    public class WaitExtensionTests
    {
        [Test]
        public void TestSome()
        {
            var driver = WebDriverFactory.Create("firefox");
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.FindElement(By.Id("hplogo")).WaitUntil(e => !e.Displayed);
        }
    }
}