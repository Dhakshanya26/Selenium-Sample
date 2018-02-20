using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;

namespace Serve.Platform.Configuration.UI.SeleniumTest.FunctionalTests
{
    [Ignore]
    [TestClass]
    public class LoginUITest : SeleniumTestBase
    {
        [TestMethod]
        public void UnAuthorize()
        {
            Helper.GoToPage(string.Empty, Driver.Instance);
            var acturalResult=Driver.Instance.FindElement(By.TagName("h2")).Text;
            Assert.AreEqual(acturalResult, "Access Denied - You are not an authorized user");
        }
    }
}
