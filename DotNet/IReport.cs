using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended
{
    public interface IReport
    {
        /// <summary>
        /// Prep the reporting
        /// </summary>
        void PrepReporting();

        /// <summary>
        /// Validate a check on the page or object
        /// </summary>
        /// <param name="text"></param>
        /// <param name="passFail"></param>
        /// <param name="pasitiveCheck"></param>
        /// <param name="screenShot"></param>
        void Validate(string text, bool passFail, bool pasitiveCheck = true, byte[] screenShot = null);

        /// <summary>
        /// Write a step to the test
        /// </summary>
        /// <param name="text"></param>
        void WriteStep(string text);

        /// <summary>
        /// Finish wrtiting the report to the store
        /// </summary>
        void WriteReport();
    }
}
