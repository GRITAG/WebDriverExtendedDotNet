using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;
using WebDriverExtended.Reporting;
using WebDriverExtended.Page;
using System.Diagnostics;

namespace WebDriverExtended
{
    public class DynamicElement : IWebElement
    {
        public string DisplayName { get; set; }
	    protected IWebElement RootElement;
        protected string ParrentPage { get; set; }
        private List<By> SearchOptions = new List<By>();
        private DynamicElement ParentElement;
        private Boolean NoCache = false;
        private IWebDriver Driver;

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
                this.Find();
                return RootElement.Text;
            }
        }
        private DynamicElement(IPageObject page, string displayName) : this(page, displayName, null)
        {

        }

        private DynamicElement(IPageObject page, string displayName, DynamicElement parentElement)
        {
            ParentElement = parentElement;
            this.Driver = page.getDriver();
            this.ParrentPage = page.GetDisplayName();
            this.DisplayName = displayName;
        }

        private DynamicElement(IWebDriver driver, string page, string displayName)
        {
            this.ParentElement = null;
            this.Driver = driver;
            this.ParrentPage = page;
            this.DisplayName = displayName;
        }

        private DynamicElement(IWebDriver driver, IWebElement rootElement)
        {
            this.ParentElement = null;
            this.RootElement = rootElement;
            this.Driver = driver;
            DisplayName = "Unknown";
        }

        public DynamicElement(IWebDriver driver, string displayName = "Unknown")
        {
            this.Driver = driver;
        }


        public DynamicElement(IWebDriver driver, IReport reporting, string displayName = "Unknown") : this(driver, displayName)
        {
            Reporting = reporting;
        }

        private DynamicElement(IPageObject page, string displayName, IWebElement rootElement)
        {
            this.RootElement = rootElement;
            this.Driver = page.getDriver();
            this.DisplayName = "Unknown";
        }

        private DynamicElement Find()
        {
            try
            {
                if (RootElement == null || NoCache)
                {

                    foreach (By currentBy in SearchOptions)
                    {
                        try
                        {
                            if (ParentElement == null)
                            {
                                RootElement = Driver.FindElement(currentBy);
                            }
                            else
                            {
                                RootElement = ParentElement.FindElement(currentBy);
                            }
                        }
                        catch (Exception e)
                        {
                            //todo: add logging
                            // contiune on
                        }
                    }

                    throw new WebDriverException("Error finding " + DisplayName + " on the page / screen" + ParrentPage);
                }
                return this;
            } catch (Exception e)
            {
                throw new WebDriverException("Error finding " + DisplayName + " on the page / screen" + ParrentPage + "\n" + e.ToString());
            }

            
        }

        /// <summary>
        /// Turn element search cache off
        /// </summary>
        /// <returns></returns>
        public DynamicElement CacheOff()
        {
            NoCache = true;
            return this;
        }

        /// <summary>
        /// Turn element search cache on
        /// </summary>
        /// <returns></returns>
        public DynamicElement CacheOn()
        {
            NoCache = false;
            return this;
        }

        public void Clear()
        {
            this.Find();
            try
            {
                this.RootElement.Clear();
            }
            catch (Exception e)
            {
                throw new WebDriverException("Error clearing " + DisplayName + " on the page / screen" + ParrentPage + "\n" + e.ToString()); 
            }
        }

        public void Click()
        {
            this.Find();
            try
            {
                this.RootElement.Click();
            } catch (Exception e)
            {
                throw new WebDriverException("Error clicking " + DisplayName + " on the page / screen" + ParrentPage + "\n" + e.ToString());
            } 
        }

        public IWebElement FindElement(By by)
        {
            Find();
            RootElement = RootElement.FindElement(by);
            return RootElement;
        }

        public DynamicElement FindDynamicElement(By by)
        {
            return new DynamicElement(Driver, RootElement.FindElement(by));
        }

        public List<DynamicElement> FindDynamicElements(By by)
        {
            this.Find();
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

            }

            return elementsToReturn;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            Find();
            return RootElement.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            this.Find();
            return RootElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            this.Find();
            return RootElement.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            Find();
            try
            {
                RootElement.SendKeys(text);
            }
            catch (Exception e)
            {
                throw new WebDriverException("Error with send keys on " + DisplayName + " on the page / screen" + ParrentPage + "\n" + e.ToString() + " with input of " + text); //report.writeStep("Click element " + displayName);
            }
        }

        public void Submit()
        {
            Find();
            try
            {
                this.RootElement.Submit();
            }
            catch (Exception e)
            {
                throw new WebDriverException(
                    "Error with submitting " + DisplayName + " on the page / screen" + ParrentPage + "\n" + e.ToString()); //report.writeStep("Click element " + displayName);
            }
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

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public Boolean Exists()
        {
            DynamicElement Result;
            try
            {
                Result = Find();
            }
            catch (Exception e)
            {
                //Todo: Add logging
                Result = null;
            }

            if (Result == null || Result.RootElement == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Waits for the element defined by the first By added to the search collection to not be enabled on the page
        /// </summary>
        /// <param name="timeOutinSec">How long to wait fort the elements existence in secs</param>
        public void WaitForInvsibility(int timeOutinSec)
        {
            if (SearchOptions.Count() > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                while (timer.Elapsed < TimeSpan.FromSeconds(timeOutinSec))
                {
                    foreach (By currentBy in SearchOptions)
                    {
                            IWebElement foundElement = CheckCondition(currentBy);
                        try
                        {
                            Boolean visibilityState = foundElement.Enabled;
                            if (visibilityState == false) return;
                        }
                        catch (StaleElementReferenceException elementNoLongerVisible)
                        {
                            //ToDo: Add logging
                            // Returns out of the loop because stale element reference implies that element
                            // is no longer visible.
                            return;
                        }
                        catch (NoSuchElementException elementDoesntExist)
                        {
                            // Returns out of the loop because the element is not present in DOM. The
                            // try block checks if the element is present but is invisible.
                            return;
                        }
                        catch (NullReferenceException elementClearedOut)
                        {
                            //DynElement clears out the element when it doest exist on the page anymore
                            return;
                        }
                    }
                }
            }
            else
            {
                throw new WebDriverException("No Search Bys to wait for");
            }
        }

        public void Wait(int timeOutinSec)
        {
            if (SearchOptions.Count() > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                while (timer.Elapsed < TimeSpan.FromSeconds(timeOutinSec))
                {
                    foreach (By currentBy in SearchOptions)
                    {
                        IWebElement foundElement = CheckCondition(currentBy);
                        if (foundElement != null)
                        {
                            if (foundElement.Displayed && foundElement.Enabled)
                            {
                                return;
                            }
                        }
                    }
                }
                throw new WebDriverException("Error waiting for element " + DisplayName);

            }
            else
            {
                throw new WebDriverException("No Search Bys to wait for");
            }
        }

        private IWebElement CheckCondition(By by)
        {
            try
            {
                return Driver.FindElement(by);
            }
            catch (WebDriverException wdException)
            {
                //ToDo Add logging
                return null;
            }
        }

    }
}
