using System;

using OpenQA.Selenium;

namespace Amido.WebDriver.Utilities.Extensions
{
    public static class WebElementExtensions
    {
        public static void EnterText(this IWebElement element, string text)
        {
            WaitForElement(element);
            element.SendKeys(text);
        }

        public static string GetTextBoxValue(this IWebElement element, Func<IWebElement, bool> waitCondition = null)
        {
            WaitForElement(element);

            if (waitCondition != null)
            {
                WaitExtensions.WaitForElement(element, waitCondition);
            }

            return element.GetAttribute("value");
        }

        public static void ClickWhenPresent(this IWebElement element)
        {
            WaitForElement(element);
            element.Click();
        }

        public static void Tab(this IWebElement element)
        {
            WaitForElement(element);
            element.SendKeys(Keys.Tab);
        }

        public static void PressEnter(this IWebElement element)
        {
            element.SendKeys(Keys.Enter);
        }

        private static void WaitForElement(IWebElement element)
        {
            WaitExtensions.WaitForElement(element, e => e.Displayed && e.Enabled);
        }
    }
}