using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebDriverExtended;
using WebDriverExtended.Page;

namespace WDEExmple.PageObjects
{
    public class GoogleBase : IPageObject, IStatefulPageObject
    {
        protected IWebDriver Driver = null;
        private List<PageState> states = new List<PageState>();
        public string Url = string.Empty;

        public GoogleBase(IWebDriver browser)
        {
            Driver = browser;
        }

        public void AddState(string key, string value)
        {
            states.Add(new PageState(key, value));
        }

        public PageState GetState(string key)
        {
            foreach (PageState currentStte in states)
            {
                if (currentStte.Key == key) return currentStte;
            }

            return null;
        }

        public void Navigate(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Navigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void RemoveState(string key)
        {
            for(int currentIndex = 0; currentIndex < states.Count; currentIndex++)
            {
                if (states[currentIndex].Key == key) states.RemoveAt(currentIndex);
            }
        }

        public virtual void Setup()
        {
            throw new NotImplementedException();
        }

        public virtual void TearDown()
        {
            throw new NotImplementedException();
        }

        public void UpdateState(string key, string value)
        {
            foreach(PageState currentState in states)
            {
                if (currentState.Key == key) currentState.Value = value;
            }
        }

        public string GetDisplayName()
        {
            throw new NotImplementedException();
        }

        public IWebDriver getDriver()
        {
            throw new NotImplementedException();
        }
    }
}
