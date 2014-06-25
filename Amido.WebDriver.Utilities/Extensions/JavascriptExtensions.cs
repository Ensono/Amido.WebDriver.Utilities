using System;

using OpenQA.Selenium;

namespace Amido.WebDriver.Utilities.Extensions
{
    public static class JavascriptExtensions
    {
        public static void ExecuteJavascript(this IWebDriver driver, string javascript)
        {
            var javascriptExecutor = driver as IJavaScriptExecutor;
            javascriptExecutor.ExecuteScript(javascript);
        }

        public static string GetJavascriptVariableString(this IWebDriver driver, string javascript, Func<IWebDriver, bool> waitCondition = null)
        {
            var javascriptExecutor = driver as IJavaScriptExecutor;
            var obj = javascriptExecutor.ExecuteScript(javascript);
            return obj.ToString();
        } 
    }
}