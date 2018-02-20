using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Serve.Internal.BusinessServices.Shared.Tests.DataContracts;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;

namespace Serve.Platform.Configuration.UI.SeleniumTest.FunctionalTests
{
    /// <summary>
    /// App user end to end testing
    /// </summary>
    [TestClass]
    public class UserMangementUITest : SeleniumTestBase
    {
        #region ==Variable Declaration==
        
        private readonly Random random;
        private static double waitTime = 2000;

        #endregion

        #region ==Setup and Cleanup methods==

        /// <summary>
        /// Setup
        /// </summary>    
        [TestInitialize]
        public void ClassInit()        
        {                                    
            Helper.GoToPage("Manage/App Users", Driver.Instance, "ads%5Ccpt_testacc_1:CupAta6a@");
        }

        /// <summary>
        /// Constructor: Initialize the object
        /// </summary>
        public UserMangementUITest()
        {
            random = new Random();
        }

        #endregion

        /// <summary>
        /// Navigate to app user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Navigate to app user")]
        public void AppUser_Page()
        {
            //Arrange and Act
            string acutalResult = Driver.Instance.Title;

            //Assert
            Assert.AreEqual(acutalResult, "Configuration Management Console - App Users");
        }

        /// <summary>
        /// Navigate to app user and show ShowInActiveRecards.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Navigate to  app user and show ShowInActiveRecards")]
        [Ignore]
        public void AppUser_Show_InActive_Records()
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
        public void AppUser_When_Search_Text_NonEmpty()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSearch", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSearch", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 1);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        /// <summary>
        /// Add New user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New user")]
        [Ignore]
        public void AppUser_Add_NewUser()
        {
            //Arrange and Act
            var userName = "A" + Convert.ToString(random.Next(6854585));
            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "UserName", Value = userName, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            var acutalResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("'" + userName + "' has been added to Management Console");

            //Assert
            Assert.IsTrue(acutalResult);
        }

        /// <summary>
        /// Add New user SpecialChar.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New user SpecialChar")]
        [Ignore]
        public void AppUser_Add_NewUser_SpecialChar_Test()
        {
            //Arrange and Act
            var userName = "A" + Convert.ToString(random.Next(6854585)) + "~!@#$%^&*()_+`-=:[];':,./<>?";
            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "UserName", Value = userName, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            var acutalResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("has been added to Management Console");

            //Assert
            Assert.IsTrue(acutalResult);
        }

        /// <summary>
        /// Add New user.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Add New user")]
        public void AppUser_Add_NewUser_When_UserName_Empty()
        {
            //Arrange and Act
            Helper.LinkButtonClick("Add New User", Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "UserName", Value = string.Empty, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            string acutalResult = Driver.Instance.FindElements(By.Id("addUser-form"))[0].FindElements(By.TagName("span"))[0].Text;

            //Assert
            Assert.AreEqual(acutalResult, "This is a required field.");
        }

        /// <summary>
        /// Edit User.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Edit User")]
        [Ignore]
        public void AppUser_Edit_User()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 1);
            Helper.GridDoubleClick("myGrid", 1, Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "UserName", Value = firstCellValue + 1, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            string acutalResult = Helper.GetgridCellVaule("myGridData", 1, 1);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue + 1);
        }

        /// <summary>
        /// Edit User.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Edit User")]
        [Ignore]
        public void AppUser_Edit_User_When_UserName_Empty()
        {
            //Arrange and Act
            Helper.GridDoubleClick("myGrid", 1, Driver.Instance);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "UserName", Value = string.Empty, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.ButtonClickEvent("modalSubmit", Driver.Instance);
            string acutalResult = Driver.Instance.FindElements(By.Id("addUser-form"))[0].FindElements(By.TagName("span"))[0].Text;

            //Assert
            Assert.AreEqual(acutalResult, "This is a required field.");
        }

        /// <summary>
        /// Grid Next button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Next button click")]
        [Ignore]
        public void AppUser_Grid_Next_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var currentPage = Helper.GetGridCurrentPage(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(acutalResult, currentPage + 1);
            }
        }

        /// <summary>
        /// Grid Last button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Last button click")]
        [Ignore]
        public void AppUser_Grid_Last_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(acutalResult, pageCount);
            }
        }

        /// <summary>
        /// Grid Previous button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Previous button click")]
        [Ignore]
        public void AppUser_Grid_Previous_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.Previous, Driver.Instance);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(acutalResult, pageCount - 1);
            }
        }

        /// <summary>
        /// Grid First button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid First button click")]
        [Ignore]
        public void AppUser_Grid_First_Button_Click()
        {
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
                Helper.GridNavigationEvent(NavigateType.First, Driver.Instance);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(acutalResult, 1);
            }
        }

        /// <summary>
        /// Check Whether Go To is Working.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("AppUser - GO")]
        [Ignore]
        public void AppUser_GoTo()
        {
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst)))
            {
                //Arrange and Act
                var page = Helper.GetGridTotalPageCount(Driver.Instance) - 1;
                Helper.GoToNavigation(page, Driver.Instance);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

                //Assert
                Assert.AreEqual(acutalResult, page);
            }
            
        }

        /// <summary>
        /// Name: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Name: Ascending order")]
        [Ignore]
        public void AppUser_Name_Sort_Ascending()
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
        public void AppUser_Name_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// SecretKey: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("SecretKey: Ascending order")]
        [Ignore]
        public void AppUser_SecretKey_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "SecretKey", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// SecretKey: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("SecretKey: Descending order")]
        [Ignore]
        public void AppUser_SecretKey_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "SecretKey", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// CreateDate: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("CreateDate: Ascending order")]
        public void AppUser_CreateDate_Sort_Ascending()
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
        public void AppUser_CreateDate_Sort_Descending()
        {
            //Arrange and Act
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
        public void AppUser_CreatedBy_Sort_Ascending()
        {
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
        public void AppUser_CreatedBy_Sort_Descending()
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
        public void AppUser_UpdateDate_Sort_Ascending()
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
        public void AppUser_UpdateDate_Sort_Descending()
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
        public void AppUser_UpdatedBy_Sort_Ascending()
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
        public void AppUser_UpdatedBy_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated By", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

    }
}
