using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class ProjectTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareProjectsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ProjectData> fromUI = app.Projects.GetProjectsList();
                List<ProjectData> fromDB = ProjectData.GetAll();

                app.Projects.AreListEqual(fromUI, fromDB);
            }
        }
    }
}
