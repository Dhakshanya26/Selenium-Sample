using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using Serve.Internal.BusinessServices.Shared.Tests.DataContracts;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.Pages;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;
using System.Threading;

namespace Serve.Platform.Configuration.UI.SeleniumTest.FunctionalTests
{
    [Ignore]
    [TestClass]
    public class ConsoleUserUITest : SeleniumTestBase
    {
        #region ==Variable Declaration==

        private readonly Random random;
        private static ConfigAppNamespacePage _configAppNamespacePage;
        #endregion

        #region ==Setup and Cleanup methods==

        /// <summary>
        /// Setup
        /// </summary>    
        [TestInitialize]
        public void ClassInit()
        {
            Helper.GoToPage("Manage/Users", Driver.Instance, "ads%5Ccpt_testacc_1:CupAta6a@");
        }

        /// <summary>
        /// Constructor: Initialize the object
        /// </summary>
        public ConsoleUserUITest()
        {
            random = new Random();
        }

        #endregion

        #region ==Page Load==

        /// <summary>
        /// Navigate to app user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Navigate to app user")]
        [Ignore]
        public void ConsoleUser_Page()
        {
            //Arrange and Act
            string actualResult = Driver.Instance.Title;

            //Assert
            Assert.AreEqual(actualResult, "Configuration Management Console - Users");
        }

        #endregion

        #region ==Search and Show Inactive Records==

        /// <summary>
        /// Navigate to app user and show ShowInActiveRecards.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Navigate to  app user and show ShowInActiveRecards")]
        [Ignore]
        public void ConsoleUser_Show_InActive_Records()
        {
            //Arrange and Act
            int gridColumnCount = Helper.BuildTable(Constants.GridId, Driver.Instance).Count;
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "ShowInactiveItems", Value = "True", Type = ControlType.CheckBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSearch", Driver.Instance);
            int newGridColumnCount = Helper.BuildTable(Constants.GridId, Driver.Instance).Count;
            //Deactivate
            //Assert
            Assert.IsTrue(gridColumnCount != newGridColumnCount);
        }

        /// <summary>
        /// Navigate to app user and show given search app user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Navigate to app user and show given search app user.")]
        [Ignore]
        public void ConsoleUser_When_Search_Text_NonEmpty()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSearch", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSearch", Driver.Instance);
            var actualResult = Helper.GetgridCellVaule("myGridData", 1, 1);

            //Assert
            Assert.AreEqual(actualResult, firstCellValue);
        }

        #endregion

        #region  Test methods for Add ConsoleUser
    
        /// <summary>
        /// Add New user SpecialChar.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New ConsoleUser SpecialChar")]
        [Ignore]
        public void AppUser_Add_NewUser_Test()
        {
            //Arrange and Act
            _configAppNamespacePage = new ConfigAppNamespacePage(Driver.Instance);
            _configAppNamespacePage.CreateAppNamespace(true);
            Helper.GoToPage("Manage/Users", Driver.Instance, "ads%5Ccpt_testacc_1:CupAta6a@");
            var userName = "A" + Convert.ToString(random.Next(6854585));
            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtUserName", Value = userName, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("togglevalue", Driver.Instance);
            Helper.GridRowSelect("PopupGridData", 1, 1);
            Helper.GoToNavigation(2, Driver.Instance, "PopupGrid");
            Helper.GridRowSelect("PopupGridData", 1, 1);
            Helper.ButtonClickEvent("togglevalue1", Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            Thread.Sleep(2000);
            string[] splitValue = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Split('\n');
            //Assert
            Assert.AreEqual(splitValue[1], "'" + userName + "' has been added to Management Console");
        }

        /// <summary>
        /// Add New user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New ConsoleUser")]
        [Ignore]
        public void AppUser_Add_NewUser_When_UserName_Empty()
        {
            //Arrange and Act
            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtUserName", Value = string.Empty, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            var actualResult = Driver.Instance.FindElements(By.ClassName("field-validation-error"))[0].Text;

            //Assert
            Assert.AreEqual(actualResult, "This is a required field.");
        }

        /// <summary>
        /// Add New user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New ConsoleUser")]
        [Ignore]
        public void AppUser_Add_NewUser_When_AppNamespace_Empty()
        {
            //Arrange and Act
            var userName = "A" + Convert.ToString(random.Next(6854585));

            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtUserName", Value = userName, Type = ControlType.TextBox });
            controlDetails.Add(new ControlDetails() { Name = "AppNamespaces", Value = string.Empty, Type = ControlType.DropDownList });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            var actualResult = Driver.Instance.FindElements(By.ClassName("field-validation-error"))[0].Text;

            //Assert
            Assert.AreEqual(actualResult, "This is a required field.");
        }
        #endregion

        #region  Test methods for Edit ConsoleUser
        /// <summary>
        /// Edit User.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Edit ConsoleUser")]
        [Ignore]
        public void AppUser_Edit_ConsoleUser()
        {
            //Arrange and Act
            _configAppNamespacePage = new ConfigAppNamespacePage(Driver.Instance);
            _configAppNamespacePage.CreateAppNamespace(true);
            Helper.GoToPage("Manage/Users", Driver.Instance, "ads%5Ccpt_testacc_1:CupAta6a@");
            var userName = Helper.GetgridCellVaule("myGridData", 1, 1);
            Helper.GridDoubleClick("myGrid", 1, Driver.Instance);
            Thread.Sleep(1000);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtUserName", Value = userName, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("togglevalue", Driver.Instance);
            Helper.GridRowSelect("PopupGridData", 1, 1);
            //Helper.GridRowSelect("PopupGridData", 3, 1);
            Helper.GoToNavigation(3, Driver.Instance, "PopupGrid");
            Helper.GridRowSelect("PopupGridData", 1, 1);
            Helper.ButtonClickEvent("togglevalue1", Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            Thread.Sleep(1000);
            var actualResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("updated successfully");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Edit User.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Edit ConsoleUser")]
        [Ignore]
        public void AppUser_Edit_User_When_UserName_Empty()
        {
            //Arrange and Act
            Helper.GridDoubleClick("myGrid", 1, Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtUserName", Value = string.Empty, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            var actualResult = Driver.Instance.FindElements(By.ClassName("field-validation-error"))[0].Text;

            //Assert
            Assert.AreEqual(actualResult, "This is a required field.");
        }

        #endregion

        #region ==Paging==

        /// <summary>
        /// Grid Next button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Next button click")]
        [Ignore]
        public void ConsoleUser_Grid_Next_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var currentPage = Helper.GetGridCurrentPage(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
                var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(actualResult, currentPage + 1);
            }
        }

        /// <summary>
        /// Grid Last button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Last button click")]
        [Ignore]
        public void ConsoleUser_Grid_Last_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
                var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(actualResult, pageCount);
            }
        }

        /// <summary>
        /// Grid Previous button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Previous button click")]
        [Ignore]
        public void ConsoleUser_Grid_Previous_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Previous, Driver.Instance);
                var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(actualResult, pageCount - 1);
            }
        }

        /// <summary>
        /// Grid First button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid First button click")]
        [Ignore]
        public void ConsoleUser_Grid_First_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.First, Driver.Instance);
                var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(actualResult, 1);
            }
        }

        /// <summary>
        /// Check Whether Go To is Working.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("AppUser - GO")]
        [Ignore]
        public void ConsoleUser_GoTo()
        {
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var page = Helper.GetGridTotalPageCount(Driver.Instance) - 1;
                Helper.GoToNavigation(page, Driver.Instance);
                var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(actualResult, page);
            }

        }

        #endregion

        #region ==Sorting==

        /// <summary>
        /// Name: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Name: Ascending order")]
        [Ignore]
        public void ConsoleUser_Name_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Name: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Name: Descending order")]
        [Ignore]
        public void ConsoleUser_Name_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// CreateDate: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("CreateDate: Ascending order")]
        [Ignore]
        public void ConsoleUser_CreateDate_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "CreateDate", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// CreateDate: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("CreateDate: Descending order")]
        [Ignore]
        public void ConsoleUser_CreateDate_Sort_Descending()
        {
            //Arrange and Act
            Thread.Sleep(1000);
            var actualResult = Helper.SortByColumn(Driver.Instance, "CreateDate", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// CreatedBy: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("CreatedBy: Ascending order")]
        [Ignore]
        public void ConsoleUser_CreatedBy_Sort_Ascending()
        {
            Thread.Sleep(1000);
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "CreatedBy", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// CreatedBy: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("CreatedBy: Descending order")]
        [Ignore]
        public void ConsoleUser_CreatedBy_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "CreatedBy", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// UpdateDate: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("UpdateDate: Ascending order")]
        [Ignore]
        public void ConsoleUser_UpdateDate_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "UpdateDate", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// UpdateDate: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("UpdateDate: Descending order")]
        [Ignore]
        public void ConsoleUser_UpdateDate_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "UpdateDate", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Updated By: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Updated By: Ascending order")]
        [Ignore]
        public void ConsoleUser_UpdatedBy_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated By", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Updated By: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Updated By: Descending order")]
        [Ignore]
        public void ConsoleUser_UpdatedBy_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated By", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// ConsoleUser_Deactivation_ConfirmationPopUp_Ok
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Deactivation_ConfirmationPopUp_Ok")]
        [Ignore]
        public void ConsoleUser_Deactivation_ConfirmationPopUp_Ok()
        {
            var actualResult = Driver.Instance.FindElements(By.ClassName("supergrid-grid-column-cell"))[5];
            actualResult.Click();
            Thread.Sleep(1000);
            var btnOk = Driver.Instance.FindElement(By.XPath("/html/body/div[3]/div[3]/div/button[1]"));
            btnOk.Click();
            Thread.Sleep(2000);
            string[] notifyMessage = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Split('\n');
            Assert.AreEqual(notifyMessage[1], "User deactivated successfully");
        }

        /// <summary>
        /// ConsoleUser_Deactivation_ConfirmationPopUp_Cancel
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Deactivation_ConfirmationPopUp_Cancel")]
        [Ignore]
        public void ConsoleUser_Deactivation_ConfirmationPopUp_Cancel()
        {
            var actualResult = Driver.Instance.FindElements(By.ClassName("supergrid-grid-column-cell"))[5];
            actualResult.Click();
            Thread.Sleep(1000);
            var btnCancel = Driver.Instance.FindElement(By.XPath("/html/body/div[3]/div[3]/div/button[2]"));
            btnCancel.Click();
            Assert.IsFalse(Helper.IsElementPresent(By.ClassName("dialog-confirm-delete")));
        }

        /// <summary>
        /// ConsoleUser_Pagination_Next
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Pagination_Next")]
        [Ignore]
        public void ConsoleUser_Pagination_Next()
        {
            Thread.Sleep(500);
            IWebElement nextButton = Driver.Instance.FindElement(By.Id("superGridNextPagemyGrid"));
            nextButton.Click();
            Thread.Sleep(2000);
            IWebElement goToText = Driver.Instance.FindElement(By.Id("superGridCurrentPagemyGrid"));
            Assert.AreEqual(goToText.Text, "2");
        }


        /// <summary>
        /// ConsoleUser_Pagination_Previous
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Pagination_Previous")]
        [Ignore]
        public void ConsoleUser_Pagination_Previous()
        {
            Thread.Sleep(500);
            IWebElement gotoText = Driver.Instance.FindElement(By.Id("supergridgototextmyGrid"));
            gotoText.Clear();
            gotoText.SendKeys("2");
            IWebElement gotoButton = Driver.Instance.FindElement(By.Id("goToPageBtnmyGrid"));
            gotoButton.Click();
            Thread.Sleep(2000);
            IWebElement previousButton = Driver.Instance.FindElement(By.Id("superGridPrevPagemyGrid"));
            previousButton.Click();
            Thread.Sleep(2000);
            IWebElement currentPage = Driver.Instance.FindElement(By.Id("superGridCurrentPagemyGrid"));
            Assert.AreEqual(currentPage.Text, "1");
        }

        /// <summary>
        /// ConsoleUser_Pagination_First
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Pagination_First")]
        [Ignore]
        public void ConsoleUser_Pagination_First()
        {
            Thread.Sleep(500);
            IWebElement gotoText = Driver.Instance.FindElement(By.Id("supergridgototextmyGrid"));
            gotoText.Clear();
            gotoText.SendKeys("2");
            IWebElement gotoButton = Driver.Instance.FindElement(By.Id("goToPageBtnmyGrid"));
            gotoButton.Click();
            Thread.Sleep(2000);
            IWebElement firstButton = Driver.Instance.FindElement(By.Id("superGridFirstPagemyGrid"));
            firstButton.Click();
            Thread.Sleep(2000);
            IWebElement currentPage = Driver.Instance.FindElement(By.Id("superGridCurrentPagemyGrid"));
            Assert.AreEqual(currentPage.Text, "1");
        }

        /// <summary>
        /// ConsoleUser_Pagination_Last
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Pagination_Last")]
        [Ignore]
        public void ConsoleUser_Pagination_Last()
        {
            Thread.Sleep(500);
            IWebElement lastButton = Driver.Instance.FindElement(By.Id("superGridLastPagemyGrid"));
            lastButton.Click();
            Thread.Sleep(2000);
            IWebElement currentPage = Driver.Instance.FindElement(By.Id("superGridCurrentPagemyGrid"));
            IWebElement totalNumberOfPages = Driver.Instance.FindElement(By.Id("superGridPageTotalCountmyGrid"));
            Assert.AreEqual(currentPage.Text, totalNumberOfPages.Text);
        }


        /// <summary>
        /// ConsoleUser_Pagination_Go_To
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConsoleUser_Pagination_Go_To")]
        [Ignore]
        public void ConsoleUser_Pagination_Go_To()
        {
            Thread.Sleep(500);
            IWebElement gotoText = Driver.Instance.FindElement(By.Id("supergridgototextmyGrid"));
            gotoText.Clear();
            gotoText.SendKeys("2");
            IWebElement gotoButton = Driver.Instance.FindElement(By.Id("goToPageBtnmyGrid"));
            gotoButton.Click();
            IWebElement currentPage = Driver.Instance.FindElement(By.Id("superGridCurrentPagemyGrid"));
            Assert.AreEqual(currentPage.Text, "2");
        }


        /// <summary>
        /// AppNamespace : Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("AppNamespace: Ascending order")]        
        public void ConsoleUser_AppNamespace_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "AppNamespace", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// AppNamespace : Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Updated By: Descending order")]        
        public void ConsoleUser_AppNamespace_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "AppNamespace", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }
        #endregion

        #region ==Grid Filter==

        /// <summary>
        /// Filter Appnamespace column value.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter Appnamespace value")]
        public void ConsoleUser_AppNamespace_Filter()
        {
            //Arrange and Act
            var appNamespaceValue = Helper.GetgridCellVaule("myGridData", 1, 2);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrmyGridAppnamespaceName", Value = appNamespaceValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrmyGridAppnamespaceName", Driver.Instance);
            var actualResult = Helper.GetgridCellVaule("myGridData", 1, 2);

            //Assert
            Assert.AreEqual(actualResult, appNamespaceValue);
        }

        #endregion

        #region AppNamespace Added to Grid
        /// <summary>
        /// Has AppNamespace in Grid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Show AppNamespace Name in Grid")]        
        public void ConsoleUser_Show_AppNamespace_Column()
        {
            var appNamespaceHeader = Driver.Instance.FindElement(By.XPath("//*[@id='myGridData']/tbody[1]/tr[2]/th[2]/a"));
            Assert.AreEqual("AppNamespace", appNamespaceHeader.Text);
            var actualResult = Helper.GetgridCellVaule("myGridData", 1, 2);
            Assert.IsNotNull(actualResult);
        }
        #endregion
    }
}
