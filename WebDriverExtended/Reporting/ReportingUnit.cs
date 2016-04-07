using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended.Reporting
{
    public class ReportingUnit : IReport
    {


        public TestReport CurrentTest { get; set; }

        public void PrepReporting(string TestName, Guid AssociatedRunID, string Description = "")
        {
            CurrentTest = new TestReport(TestName, AssociatedRunID, Description);
        }

        public void Validate(string text, bool passFail, bool positiveCheck, byte[] screenshot)
        {
            //if this is a positive check then a pass is true or false... if It isn't a positive check than the results are opposite
            if (positiveCheck)
            {
                CurrentTest.AddStep(new StepReport(text, CurrentTest, passFail, screenshot));
            }

            if (!positiveCheck)
            {
                bool decidedoutcome = true;

                if (passFail) { decidedoutcome = false; }
                else if (!passFail) { decidedoutcome = true; }

                CurrentTest.AddStep(new StepReport(text, CurrentTest, decidedoutcome, screenshot));
            }
            //TODO: Create new step outcome for Current TestID
        }

        public void WriteStep(string text)
        {
            //TODO: Create new step outcome for current TestID
            CurrentTest.AddStep(new StepReport(text, CurrentTest));
        }

        public void WriteReport()
        {
            CurrentTest.FinishTest();
        }

        public void PrepReporting()
        {
            throw new NotImplementedException();
        }
    }
}
