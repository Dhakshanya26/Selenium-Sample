using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;

namespace Serve.Platform.Configuration.UI.SeleniumTest.Pages
{
    public class ConfigAppNamespacePage
    {
        private static IWebDriver _driver;

        /// <summary>
        /// ConfigAppNamespacePage
        /// </summary>
        /// <param name="driver"></param>
        public ConfigAppNamespacePage(IWebDriver driver)
        {
            _driver = driver;
            Helper.GoToPage(Constants.ConfigAppNamespaceLink, _driver, "ads%5Ccpt_testacc_1:CupAta6a@");
        }


        #region Search by app namespace

        /// <summary>
        /// GetAppNamespaceGridRecordCount
        /// </summary>
        /// <returns>int</returns>
        public int GetAppNamespaceGridRecordCount()
        {
            var totalPageCount = Convert.ToInt32(_driver.FindElement(By.Id(string.Format(Constants.GridTotalPageCount, Constants.GridId))).Text);
            return totalPageCount;
        }

        /// <summary>
        /// SearchByAppNamespaceName
        /// </summary>
        /// <param name="appNamespaceName"></param>
        /// <returns>bool</returns>
        public bool SearchByAppNamespaceName(string appNamespaceName = null)
        {
            var isRecordsExisting = false;
            var name = string.Empty;

            if (string.IsNullOrEmpty(appNamespaceName))
            {
                var configAppNamespace = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
                name = configAppNamespace[0].ToString();
            }
            else
            {
                name = appNamespaceName;
            }

            _driver.FindElement(By.Id(Constants.SearchByNameTextBoxId)).SendKeys(string.Format("{0}{1}", name, OpenQA.Selenium.Keys.Enter));
            Thread.Sleep(2000);

            if (string.IsNullOrEmpty(appNamespaceName))
            {
                var result = Helper.ValidateGridColumnValue(Constants.GridId, _driver, "Name", null);
                result.ForEach(item =>
                {
                    isRecordsExisting = item.Contains(Constants.NoRecordsFoundMessage);
                });
                return !isRecordsExisting;
            }
            return true;
        }


        /// <summary>
        /// ClearImageIconInSearchByNameTextBox
        /// </summary>
        /// <returns>int</returns>     
        public int ClearImageIconInSearchByNameTextBox()
        {
            IWebElement hiddenWebElement = _driver.FindElement(By.XPath("//div[@id='body']/div/section/div/div[2]/div/div"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", hiddenWebElement);
            return GetAppNamespaceGridRecordCount();
        }


        /// <summary>
        /// ClickInActiveCheckBoxSearchByInactiveRecords
        /// </summary>
        /// <returns>string</returns>
        public string ClickInActiveCheckBoxSearchByInactiveRecords()
        {
            Random random = new Random();
            ClickAddNewAppNamespaceButton();
            var name = string.Format("ConfigItemTestCase{0}", random.Next(1000));
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "txtName", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "txtDescription", Type = ControlType.TextBox, Value = string.Format("AppNamespace{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(CreateControlDetails(name), _driver);
            IWebElement isActiveCheckBox = _driver.FindElement(By.Id(Constants.IsEnabledCheckBoxId));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", isActiveCheckBox);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(5000);
            IWebElement showInactiveItemControl = _driver.FindElement(By.Id("ShowInactiveItems"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", showInactiveItemControl);
            return name;
        }


        #endregion

        #region Methods for Add app namespace Screen

        /// <summary>
        /// LoadConfigAppNamespaceSearchScreen
        /// </summary>
        /// <returns>bool</returns>
        public bool ClickAddNewAppNamespaceButton()
        {
            _driver.FindElement(By.Id("create-item")).Click();
            return Helper.IsTextPresent(Constants.AddNewConfigAppNamespaceTitle, _driver);
        }

        /// <summary>
        /// ValidateAppNamespaceRequiredField
        /// </summary>
        /// <param name="controlName"></param>
        public void ValidateAppNamespaceRequiredField(string controlName)
        {
            var controlDetails = CreateControlDetails();
            if (ClickAddNewAppNamespaceButton())
            {
                controlDetails.ForEach(item =>
                {
                    if (string.Equals(item.Name, controlName, StringComparison.OrdinalIgnoreCase))
                    {
                        item.Value = string.Empty;
                    }

                });
                Helper.AssignValuesToControl(controlDetails, _driver);
                Thread.Sleep(1000);
                Helper.SaveButtonClickEvent("modalSubmit", _driver);
            }
        }

        /// <summary>
        /// CreateControlDetails
        /// </summary>
        /// <param name="configItemName"></param>
        /// <returns>ControlDetails list</returns>
        private List<ControlDetails> CreateControlDetails(string appNamespaceName = null)
        {
            Random random = new Random();
            var name = string.IsNullOrEmpty(appNamespaceName) ? string.Format("Config.Test.FunctionalTest{0}", random.Next(1000)) : appNamespaceName;
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "txtName", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "txtDescription", Type = ControlType.TextBox, Value = string.Format("AppNamespace{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails
            {
                Name = "txtPublicKey",
                Type = ControlType.TextBox,
                Value = @"MIIGEzCCBXygAwIBAgIKJ5Dt4gADAADZKjANBgkqhkiG9w0BAQUFADBHMRMwEQYK
CZImiZPyLGQBGRYDY29tMRowGAYKCZImiZPyLGQBGRYKZ3JhdGlzY2FyZDEUMBIG
A1UEAxMLdHBhcHdpY2EwMXYwHhcNMTQwOTIzMTcwNzAyWhcNMTgwOTAzMTQzODU4
WjCBojELMAkGA1UEBhMCVVMxEDAOBgNVBAgTB0Zsb3JpZGExFjAUBgNVBAcTDVN0
IFBldGVyc2J1cmcxIjAgBgNVBAoTGVNlcnZlIFZpcnR1YWwgRW50ZXJwcmlzZXMx
ETAPBgNVBAsTCFBsYXRmb3JtMTIwMAYDVQQDEylDb25maWdTZXJ2aWNlLVByZXNl
bnRhdGlvbi1UZXN0LnNlcnZlLmNvbTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCC
AQoCggEBAK8pbjz5cYWi6/cDY0yIC+p9uFqlyWLmvRxGAdk+QmFIVS0bWVHZZPya
32Eq1GEogSIJMhe4GCZzPr5PaV6YejHQIxAeuIa2/WKynM/XjGrbsqp7GrVghco2
vosfxe0k2jqkoqrVvMQoEmackAvO0mDrw8QTJaEW+b0JZ93X/eBc+2DwCZqYdFGu
36gJ3AaxOjdGggUcog1mcwYvfUM0JeBSzutNnwxQfjGzhK+9MlV28H74ECHPmnXW
vH9xNZCKTpHeEV/GrdurFqGPxlCVDJxJeE3Tu5bjyD3+kHENY5J/B864hIry7o7y
rtomjFzqqq81inAE1xjZsyExxV7HIc0CAwEAAaOCAyQwggMgMAsGA1UdDwQEAwIF
4DA+BgkrBgEEAYI3FQcEMTAvBicrBgEEAYI3FQiFndYahruraoflgQuDnYddhM30
FIE2g9aBeYOu4gACAWsCAQAwHQYDVR0OBBYEFB02FsG6G4d0PahAMLpwq0SWHJp2
MB8GA1UdIwQYMBaAFIAGxON49lPeW2p5M7xlufMdUboHMIIBGQYDVR0fBIIBEDCC
AQwwggEIoIIBBKCCAQCGgbxsZGFwOi8vL0NOPXRwYXB3aWNhMDF2KDMpLENOPVRQ
QVBXSUNBMDFWLENOPUNEUCxDTj1QdWJsaWMlMjBLZXklMjBTZXJ2aWNlcyxDTj1T
ZXJ2aWNlcyxDTj1Db25maWd1cmF0aW9uLERDPWdyYXRpc2NhcmQsREM9Y29tP2Nl
cnRpZmljYXRlUmV2b2NhdGlvbkxpc3Q/YmFzZT9vYmplY3RDbGFzcz1jUkxEaXN0
cmlidXRpb25Qb2ludIY/aHR0cDovL3RwYXB3aWNhMDF2LmdyYXRpc2NhcmQuY29t
L0NlcnRFbnJvbGwvdHBhcHdpY2EwMXYoMykuY3JsMIIBKgYIKwYBBQUHAQEEggEc
MIIBGDCBrQYIKwYBBQUHMAKGgaBsZGFwOi8vL0NOPXRwYXB3aWNhMDF2LENOPUFJ
QSxDTj1QdWJsaWMlMjBLZXklMjBTZXJ2aWNlcyxDTj1TZXJ2aWNlcyxDTj1Db25m
aWd1cmF0aW9uLERDPWdyYXRpc2NhcmQsREM9Y29tP2NBQ2VydGlmaWNhdGU/YmFz
ZT9vYmplY3RDbGFzcz1jZXJ0aWZpY2F0aW9uQXV0aG9yaXR5MGYGCCsGAQUFBzAC
hlpodHRwOi8vdHBhcHdpY2EwMXYuZ3JhdGlzY2FyZC5jb20vQ2VydEVucm9sbC9U
UEFQV0lDQTAxVi5ncmF0aXNjYXJkLmNvbV90cGFwd2ljYTAxdigzKS5jcnQwHQYD
VR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMBMCcGCSsGAQQBgjcVCgQaMBgwCgYI
KwYBBQUHAwIwCgYIKwYBBQUHAwEwDQYJKoZIhvcNAQEFBQADgYEAhqfPg9Q1nD4i
L1+faV+ghLVcqLtsJafSsEtAkPl5IesMPgLjecmuxN1tloKdiF5lmF1E2qto82+4
iEc7vTu4b8k89o8fGxB3odHCUwHDhlsnIPsS/N+6Lza2mxV10WwLer8QtCE3Dk5g
daIcfCU7BxhrMEGr5k/OAlHhXEiMjzo="
            });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            return controlDetails;
        }


        /// <summary>
        /// Create AppNamespace
        /// </summary>
        /// <param name="useSpecialChar"></param>
        /// <returns>string</returns>
        public string CreateAppNamespace(bool useSpecialChar = false)
        {
            var appNameSpaceDetails = CreateControlDetails();
            if (ClickAddNewAppNamespaceButton())
            {
                if (useSpecialChar)
                {
                    appNameSpaceDetails[0].Value = appNameSpaceDetails[0].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
                    appNameSpaceDetails[1].Value = appNameSpaceDetails[1].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
                }
                Helper.AssignValuesToControl(appNameSpaceDetails, _driver);
                Helper.SaveButtonClickEvent("modalSubmit", _driver);
            }
            Thread.Sleep(3000);
            Helper.SaveButtonClickEvent("modalClose", _driver);
            return appNameSpaceDetails[0].Value;
        }

        /// <summary>
        /// AddDuplicateAppNamespace
        /// </summary>
        public bool AddDuplicateAppNamespace()
        {
            var configAppNamespace = Helper.ValidateGridColumnDuplicateValue("myGrid", _driver, "Name");
            if (ClickAddNewAppNamespaceButton())
            {
                Helper.AssignValuesToControl(CreateControlDetails(configAppNamespace[0]), _driver);
                Helper.SaveButtonClickEvent("modalSubmit", _driver);
            }
            var expectedResult = string.Format("'{0}' App Namespace already exists, please edit.", configAppNamespace[0]);
            Thread.Sleep(1000);

            var result = _driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Equals(expectedResult);
            Helper.SaveButtonClickEvent("modalClose", _driver);
            return result;
        }

        #endregion

        #region Edit Config AppNamespace

        /// <summary>
        /// OpenEditConfigAppNamespacePopUpWindow
        /// </summary>
        /// <param name="gridRowNumber"></param>
        /// <returns>bool</returns>
        public bool OpenEditConfigAppNamespacePopUpWindow(int gridRowNumber = 0)
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigAppNamespaceGridXPath);
            if (count > 0)
            {
                var action = new Actions(_driver);
                var element = _driver.FindElement(By.Id(string.Format("myGridRow{0}",gridRowNumber)));
                //Double click
                action.DoubleClick(element).Perform();
            }
            return Helper.IsTextPresent(Constants.AddNewConfigAppNamespaceTitle, _driver);
        }

        /// <summary>
        /// GetNumberOfRowsInATable
        /// </summary>
        /// <param name="tableXPath"></param>
        /// <returns>int</returns>
        public int GetNumberOfRowsInATable(string tableXPath)
        {
            Thread.Sleep(1500);
            var table = _driver.FindElements(By.XPath(tableXPath));
            return table
                   .Select(webElement => webElement.FindElements(By.XPath("//tr")))
                       .Select(rows => rows.Count)
                       .FirstOrDefault();
        }


        /// <summary>
        /// ValidateRequiredField
        /// </summary>
        public void ValidateRequiredField(string controlName = null)
        {
            OpenEditConfigAppNamespacePopUpWindow();
            var controlDetails = CreateControlDetails();
            controlDetails.ForEach(item =>
            {
                if (string.Equals(item.Name, controlName, StringComparison.OrdinalIgnoreCase))
                {
                    item.Value = string.Empty;
                }

            });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Thread.Sleep(1000);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
        }

        /// <summary>
        /// EditAppNamespace
        /// </summary>
        /// <param name="useSpecialChar"></param>
        /// <returns>string</returns>
        public string EditAppNamespace(bool useSpecialChar = false)
        {
            OpenEditConfigAppNamespacePopUpWindow();
            var appNameSpaceDetails = new List<ControlDetails>();
            appNameSpaceDetails.Add(new ControlDetails { Name = "txtName", Type = ControlType.TextBox, Value = "AppNamespace408" });
            appNameSpaceDetails.Add(new ControlDetails { Name = "txtDescription", Type = ControlType.TextBox, Value = string.Format("Testing AppNamespace") });
            
            if (useSpecialChar)
            {
                appNameSpaceDetails[0].Value = appNameSpaceDetails[0].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
                appNameSpaceDetails[1].Value = appNameSpaceDetails[1].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
            }
            Helper.AssignValuesToControl(appNameSpaceDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return appNameSpaceDetails[0].Value;
        }

        /// <summary>
        /// EditDuplicateAppNamespace
        /// </summary>
        /// <returns>bool</returns>
        public bool EditDuplicateAppNamespace()
        {
            OpenEditConfigAppNamespacePopUpWindow(1);
            var configAppNamespace = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            _driver.FindElement(By.Id("txtName")).Clear();
            _driver.FindElement(By.Id("txtName")).SendKeys(configAppNamespace[0]);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            var expectedResult = string.Format("'{0}' App Namespace already exists, please edit.", configAppNamespace[0]);
            Thread.Sleep(1000);
            return _driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Equals(expectedResult);
        }


        #endregion

        #region Delete Config AppNamespace

        /// <summary>
        /// OpenDeleteConfigAppNamespacePopUpWindow
        /// </summary>
        /// <returns>bool</returns>
        public bool OpenDeleteConfigAppNamespacePopUpWindow()
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigItemGridXPath);
            if (count > 0)
            {
                var action = new Actions(_driver);
                var element = _driver.FindElement(By.Id("myGridRow0")).FindElement(By.ClassName(Constants.DeleteButtonClass));
                action.Click(element).Perform();
            }
            return Helper.IsTextPresent(Constants.DeleteTitle, _driver);
        }

        /// <summary>
        /// DeleteConfigAppNamespace
        /// </summary>
        public void DeleteConfigAppNamespace()
        {
            if (OpenDeleteConfigAppNamespacePopUpWindow())
            {
                var action = new Actions(_driver);
                var element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[0];
                action.Click(element).Perform();

            }
        }

        /// <summary>
        /// DeleteCancelConfigAppNamespace
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteCancelConfigAppNamespace()
        {
            if (OpenDeleteConfigAppNamespacePopUpWindow())
            {
                var action = new Actions(_driver);
                var element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[1];
                action.Click(element).Perform();
                return true;
            }
            return false;
        }

        /// <summary>
        /// RefreshAppNamespace
        /// </summary>
        /// <returns>bool</returns>
        public bool RefreshAppNamespace()
        {
            var element = _driver.FindElement(By.Id("myGridRow0")).FindElement(By.ClassName("supergrid-grid-column-rowSelectorCheckBox"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", element);
            _driver.FindElement(By.Id("refresh-appnamespace")).Click();
             return Helper.IsTextPresent("Cache has been refreshed successfully", _driver); 
        }

        #endregion
    }
}
