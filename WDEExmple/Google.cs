using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WDEExmple.PageObjects;

namespace WDEExmple
{
    [TestFixture]
    public class Google
    {
        [Test]
        public void SearchGoogle()
        {
            IWebDriver Browser = new FirefoxDriver();

            GoogleLanding landing = new GoogleLanding(Browser);
            landing.Navigate("http://www.google.com");
            landing.SendKeys_SearchBar("Sting Ray");
            landing.Click_SearchButton();

            Thread.Sleep(5000);
            landing.Click_Result(2);
            Thread.Sleep(5000);

            Assert.AreEqual(Browser.Url, "http://www.autodesk.com/products/stingray/overview");

            Browser.Close();
        }
    }
}
