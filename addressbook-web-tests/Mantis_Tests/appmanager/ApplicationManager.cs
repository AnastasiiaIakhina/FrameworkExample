using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;

namespace mantis_tests
{
    public class ApplicationManager
    {
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            Driver = new FirefoxDriver(new FirefoxBinary("C:/Program Files (x86)/Mozilla Firefox/Firefox.exe"), new FirefoxProfile(), TimeSpan.FromMinutes(3));
            BaseURL = "http://localhost/mantisbt-1.2.17/";
            Registration = new RegistrationHelper(this);
            // Ftp = new FtpHelper(this);
            Auth = new LoginHelper(this);
            Projects = new ProjectManagerHelper(this);
            Menu = new ManagementMenuHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstanse = new ApplicationManager();
                //  newInstanse.Driver.Url = "http://localhost/mantisbt-1.2.17/login_page.php";
                newInstanse.Menu.OpenHomePage();
                app.Value = newInstanse;
            }
            return app.Value;
        }

        public IWebDriver Driver { get; set; }

        public string BaseURL { get; set; }

        public RegistrationHelper Registration { get; set; }

        public FtpHelper Ftp { get; set; }

        public LoginHelper Auth { get; set; }

        public ProjectManagerHelper Projects { get; set; }

        public ManagementMenuHelper Menu { get; set; }
    }
}
