using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended
{
    /// <summary>
    /// This advnace fourm of the page object interface 
    /// allows for a page object to be loaded from a file
    /// and have a key value pair of Elements.
    /// </summary>
    public interface IAdvancePageObject : IPageObject
    {
        /// <summary>
        /// Addes a dynamicElement to the key element list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="elementToAdd"></param>
        void AddElement(string name, DynamicElement elementToAdd);

        /// <summary>
        /// Loads a page object from a file
        /// </summary>
        /// <param name="filePath"></param>
        void LoadPage(string filePath);

        /// <summary>
        /// Removes an element from the key element store
        /// </summary>
        /// <param name="name"></param>
        void RemoveElement(string name);
    }
}
