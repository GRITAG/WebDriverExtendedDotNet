using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended
{
    /// <summary>
    /// An object used to trake state with in a page object
    /// </summary>
    public class PageState
    {
        private string stateKey;
        private string stateValue;
        private DateTime lastInteraction;

        /// <summary>
        /// The key used to interact with a page state
        /// </summary>
        public string Key { get { return stateKey; } }

        /// <summary>
        /// The value of a page state
        /// </summary>
        public string Value
        {
            get { return stateValue; }

            set
            {
                stateValue = value;
                lastInteraction = DateTime.Now;
            }
        }

        /// <summary>
        /// the last time a state has been updated
        /// </summary>
        public DateTime LastInteraction
        {
            get
            {
                return lastInteraction;
            }
        }
        
        /// <summary>
        /// constuctor to track state
        /// </summary>
        /// <param name="key">used to interact with a state</param>
        /// <param name="value">the current value of the state</param>
        public PageState(string key, string value)
        {
            stateKey = key;
            Value = value;
        }
    }
}
