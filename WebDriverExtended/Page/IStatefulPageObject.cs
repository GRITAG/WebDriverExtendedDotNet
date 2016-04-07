using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended.Page
{
    /// <summary>
    /// Adds statful functionality to a page object
    /// </summary>
    public interface IStatefulPageObject
    {
        /// <summary>
        /// Adds a state to the page object
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddState(string key, string value);

        /// <summary>
        /// retives a page state
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        PageState GetState(string key);

        /// <summary>
        /// updates a state to a page
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void UpdateState(string key, string value);

        /// <summary>
        /// remove a state from a page
        /// </summary>
        /// <param name="key"></param>
        void RemoveState(string key);
    }
}
