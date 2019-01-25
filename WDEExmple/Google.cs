using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WDEExmple.PageObjects;

namespace WDEExmple
{
    [TestFixture]
    public class Google
    {
        private IWebDriver Browser { get; set; }

        [SetUp]
        public void SettUp()
        {
            Browser = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            Browser.Quit();
        }

        [Test]
        public void SearchGoogle()
        {
            GoogleLanding landing = new GoogleLanding(Browser);
            landing.Navigate("http://www.google.com");
            landing.SendKeys_SearchBar("Sting Ray");
            landing.Enter_SearchBar();

            landing.Click_Result(2);

            Assert.IsTrue(Browser.PageSource.Contains("Stingray"));
        }
    }
}
