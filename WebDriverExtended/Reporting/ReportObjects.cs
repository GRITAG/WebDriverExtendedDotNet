using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended.Reporting
{
    /// <summary>
    /// Static refrence to the reporting Object
    /// Use this to execercise the properly implemented Database reporting objects
    /// </summary>
    public static class ReportingObject
    {
        public static ReportWriter reportWriter;
    }


    public abstract class RunReport
    {

        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ID { get; set; }
        public string Description { get; set; }

        public RunReport(string RunName = "", string RunDescription = "")
        {
            ID = Guid.NewGuid();
            StartTime = new DateTime();
            Description = string.Empty;
            RunName = string.Empty;
            EndTime = new DateTime();

        }

        public RunReport(Guid runID, string RunName = "", string RunDescription = "")
        {
            this.ID = runID;
            this.Name = RunName;
            this.Description = RunDescription;
            this.StartTime = new DateTime();
            this.EndTime = new DateTime();
        }

        public virtual void StartRun()
        {
            this.StartTime = DateTime.Now;
            ReportingObject.reportWriter.IWritePreRun(this);

        }
        public virtual void CompleteRun()
        {
            this.EndTime = DateTime.Now;
            ReportingObject.reportWriter.IWritePostRun(this);
        }

    }

    public class TestReport
    {

        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid RunID { get; set; }
        public bool OutCome { get; set; }
        public List<StepReport> StepOutCome { get; set; }



        public TestReport(string TestName, Guid RunGuid, string description = "")
        {
            Name = TestName;
            RunID = RunGuid;
            ID = Guid.NewGuid();
            StartTime = DateTime.Now;
            StepOutCome = new List<StepReport>();
            Description = description;
            ReportingObject.reportWriter.IWritePreTest(this);
        }
        public void AddStep(StepReport StepToAdd)
        {
            this.StepOutCome.Add(StepToAdd);
        }

        public TestReport GetTestObject()
        {
            return this;
        }

        public void FinishTest()
        {
            this.EndTime = DateTime.Now;
            this.OutCome = true;
            foreach (StepReport x in this.StepOutCome)
            {
                if (x.OutCome != true)
                {
                    this.OutCome = false;

                }
                else
                {
                    continue;
                }

            }
            ReportingObject.reportWriter.IWritePostTest(this);
        }

    }

    public class StepReport
    {
        ReportWriter reportWriter { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ID { get; set; }
        public string StepDescription { get; set; }
        public Guid TestID { get; set; }
        public bool OutCome { get; set; }
        public byte[] ScreenShot { get; set; }

        public StepReport(string step_Description, TestReport currentTest, bool outcome = true, byte[] screenshot = null) : this(step_Description, currentTest.ID, outcome, screenshot) { }

        public StepReport(string step_Description, Guid TestGuid, bool outcome = true, byte[] screenshot = null)
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            ID = Guid.NewGuid();
            StepDescription = step_Description;
            TestID = TestGuid;
            OutCome = outcome;
            ScreenShot = screenshot;
            ReportingObject.reportWriter.IWriteStep(this);


        }


    }

}
