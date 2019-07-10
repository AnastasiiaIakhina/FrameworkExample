using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        private string baseURL;

        public ManagementMenuHelper(ApplicationManager manager)
            : base(manager)
        {
            this.manager = manager;
            this.baseURL = manager.BaseURL;
        }

        public void OpenHomePage()
        {
            if (!manager.Auth.IsLoggedIn())
            {
                if (IsSignInPage()) return;
                GoToUrl(baseURL + "login_page.php");
            }
            else
            {
                if (IsMyViewPage()) return;
                Click(By.LinkText("My View"));
            }

        }

        public bool IsMyViewPage()
        {
            return Url() == baseURL + "my_view_page.php";
        }

        public bool IsSignInPage()
        {
            return Url() == baseURL + "login_page.php";
        }


        public void GoToManageProjectPage()
        {
            if (IsManageProjectPage())
            {
                if (!IsSessionExpired()) return;
                manager.Auth.VerifyAccount();
                return;
            }
            GoToManagePage();
            Click(By.LinkText("Manage Projects"));
        }

        public void GoToManagePage()
        {
            if (IsManagePage())
            {
                if (!IsSessionExpired()) return;
                manager.Auth.VerifyAccount();
                return;
            }
            Click(By.LinkText("Manage"));
            if (!IsSessionExpired()) return;
            manager.Auth.VerifyAccount();
        }

        public bool IsManagePage()
        {
            return Url() == baseURL + "manage_overview_page.php";
        }

        public bool IsSessionExpired()
        {
            return IsElementPresent(By.Name("password"));
        }

        public bool IsManageProjectPage()
        {
            return Url() == baseURL + "manage_proj_page.php";
        }
    }
}
