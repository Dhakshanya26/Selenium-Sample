using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Configuration;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;


namespace Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase
{
    public class SeleniumTestBase
    {

        [TestInitialize]
        public void Init()
        {
            Driver.Initialize();            
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
            
    }
}
