using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExtended.Reporting
{
    public interface ReportWriter
    {
        void IWritePreRun(RunReport RunToStore);

        void IWritePostRun(RunReport RunToStore);

        void IWritePreTest(TestReport TestToStore);

        void IWritePostTest(TestReport TestToStore);

        void IWriteStep(StepReport StepToStore);

    }
}
