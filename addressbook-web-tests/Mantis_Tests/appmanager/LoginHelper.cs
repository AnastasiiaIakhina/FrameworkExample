using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {

        public LoginHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            FillLoginForm(account);
            SubmitLogin();
        }

        public void VerifyAccount()
        {
            FillPassword();
            SubmitLogin();
        }

        private void FillPassword()
        {
            Type(By.Name("password"), "root");
        }

        public void SubmitLogin()
        {
            Click(By.CssSelector("input.button"));
        }

        public void FillLoginForm(AccountData account)
        {
            Type(By.Name("username"), account.Name);
            Type(By.Name("password"), account.Password);
        }

        public void Logout()
        {
            if (!IsLoggedIn()) return;
            Click(By.LinkText("Logout"));
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.LinkText("Logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Name;
        }

        public string GetLoggedUserName()
        {
            string text = FindElement(By.CssSelector("td.login-info-left")).FindElement(By.ClassName("italic")).Text;
            return text;
        }

        public void IsLoginSuccuss(AccountData account)
        {
            Assert.IsTrue(IsLoggedIn(account));
        }

        public void IsLoginFail(AccountData account)
        {
            Assert.IsFalse(IsLoggedIn(account));
        }
    }
}
