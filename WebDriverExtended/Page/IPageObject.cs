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
        /// <summary>
        /// Navigate to an address
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        void Navigate(IWebDriver driver, string url);

        /// <summary>
        /// Navigate to an address
        /// </summary>
        /// <param name="url"></param>
        void Navigate(string url);

        /// <summary>
        /// Get the the Display name used in reporting
        /// </summary>
        /// <returns>Displyname of an object as a string</returns>
        string GetDisplayName();

        /// <summary>
        /// Gets the driver
        /// </summary>
        /// <returns>Driver</returns>
        IWebDriver getDriver();

    }
}
