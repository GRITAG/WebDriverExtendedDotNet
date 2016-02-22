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
	    private IWebElement rootElement;
        private List<By> SearchOptions = new List<By>();

        public bool Displayed
        {
            get
            {
                Find();
                return rootElement.Displayed;
            }
        }

        public bool Enabled
        {
            get
            {
                Find();
                return rootElement.Enabled;
            }
        }

        public Point Location
        {
            get
            {
                Find();
                return rootElement.Location;
            }
        }

        public bool Selected
        {
            get
            {
                Find();
                return rootElement.Selected;
            }
        }

        public Size Size
        {
            get
            {
                Find();
                return rootElement.Size;
            }
        }

        public string TagName
        {
            get
            {
                Find();
                return rootElement.TagName;
            }
        }

        public string Text
        {
            get
            {
                Find();
                return rootElement.Text;
            }
        }

        public DynamicElement(IWebDriver driver)
        {
            this.Driver = driver;
        }

        private DynamicElement Find()
        {
            rootElement = null;

            foreach(By currentBy in SearchOptions)
            {
                try
                {
                    rootElement = Driver.FindElement(currentBy);
                    return this;
                }
                catch(Exception e)
                {
                    // contiune on
                }
            }


            return null;
        }

        public void Clear()
        {
            this.Find();
            this.rootElement.Clear();
        }

        public void Click()
        {
            this.Find();
            this.rootElement.Click();
        }

        public IWebElement FindElement(By by)
        {

            rootElement = rootElement.FindElement(by);
            return rootElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            //return rootElement.FindElements(by);
            throw new Exception("Improrper use of element type");
        }

        public string GetAttribute(string attributeName)
        {
            this.Find();
            return rootElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            Find();
            return rootElement.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            Find();
            rootElement.SendKeys(text);
        }

        public void Submit()
        {
            Find();
            rootElement.Submit();
        }

        public DynamicElement AddSearch(By byToAdd)
        {
            SearchOptions.Add(byToAdd);
            return this;
        }
    }
}
