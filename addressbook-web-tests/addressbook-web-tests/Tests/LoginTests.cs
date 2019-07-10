using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]

        public void LoginWithValidCredentials()
        {
            //preparing test situation
            app.Auth.Logout();

            //action
            AccountData account = new AccountData();
            account.Username = "admin";
            account.Password = "secret";
            app.Auth.LogIn(account);

            //verification
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]

        public void LoginWithInvalidCredentials()
        {
            //preparing test situation
            app.Auth.Logout();

            //action
            AccountData account = new AccountData();
            account.Username = "admin";
            account.Password = "123456";
            app.Auth.LogIn(account);

            //verification
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
