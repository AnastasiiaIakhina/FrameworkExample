using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
     [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {

         [Test]
         public void ContactModificationTest()
         {
            ContactData NewData = new ContactData();
            NewData.FirstName = "Elena";
            NewData.Middlename = "Maria";
            NewData.Lastname = "Smith";
            NewData.Nickname = "koza";
            NewData.Title = null;
            NewData.Company = "Google";
            NewData.Address = "dfgffg";
            NewData.Home = "111";
            NewData.Mobile = "222";
            NewData.Work = "333";
            NewData.Fax = "444";
            NewData.Email2 = "fff@mail.ru";
            NewData.Email3 = "aaa@list.ru";
            NewData.Homepage = "fgfdgfdg";
            NewData.Byear = "1988";
            NewData.Ayear = "2010";
            NewData.Address2 = "adwdfwafdwaf";
            NewData.Phone2 = "555";
            NewData.Notes = "blablabla";

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

             app.Contacts.Modify(0, NewData);

             Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

             List<ContactData> newContacts = app.Contacts.GetContactList();
             oldContacts[0].FirstName = NewData.FirstName;
             oldContacts[0].Lastname = NewData.Lastname;
             oldContacts.Sort();
             newContacts.Sort();
             Assert.AreEqual(oldContacts, newContacts);

             foreach (ContactData contact in newContacts)
             {
                 if (contact.Id == oldData.Id)
                 {
                     Assert.AreEqual(NewData.FirstName, contact.FirstName);
                 }
             }

         }
    }
}
