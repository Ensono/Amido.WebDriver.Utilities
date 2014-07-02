using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Amido.WebDriver.Utilities.Extensions
{
    public static class WaitExtensions
    {
        /// <summary>
        /// Waits for an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="timeOutInSeconds">The time out in seconds.</param>
        /// <param name="pollingIntervalInMilliseconds">The polling interval in milliseconds.</param>
        public static void WaitForElement(IWebElement element, Func<IWebElement, bool> condition, int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 500)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds);
            wait.Until(condition);
        }

        /// <summary>
        /// Waits until a condition is met.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="timeOutInSeconds">The time out in seconds.</param>
        /// <param name="pollingIntervalInMilliseconds">The polling interval in milliseconds.</param>
        /// <returns></returns>
        public static IWebElement WaitUntil(this IWebElement element, Func<IWebElement, bool> condition, int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 500)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds);
            wait.Until(condition);
            return element;
        }
    }
}