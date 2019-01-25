using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverExtended;

namespace WDEExmple.PageObjects
{
    public class GoogleLanding : GoogleBase
    {
        DynamicElement SearchBar { get; set; }
        DynamicElement SearchButton { get; set; }
        DynamicElement Result { get; set; }

        public GoogleLanding(IWebDriver browser): base(browser)
        {
            Setup();
        }

        public override void Setup()
        {
            //base.Setup();

            AddState("ResultsAvaible", "false");

            SearchBar = new DynamicElement(Driver, "SearchBar")
                .AddSearch(By.CssSelector("input[name='q']"))
                .AddSearch(By.CssSelector("input[id='lst-ib']"));
            SearchButton = new DynamicElement(Driver, "SearchButton")
                .AddSearch(By.CssSelector("input[name='btnK']"));
            Result = new DynamicElement(Driver, "Search Result")
                .AddSearch(By.CssSelector("div[class='g'] div[class='rc'] a"));
        }

        public GoogleLanding SendKeys_SearchBar(string text)
        {
            SearchBar.SendKeys(text);
            UpdateState("ResultsAvaible", "true");
            return this;
        }

        public GoogleLanding Click_SearchButton()
        {
            if (GetState("ResultsAvaible").Value == "false")
            {
                SearchButton.Click();
            }
            else
            {
                SearchButton.ClearSearches();
                SearchButton.AddSearch(By.CssSelector("button[value='Search']"));
                SearchButton.Click();
            }
            return this;
        }

        public GoogleLanding Enter_SearchBar()
        {
            SearchBar.SendKeys(Keys.Enter);
            return this;
        }

        public void Click_Result(int index)
        {
            if(GetState("ResultsAvaible").Value == "true")
            {
                Result.Wait(30);

                List<DynamicElement> sortedElements = Result.FindDynamicElements();
                //sortedElements.Reverse();
                sortedElements[index].Click();
            }
        }
    }
}
