using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended
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
        /// Navigate to an address
        /// </summary>
        /// <param name="driver"></param>
        void Navigate(IWebDriver driver);

        /// <summary>
        /// Do any clenup the page object needs
        /// </summary>
        void TearDown();
    }
}
