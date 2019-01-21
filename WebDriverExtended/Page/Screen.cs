using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverExtended.Page
{
    class Screen : IPageObject
    {

        protected IWebDriver Driver;
        protected string DisplayName;
        private WebDriverWait PageWait;

        public Screen(IWebDriver driver, string displayName)
        {
            Driver = driver;
            PageWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            DisplayName = displayName;
        }

        public void Navigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public string GetDisplayName()
        {
            return DisplayName;
        }

        public IWebDriver getDriver()
        {
            return Driver;
        }
    }

}
