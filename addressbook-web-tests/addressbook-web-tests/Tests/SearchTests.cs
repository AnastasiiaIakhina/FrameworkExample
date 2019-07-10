using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
     [Test]   
        public void TestSearch()
        {
            string text = "1";

            List<ContactData> allContacts = app.Contacts.GetContactList();

            text = app.Contacts.GetCorrectText(allContacts);

            app.Contacts.Search(text);

            List<ContactData> searchResult = app.Contacts.GetDisplayedList();

            Assert.AreEqual(app.Contacts.GetNumberOfSearchResults(), searchResult.Count);

        }

     [Test]
     public void TestSearchData()
     {
         ContactData newContact = app.Contacts.GenerateContact();

         app.Contacts.CreateContact(newContact);

         app.Contacts.Search(newContact.Lastname);

         List<ContactData> searchResult = app.Contacts.GetDisplayedList();

         Assert.IsTrue(app.Contacts.SearchContact(newContact, searchResult));

     }

     [Test]
     public void EmptyTestSearch()
     {
         app.Contacts.DeleteAllContacts();

         ContactData newContact = app.Contacts.GenerateContact();

         app.Contacts.CreateContact(newContact);

         app.Contacts.Search(newContact.Lastname.Substring(0, newContact.Lastname.Length - 1) + "aaaaaaaa");

         Assert.IsTrue(app.Contacts.GetNumberOfSearchResults() == 0);
     }
    }
}
