using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddingProjectTests : AuthTestBase
    {
        [Test]
        public void AddingProjectTest()
        {
            ProjectData newProject = new ProjectData("teseresert");

            List<ProjectData> oldProjects = ProjectData.GetAll();

            //           List<ProjectData> old = app.Projects.GetProjectsList();

            bool IsAvailableToAdding = app.Projects.CheckProjectData(newProject, oldProjects);

            int tries = 0;

            while (!IsAvailableToAdding && tries < 10)
            {
                newProject.Name = GenerateRandomString(10);
                IsAvailableToAdding = app.Projects.CheckProjectData(newProject, oldProjects);
                tries++;
            }

            if (IsAvailableToAdding)
            {
                app.Projects.AddProject(newProject);
                oldProjects.Add(newProject);
            }
            else
            {
                System.Console.Out.WriteLine("New project is not available to adding\n"
                    + "Error Message\n"
                    + app.Projects.GetMessageOfAddingError(newProject));
            }

            List<ProjectData> newProjects = ProjectData.GetAll();

            app.Projects.AreListEqual(oldProjects, newProjects);
        }

        [Test]
        public void AddingProjectTest2()
        {
            ProjectData newProject = new ProjectData("tdesehresert");

            List<ProjectData> oldProjects = ProjectData.GetAll();

            string error = app.Projects.AddNewProject(newProject);

            if (error != null)
                System.Console.Out.WriteLine("New project is not available to adding\n"
                    + "Error Message\n"
                    + error);
            else
            {
                List<ProjectData> newProjects = ProjectData.GetAll();
                oldProjects.Add(newProject);
                app.Projects.AreListEqual(oldProjects, newProjects);
            }

        }

    }
}
