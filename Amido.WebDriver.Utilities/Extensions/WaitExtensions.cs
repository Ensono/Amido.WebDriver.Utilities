using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Amido.WebDriver.Utilities.Extensions
{
    public static class WaitExtensions
    {
        public static void WaitForElement(IWebElement element, Func<IWebElement, bool> condition)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.Until(condition);
        }
    }
}