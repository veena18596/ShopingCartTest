using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Internal;
using NUnit.Framework;

namespace ShoppingCartTest.Extensions
{
    public static class SeleniumExtension
    {
        public static void WaitForElement(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(driver =>
            {
                    return driver.FindElement(locator);
               
            });
        }
    }
}
