using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverExtended.Page
{
    class Page : IPageObject
    {
        protected IWebDriver Driver;
        protected string DisplayName;
        public string BaseUrl { get; set; }
        public string Path { get; set; }
        private WebDriverWait PageWait;
        public Boolean Mobile { get; protected set; }

        public Page(IWebDriver driver, string displayName)
        {
            Driver = driver;
            PageWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            DisplayName = displayName;
            Mobile = false;
        }

        public string getPageUrl()
        {
            if (Path != null)
            {
                return BaseUrl + Path;
            }
            return BaseUrl;
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Navigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
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
