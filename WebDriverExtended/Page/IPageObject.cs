using OpenQA.Selenium;

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
