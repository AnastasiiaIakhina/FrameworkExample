using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace mantis_tests
{
    public class HelperBase
    {
        protected IWebDriver driver;
        protected ApplicationManager manager;
        private bool acceptNextAlert = true;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            this.driver = manager.Driver;
        }
        protected void Input(By locator, string text)
        {
            if (text == null || text == "") return;
            FindElement(locator).SendKeys(text);
        }

        protected void Type(By locator, string text)
        {

            if (text == null || text == "") return;
            FindElement(locator).Clear();
            FindElement(locator).SendKeys(text);
        }

        protected void Select(By locator, string text)
        {
            if (text == null || text == "" || text == "-") return;
            new SelectElement(FindElement(locator)).SelectByText(text);
        }

        protected ICollection<IWebElement> FindElements(By locator)
        {
            return driver.FindElements(locator);
        }

        protected IWebElement FindElement(By locator)
        {
            return driver.FindElement(locator);
        }

        protected string GetElementValue(By locator)
        {
            return FindElement(locator).GetAttribute("value");
        }

        protected void Click(By locator)
        {
            FindElement(locator).Click();
        }

        public int GetCountElements(By locator)
        {
            return FindElements(locator).Count;
        }

        protected string Url()
        {
            return driver.Url;
        }

        protected void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        protected string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        protected SelectElement GetSelectedElement(By locator)
        {
            return new SelectElement(FindElement(locator));
        }

        protected void CreateWebDriverWait()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

    }
}
