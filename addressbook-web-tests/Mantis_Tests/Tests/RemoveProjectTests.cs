using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class RemoveProjectTests : ProjectTestBase
    {
        [Test]
        public void RemoveProjectTest()
        {
            int indexProject = 0;

            List<ProjectData> oldProjects = ProjectData.GetAll();

            ProjectData toBeRemoved = oldProjects.ElementAt(indexProject);

            app.Projects.Remove(toBeRemoved);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();

            oldProjects.Remove(toBeRemoved);

            //  Assert.AreEqual(oldProjects, newProjects);
            app.Projects.AreListEqual(oldProjects, newProjects);
        }
    }
}
