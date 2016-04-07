using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;
using WebDriverExtended.Reporting;

namespace WebDriverExtended
{
    public class DynamicElement : IWebElement
    {
        private IWebDriver Driver;
	    protected IWebElement RootElement;
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
            if (Reporting != null) Reporting.WriteStep(String.Format(StringLocalization.ClearReportText, DisplayName));
            this.RootElement.Clear();
        }

        public void Click()
        {
            this.Find();
            if (Reporting != null) Reporting.WriteStep(string.Format(StringLocalization.ClickReportText, DisplayName));
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

        public List<DynamicElement> FindDynamicElements(By by)
        {
            ReadOnlyCollection<IWebElement> baseElements = RootElement.FindElements(by);

            List<DynamicElement> elementsToReturn = new List<DynamicElement>();

            foreach(IWebElement currentElement in baseElements)
            {
                elementsToReturn.Add(new DynamicElement(Driver, currentElement));
            }

            return elementsToReturn;
        }

        public List<DynamicElement> FindDynamicElements()
        {
            bool foundElements = false;
            ReadOnlyCollection<IWebElement> elements  = null;
            List<DynamicElement> elementsToReturn = new List<DynamicElement>();

            foreach (By currentSearch in SearchOptions)
            {
                if (!foundElements)
                {
                    elements = Driver.FindElements(currentSearch);
                    if (elements.Count > 0) foundElements = true;
                }
            }

            if(foundElements)
            {
                foreach (IWebElement currentElement in elements)
                {
                    elementsToReturn.Add(new DynamicElement(Driver, currentElement));
                }

                elementsToReturn.Reverse();

                return elementsToReturn;
            }

            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return RootElement.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            this.Find();
            if (Reporting != null) Reporting.WriteStep(string.Format(StringLocalization.GetAttReportText, attributeName, DisplayName));
            return RootElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            Find();
            if (Reporting != null) Reporting.WriteStep(string.Format(StringLocalization.GetCSSReportText, propertyName, DisplayName));
            return RootElement.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            Find();
            if (Reporting != null) Reporting.WriteStep(string.Format(StringLocalization.SendKeyReportText, text, DisplayName));
            RootElement.SendKeys(text);
        }

        public void Submit()
        {
            Find();
            if (Reporting != null) Reporting.WriteStep(string.Format(StringLocalization.SubmitReportText, DisplayName));
            RootElement.Submit();
        }

        public DynamicElement AddSearch(By byToAdd)
        {
            SearchOptions.Add(byToAdd);
            return this;
        }

        public void ClearSearches()
        {
            SearchOptions.Clear();
        }

        public DynamicElement SetDisplayName(string name)
        {
            DisplayName = name;
            return this;
        }

        public IWebElement ReturnRoot()
        {
            return RootElement;
        }
    }
}
