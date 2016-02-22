using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;

namespace WebDriverExtended
{
    public class DynamicElement : IWebElement
    {
        private IWebDriver Driver;
	    private IWebElement RootElement;
        private List<By> SearchOptions = new List<By>();
        public string DisplayName { get; set; }

        IReport Reporting;

        public bool Displayed
        {
            get
            {
                Find();
                return RootElement.Displayed;
            }
        }

        public bool Enabled
        {
            get
            {
                Find();
                return RootElement.Enabled;
            }
        }

        public Point Location
        {
            get
            {
                Find();
                return RootElement.Location;
            }
        }

        public bool Selected
        {
            get
            {
                Find();
                return RootElement.Selected;
            }
        }

        public Size Size
        {
            get
            {
                Find();
                return RootElement.Size;
            }
        }

        public string TagName
        {
            get
            {
                Find();
                return RootElement.TagName;
            }
        }

        public string Text
        {
            get
            {
                Find();
                return RootElement.Text;
            }
        }

        public DynamicElement(IWebDriver driver, string displayName = "Unknown")
        {
            this.Driver = driver;
        }

        public DynamicElement(IWebDriver driver, IReport reporting, string displayName = "Unknown") : this(driver, displayName)
        {
            Reporting = reporting;
        }

        private DynamicElement(IWebDriver driver, IWebElement rootElement)
        {
            RootElement = rootElement;
            Driver = driver;
            DisplayName = "Unknown";
        }

        private DynamicElement Find()
        {
            if (RootElement == null || ElementStale() == true)
            {

                foreach (By currentBy in SearchOptions)
                {
                    try
                    {
                        RootElement = Driver.FindElement(currentBy);
                        return this;
                    }
                    catch (Exception e)
                    {
                        // contiune on
                    }
                }

                if (Reporting != null) Reporting.Validate("Could not find the element " + DisplayName, false);
                return this;
            }

            return this;
        }

        private bool ElementStale()
        {
            try
            {
                bool staleCheck = RootElement.Enabled;
                return false;

            }
            catch (StaleElementReferenceException)
            {

                return true;
            }
        }

        public void Clear()
        {
            this.Find();
            this.RootElement.Clear();
        }

        public void Click()
        {
            this.Find();
            this.RootElement.Click();
        }

        public IWebElement FindElement(By by)
        {

            RootElement = RootElement.FindElement(by);
            return RootElement;
        }

        public DynamicElement FindDynamicElement(By by)
        {
            return new DynamicElement(Driver, RootElement.FindElement(by));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            //return rootElement.FindElements(by);
            throw new Exception("Improrper use of element type");
        }

        public string GetAttribute(string attributeName)
        {
            this.Find();
            return RootElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            Find();
            return RootElement.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            Find();
            RootElement.SendKeys(text);
        }

        public void Submit()
        {
            Find();
            RootElement.Submit();
        }

        public DynamicElement AddSearch(By byToAdd)
        {
            SearchOptions.Add(byToAdd);
            return this;
        }

        public DynamicElement SetDisplayName(string name)
        {
            DisplayName = name;
            return this;
        }
    }
}
