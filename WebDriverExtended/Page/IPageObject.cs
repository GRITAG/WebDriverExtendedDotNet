using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace WebDriverExtended.Page
{
    /// <summary>
    /// A basic page object interface
    /// </summary>
    public interface IPageObject
    {
        IWebDriver Driver { get; }
        string DisplayName { get; }
    }
}
