using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebDriverExtended.Page
{
    public class Screen : IPageObject
    {

        public IWebDriver Driver { get; protected set; }
        public string DisplayName { get; protected set; }

        private WebDriverWait PageWait;

        public Screen(IWebDriver driver, string displayName)
        {
            Driver = driver;
            PageWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            DisplayName = displayName;
        }
    }

}
