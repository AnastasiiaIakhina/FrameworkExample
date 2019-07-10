using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            CreateNewContact();
            FillInNewContactForm(contact);
            SubmitNewContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData NewData)
        {
            manager.Navigator.OpenHomePage();
            manager.Navigator.CheckIfRightPage();
            if (ContactsExist())
            {
                SelectContact(p);
                FillInNewContactForm(NewData);
                SubmitContactModification();
                ReturnToHomePage();
                return this;
            }
            ContactData contact = new ContactData();
            contact.FirstName = "Test";
            CreateNewContact();
            FillInNewContactForm(contact);
            SubmitNewContactCreation();
            ReturnToHomePage();
            SelectContact(p);
            FillInNewContactForm(NewData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }


        public string NumberListContact()
        {
            return driver.FindElement(By.Id("search_count")).Text;
        }

        public ContactHelper Remove(int p)
        {

            manager.Navigator.OpenHomePage();
            manager.Navigator.CheckIfRightPage();
            if (ContactsExist())
            {
                SelectContact(p);
                RemoveContact();
                return this;
            }
            ContactData contact = new ContactData();
            contact.FirstName = "";
            CreateNewContact();
            FillInNewContactForm(contact);
            SubmitNewContactCreation();
            ReturnToHomePage();
            SelectContact(p);
            RemoveContact();
            return this;
        }

        public ContactHelper CreateNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public void SelectAllContacts()
        {
            driver.FindElement(By.Id("MassCB")).Click();
        }

        public void DeleteAllContacts()
        {
            manager.Navigator.OpenHomePage();
            string number = NumberListContact();
            if (number == "0") return;
            SelectAllContacts();
            RemoveContact();
            manager.Navigator.CheckIfRightPage();
        }


        public ContactHelper FillInNewContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText("15");
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText("July");
            Type(By.Name("byear"), contact.Byear);
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText("1");
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText("March");
            Type(By.Name("ayear"), contact.Ayear);
            //            new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText("TestGroup2");
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitNewContactCreation()
        {
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            // ERROR: Caught exception [Error: Dom locators are not implemented yet!], so has been changed to CssSelector("input[type=\"submit\"]")
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[3]")).Click();
            contactCache = null;
            return this;
        }

        public bool ContactsExist()
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                manager.Navigator.CheckIfRightPage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    ContactData contact = new ContactData();
                    contact.FirstName = element.Text;
                    contact.Lastname = element.Text;
                    contact.Id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    contactCache.Add(contact);

                }
            }
            return new List<ContactData>(contactCache);
        }


        public int GetContactCount()
        {
           return driver.FindElements(By.Name("entry")).Count;
        }

        internal ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            manager.Navigator.CheckIfRightPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData()
            {
                Address = address,
                AllPhones = allPhones,
            };
        }

        internal ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            manager.Navigator.CheckIfRightPage();
            SelectContact(0);

            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData()
            {
                FirstName = firstname,
                Lastname = lastname,
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone
            };
        }

        public ContactData GetContactInformationFromInfo(int index)
        {
            manager.Navigator.OpenHomePage();
            OpenContactInfo(index);

            return new ContactData()
            {
                AllData = driver.FindElement(By.Id("content")).Text
            };
        }

        public void OpenContactInfo(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
        }

        public void BeforeTest(int index)
        {
            manager.Navigator.OpenHomePage();
            if (IsContactNeeded(index))
            {
                int number = HowMuchContactsAreNeeded(index);
                for (int count = 1; count <= number; count++)
                {
                    CreateContact(new ContactData() { Lastname = "name" });
                }
            }
        }

        public bool IsContactNeeded(int index)
        {
            return IsListContactEmpty()
                || (!IsListContactEmpty() && !IsContactExists(index));
        }

        public bool IsListContactEmpty()
        {
            return driver.FindElement(By.Id("search_count")).Text == "0";
        }

        public bool IsContactExists(int index)
        {
            return !IsListContactEmpty()
            && IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]"));
        }

        public int HowMuchContactsAreNeeded(int index)
        {
            if (IsListContactEmpty()) return (index + 1);
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//input[@type='checkbox']"));
            int el = elements.Count - 1;
            return index - el + 1;
        }

        public bool IsEqualsContacts(ContactData contact1, ContactData contact2)
        {
            if (contact1.AllData == contact2.AllData) return true;
            return false;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            manager.Navigator.CheckIfRightPage();
            string text = driver.FindElement(By.TagName("label")).Text;
            manager.Navigator.OpenHomePage();
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public string GetCorrectText(List<ContactData> dataList)
        {
            string text = "";
            int i = 0;
            int count = 0;
            Random random = new Random();
            char[] chars = new char[36] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            while (text == "")
            {
                while (i < 3 && count < 20)
                {
                    text += chars[random.Next(0, 35)];
                    if (IsTextContain(dataList, text)) { i++; }
                    else text = text.Substring(0, text.Length - 1);
                    count++;
                }
            }
            return text;
        }

        public bool IsTextContain(List<ContactData> dataList, string text)
        {
            bool isContain = false;
            int i = 0;
            while (!isContain && i < dataList.Count)
            {
                if (dataList.ElementAt(i).ToString().Contains(text))
                {
                    isContain = true;
                }
                i++;
            }
            return isContain;
        }

        public List<ContactData> GetDisplayedList()
        {
            List<ContactData> contactList = new List<ContactData>();
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
            ICollection<IWebElement> contactData = null;
            foreach (IWebElement element in elements)
            {
                if (element.Displayed)
                {
                    contactData = element.FindElements(By.TagName("td"));
                    contactList.Add(new ContactData()
                    {
                        Lastname = contactData.ElementAt(1).Text,
                        FirstName = contactData.ElementAt(2).Text,
                        Address = contactData.ElementAt(3).Text,
                        AllEmails = contactData.ElementAt(4).Text,
                        AllPhones = contactData.ElementAt(5).Text,
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return contactList;
        }

        public void Search(string text)
        {
            manager.Navigator.OpenHomePage();
            Type(By.Name("searchstring"), text);
        }

        public ContactData GenerateContact()
        {
            ContactData contact = new ContactData() { Lastname = "", FirstName = "", Address = "", Mobile = "", Home = "", Work = "", Email2 = "", Email3 = "" };
            Random random = new Random();
            char[] chars = new char[36] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < 5; i++) { contact.Lastname += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.FirstName += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Address += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Mobile += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Home += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Work += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Email2 += chars[random.Next(0, 35)]; }
            for (int i = 0; i < 5; i++) { contact.Email3 += chars[random.Next(0, 35)]; }
            return contact;
        }

        public bool SearchContact(ContactData contact, List<ContactData> contacts)
        {
            bool isExist = false;
            int i = 0;
            while (!isExist && i < contacts.Count)
            {
                if (IsContact(contact, contacts.ElementAt(i)))
                {
                    if (IsEqualsContacts(contact, GetContactInformationFromInfo_ID(contacts.ElementAt(i).Id)))
                        isExist = true;
                }
                i++;
            }
            return isExist;
        }

        public ContactData GetContactInformationFromInfo_ID(string id)
        {
            manager.Navigator.OpenHomePage();
            OpenContactInfo_ID(id);

            return new ContactData()
            {
                AllData = driver.FindElement(By.Id("content")).Text
            };
        }

        public void OpenContactInfo_ID(string id)
        {
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

            int i = -1;
            int index = 0;
            while (i == (-1) && index < elements.Count)
            {
                if (elements.ElementAt(index).FindElement(By.TagName("input")).GetAttribute("value") == id)
                {
                    i = index;
                    elements.ElementAt(i).FindElements(By.TagName("td")).ElementAt(6).FindElement(By.TagName("a")).Click();
                }
                index++;
            }

        }

        public bool IsContact(ContactData contact1, ContactData contact2)
        {
            if (contact1.FirstName == contact2.FirstName
                && contact1.Lastname == contact2.Lastname
                && contact1.Address == contact2.Address
                && contact1.AllEmails == contact2.AllEmails
                && contact1.AllPhones == contact2.AllPhones) return true;

            return false;
        }
    }
}
