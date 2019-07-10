using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;

namespace mantis_tests
{
    public class ProjectManagerHelper : HelperBase
    {
        public ProjectManagerHelper(ApplicationManager manager) : base(manager) { }

        private List<ProjectData> projectCache = null;

        public List<ProjectData> GetProjectsList()
        {

            if (projectCache == null)
            {
                projectCache = new List<ProjectData>();
                manager.Menu.GoToManageProjectPage();
                ICollection<IWebElement> elements1 = FindElements(By.CssSelector("table.width100 > tbody > tr.row-1"));
                ICollection<IWebElement> elements2 = FindElements(By.CssSelector("table.width100 > tbody > tr.row-2"));
                IWebElement hreff;
                foreach (IWebElement element in elements1)
                {
                    hreff = element.FindElement(By.CssSelector("td > a"));
                    int index = hreff.GetAttribute("href").LastIndexOf("id=");
                    projectCache.Add(new ProjectData(hreff.Text)
                    {
                        Id = hreff.GetAttribute("href")
                        .Substring(index + 3)
                    });
                }
                foreach (IWebElement element in elements2)
                {
                    hreff = element.FindElement(By.CssSelector("td > a"));
                    int index = hreff.GetAttribute("href").LastIndexOf("id=");
                    projectCache.Add(new ProjectData(hreff.Text)
                    {
                        Id = hreff.GetAttribute("href")
                        .Substring(index + 3)
                    });
                }
            }
            return new List<ProjectData>(projectCache);
        }

        public void Remove(ProjectData project)
        {
            manager.Menu.GoToManageProjectPage();
            SelectProjectToEditingByID(project.Id);
            RemoveProject();
            SubmitProjectRemoval();
        }

        public void AddProject(ProjectData newProject)
        {
            manager.Menu.GoToManageProjectPage();
            InitprojectCreation();
            FillProjectForm(newProject);
            SubmitProjectCreation();
            ReturnToManageProjectPage();
        }

        public string AddNewProject(ProjectData newProject)
        {
            string error = null;
            manager.Menu.GoToManageProjectPage();
            InitprojectCreation();
            FillProjectForm(newProject);
            SubmitProjectCreation();
            error = GetMessageOfError(newProject);
            ReturnToManageProjectPage();
            return error;
        }

        public string GetMessageOfError(ProjectData newProject)
        {
            if (IsElementPresent(By.CssSelector("td.form-title")))
                return FindElement(By.CssSelector("td.form-title")).Text;
            return null;
        }

        public string GetMessageOfAddingError(ProjectData newProject)
        {
            manager.Menu.GoToManageProjectPage();
            InitprojectCreation();
            FillProjectForm(newProject);
            SubmitProjectCreation();
            return FindElement(By.CssSelector("td.form-title")).Text;
        }

        public void ReturnToManageProjectPage()
        {
            if (!IsElementPresent(By.CssSelector("td.form-title > form > input.button-small"))) return;
            Click(By.CssSelector("td.form-title > form > input.button-small"));
        }

        public void SubmitProjectCreation()
        {
            Click(By.CssSelector("input.button"));
            projectCache = null;
        }

        public void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Select(By.Name("status"), project.Status);
            Select(By.Name("view_state"), project.ViewStatus);
            Type(By.Name("description"), project.Description);
        }

        public void InitprojectCreation()
        {
            Click(By.CssSelector("td.form-title > form > input.button-small"));
        }

        public bool CheckProjectData(ProjectData project, List<ProjectData> projects)
        {
            if (project.Name == "" || project.Name == null) return false;
            int i = 0;
            while (i < projects.Count)
            {
                if (project.Name == projects.ElementAt(i).Name) return false;
                i++;
            }
            return true;
        }

        public void AreListEqual(List<ProjectData> oldProjects, List<ProjectData> newProjects)
        {
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }

        private void SubmitProjectRemoval()
        {
            Click(By.CssSelector("input.button"));
        }

        private void RemoveProject()
        {
            Click(By.XPath("//input[@value='Delete Project']"));
        }

        private void SelectProjectToEditingByID(string id)
        {
            Click((By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + id + "']")));
        }
    }
}
