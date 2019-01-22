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

        /// <summary>
        /// Is the current element displayed on the screen
        /// </summary>
        public bool Displayed
        {
            get
            {
                Find();
                return RootElement.Displayed;
            }
        }

        /// <summary>
        /// Is the current element enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                Find();
                return RootElement.Enabled;
            }
        }

        /// <summary>
        /// location of the element
        /// </summary>
        public Point Location
        {
            get
            {
                Find();
                return RootElement.Location;
            }
        }

        /// <summary>
        /// Is the current element selected
        /// </summary>
        public bool Selected
        {
            get
            {
                Find();
                return RootElement.Selected;
            }
        }

        /// <summary>
        /// element dimensions on the screen
        /// </summary>
        public Size Size
        {
            get
            {
                Find();
                return RootElement.Size;
            }
        }

        /// <summary>
        /// html tag of the element
        /// </summary>
        public string TagName
        {
            get
            {
                Find();
                return RootElement.TagName;
            }
        }

        /// <summary>
        /// Inner text of the current element instance
        /// </summary>
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

        /// <summary>
        /// Creates a dynamic element that uses the parent element as the base of the search
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayName"></param>
        /// <param name="parentElement"></param>
        public DynamicElement(IPageObject page, string displayName, DynamicElement parentElement)
        {
            ParentElement = parentElement;
            this.Driver = page.Driver;
            this.ParrentPage = page.DisplayName;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Creates a Dynamic Element
        /// </summary>
        /// <param name="driver">web driver instance to attach to the element</param>
        /// <param name="page">the name of the page this element belongs too</param>
        /// <param name="displayName"> the display name of the element (human readable)</param>
        public DynamicElement(IWebDriver driver, string page, string displayName)
        {
            this.ParentElement = null;
            this.Driver = driver;
            this.ParrentPage = page;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Constructor is used to convert a WebElement to a DynamicElement
        /// </summary>
        /// <param name="driver">web driver instance to attach to the element</param>
        /// <param name="rootElement">the webelement to convert</param>
        public DynamicElement(IWebDriver driver, IWebElement rootElement)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayName"></param>
        /// <param name="rootElement"></param>
        public DynamicElement(IPageObject page, string displayName, IWebElement rootElement)
        {
            this.RootElement = rootElement;
            this.Driver = page.Driver;
            this.DisplayName = "Unknown";
        }

        /// <summary>
        /// Find the element defined and place the result in the element cache
        /// </summary>
        /// <returns>current DynElement instance</returns>
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

        /// <summary>
        /// Clears the current DynElement. Used mostly for text fields.
        /// </summary>
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

        /// <summary>
        /// Click on the element
        /// </summary>
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

        /// <summary>
        /// Find a webelement using passed by
        /// </summary>
        /// <param name="by">criteria to search</param>
        /// <returns>webelement</returns>
        public IWebElement FindElement(By by)
        {
            Find();
            RootElement = RootElement.FindElement(by);
            return RootElement;
        }

        /// <summary>
        /// Find a dynamic element using the By passed
        /// </summary>
        /// <param name="by">search criteria to find the DynElement</param>
        /// <returns>single element match</returns>
        public DynamicElement FindDynamicElement(By by)
        {
            return new DynamicElement(Driver, RootElement.FindElement(by));
        }

        /// <summary>
        /// Find all dynamicelements that fits the provided search criteria.
        /// </summary>
        /// <param name="by">search criteria</param>
        /// <returns>a list of elements that match</returns>
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

        /// <summary>
        /// Return a list of dynamic elements using the currently attached search criteria.
        /// </summary>
        /// <returns>a list of elements that match</returns>
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

        /// <summary>
        /// Find all elements that fits the provided search criteria
        /// </summary>
        /// <param name="by">search criteria</param>
        /// <returns>a list of elements that match</returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            Find();
            return RootElement.FindElements(by);
        }

        /// <summary>
        /// Retrieve the named attribute from the current element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            this.Find();
            return RootElement.GetAttribute(attributeName);
        }

        /// <summary>
        /// Retrive the css retive the css value fot the attribute given
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetCssValue(string propertyName)
        {
            this.Find();
            return RootElement.GetCssValue(propertyName);
        }

        /// <summary>
        /// Send simulated keyboard keys to the current element instance
        /// </summary>
        /// <param name="text">text to input into element</param>
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

        /// <summary>
        /// Carry out a submit action on the current element
        /// </summary>
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

        /// <summary>
        /// Adds a search item to the DynElement
        /// </summary>
        /// <param name="byToAdd">By to add to the list of searches</param>
        /// <returns>Dynamic Element</returns>
        public DynamicElement AddSearch(By byToAdd)
        {
            SearchOptions.Add(byToAdd);
            return this;
        }

        public void ClearSearches()
        {
            SearchOptions.Clear();
        }
        /// <summary>
        /// Changes the display name to the give string
        /// </summary>
        /// <param name="name">Name to change display to</param>
        /// <returns>string containing the new</returns>
        public DynamicElement SetDisplayName(string name)
        {
            DisplayName = name;
            return this;
        }

        /// <summary>
        /// Returns current
        /// </summary>
        /// <returns>Current Webelement</returns>
        public IWebElement ReturnRoot()
        {
            return RootElement;
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Is the element on the current page object
        /// </summary>
        /// <returns>true = yes, false = no</returns>
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

        /// <summary>
        /// Waits for the element defined by the first By added to the search collection to exist on the page
        /// </summary>
        /// <param name="timeOutinSec">How long to wait fort the elements existence in secs</param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by">Selenium By</param>
        /// <returns>Returns a webelement if the condition exists</returns>
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
