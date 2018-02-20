using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Serve.Internal.BusinessServices.Shared.Tests.DataContracts;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.Pages;

using System.Threading;
using System.Windows.Forms;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using Serve.Shared.AcceptanceLogging.Helpers;

namespace Serve.Platform.Configuration.UI.SeleniumTest.FunctionalTests
{
    /// <summary>
    /// ConfigurationItemUITest
    /// </summary>
    [TestClass]
    [Ignore]
    public class ConfigurationItemUITest : SeleniumTestBase
    {
        public TestContext TestContext { get; set; }
        private static ConfigItemPage _configItemPage;

        #region Setup and Cleanup methods

        /// <summary>
        /// Cleanup 
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _configItemPage = new ConfigItemPage(Driver.Instance);
        }

        /// <summary>
        /// ConfigItemSeleniumTest
        /// </summary>
        public ConfigurationItemUITest()
        {

        }

        #endregion

        #region  Test methods for config item search screen

        /// <summary>
        /// Check whether the Config item search screen is loaded
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the Config item search screen is loaded")]
        public void ConfigurationItem_WhenConFigItemSubMenuClicked_LoadSearchScreen()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.LoadConfigItemSearchScreen();


                //Assert
                Assert.IsTrue(actualResult, "Page Title is incorrect");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Check whether the Configuration item search screen loaded with app namespace 
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the Configuration item search screen loaded with app namespace ")]
        public void ConfigurationItem_WhenConfigItemSubMenuClicked_AppNamespaceDropdownPrePopulated()
        {
            //Act
            var actualResult = _configItemPage.CheckAppNamespaceDropDownValues();

            //Assert
            Assert.IsTrue(actualResult > 0);
        }

        /// <summary>
        /// Check whether the Configuration item search screen loaded with config item grid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item search screen loaded with config item grid")]
        public void ConfigurationItem_WhenConFigItemSubMenuClicked_LoadConfigItemGrid()
        {
            try
            {
                //Act

                var configItemGridRows = _configItemPage.CheckRecordsInConfigItemGrids();


                //Assert
                Assert.IsTrue(configItemGridRows > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Check whether the Configuration item is loaded for the selected App Namespace
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the Configuration item is loaded for the selected App Namespace")]
        public void ConfigurationItem_WhenSpecificAppNamespaceSelected_LoadConfigItemGrid()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.GetConfigItemForSelectedAppNamespace();


                //Assert
                Assert.IsTrue(actualResult, "Did not return correct response.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Check whether No records found message displayed when searched with invalid name.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether No records found message displayed when searched with invalid name.")]
        public void ConfigurationItem_WhenSearchedByInvalidName_ReturnNoRecordsFoundMessage()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.SearchConfigItemByInvalidName();


                //Assert
                Assert.IsTrue(actualResult, "Did not return correct response.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Check whether Configuration item for the given name is loaded 
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item for the given name is loaded")]
        public void ConfigurationItem_WhenSearchedByName_LoadConfigItemOfGivenName()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.SearchConfigurationItemByName();


                //Assert
                Assert.IsTrue(actualResult, "Did not return correct response.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }


        /// <summary>
        /// Check whether all Configuration item are loaded when clear image button in the search by name text box is clicked.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether all Configuration item are loaded when clear image button in the search by name text box is clicked.")]
        public void ConfigurationItem_WhenSearchedByNameClearIconClicked_LoadAllConfigItem()
        {
            try
            {
                //Act

                _configItemPage.ClickTextInSearchByNameTextBox();

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        #region NewWorking

        /// <summary>
        /// Test method to check the ConfigurationItem_WhenSearchByInActiveCheckBox_Checked_InActiveRecordsPresent_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether all Configuration item inactive records present in grid when inactive check box is clicked.")]
        public void ConfigurationItem_WhenSearchByInActiveCheckBox_Unchecked_InActiveRecordsPresent_InGrid()
        {
            //Act
            var name = _configItemPage.ClickInActiveCheckBoxSearchByInactiveRecords();
            Thread.Sleep(5000);
            var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");

            //Assert
            Assert.AreEqual(actualResult[0], name);
        }

        /// <summary>
        /// ConfigurationItem_WhenSearchByInActiveCheckBox_UnChecked_ActiveRecordsPresent_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether all Configuration item active records present in grid when inactive check box is Unchecked.")]
        public void ConfigurationItem_WhenSearchByInActiveCheckBox_Checked_ActiveRecordsPresent_InGrid()
        {
            try
            {
                //Act
                var name = _configItemPage.AddNewConfigItem();
                var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");

                //Assert
                Assert.AreEqual(actualResult[0], name);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        #endregion

        #endregion

        #region  Test methods for add config item

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenAddNewItemLinkClicked_DisplayAddScreen
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Add new config item button clicked properly")]
        public void ConfigurationItem_WhenAddNewItemLinkClicked_DisplayAddScreen()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.ClickAddNewConfigItemButton();


                //Assert
                Assert.IsTrue(actualResult, "Page Title is incorrect");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_AddConfigItemWithValidFields
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item Data with Valid Fields")]
        public void ConfigurationItem_AddConfigItemWithValidFields()
        {
            try
            {
                //Act
                var configItemName = _configItemPage.AddNewConfigItem();
                var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");


                //Assert
                Assert.AreEqual(actualResult[0], configItemName);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }


        /// <summary>
        /// Test Method to check ConfigurationItem_AddConfigItemWithValidFields WithSpecialChar
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether Configuration item Data with Valid Fields with special char")]
        public void ConfigurationItem_AddConfigItemWithValidFieldsWithSpecialChar()
        {
            //Act
            var configItemName = _configItemPage.AddNewConfigItemWithSpecialChar();
            var acutalResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("Saved Successfully");

            //Assert
            Assert.IsTrue(acutalResult);
        }

        /// <summary>
        /// Test Method to check whether the user able to add Configuration item with duplicate name
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to add Configuration item with duplicate name")]
        public void ConfigurationItem_WhenAddingDuplicateConfigItem_ReturnErrorMessage()
        {
            try
            {
                //Act
                var actualResult = _configItemPage.AddDuplicateConfigurationItem();
                //Assert
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check whether the user able to add Configuration item with dangerous request
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user able to add Configuration item with dangerous request")]
        public void ConfigurationItem_WhenAddingConfigItem_Dangerous_Request_ReturnErrorMessage()
        {
            //Act
            var actualResult = _configItemPage.AddConfigurationItemDangerousRequest();

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Check Configuration item name validation
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check Configuration item name validation")]
        public void ConfigurationItem_WhenConfigItemWithoutName_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredField("Name");
                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenConfigItemWithoutValue_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item Value validation")]
        public void ConfigurationItem_WhenConfigItemWithoutValue_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredField("Value");
                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenConfigItemWithoutAppNamespace_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item AppNamespace validation")]
        public void ConfigurationItem_WhenConfigItemWithoutAppNamespace_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredField("AppNamespaceId");
                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


        }

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenConfigItemWithoutInternalType_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item InternalType validation")]
        public void ConfigurationItem_WhenConfigItemWithoutInternalType_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredField("Type");
                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenAddingConfigItemWithAvailableFrom_GreaterThan_ExpiresOn_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item AvailableFrom greater than expiresOn Validation")]
        public void ConfigurationItem_WhenAddingConfigItemWithAvailableFrom_GreaterThan_ExpiresOn_ReturnErrorMessage()
        {
            try
            {
                //Act
                var actualResult = _configItemPage.AddConfigItemWithAvailableDateGreaterThanExpiryDate();


                //Assert
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check whether the user able to add Configuration item with Invalid InternalType
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to add Configuration item with Invalid InternalType")]
        public void ConfigurationItem_WhenAddingConfigItemWithInvalidInternalType_ReturnErrorMessage()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.AddConfigurationItemWithInValidInternalType();


                //Assert
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to add Configuration item with IsSecure Checked")]
        public void ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp()
        {
            try
            {
                //Act
                Thread.Sleep(3000);
                var actualResult = _configItemPage.AddConfigurationItemWithIsSecurePopUpDisplay();


                //Assert
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp_Click_OkButton
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to add Configuration item with IsSecure Checked with ok button")]
        public void ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp_Click_OkButton()
        {
            try
            {
                //Act
                Thread.Sleep(3000);
                var configItemName = _configItemPage.AddConfigurationItemWithIsSecurePopUpDisplay_Click_OkButton();
                Thread.Sleep(5000);
                var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");


                //Assert
                Assert.AreEqual(actualResult[0], configItemName);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp_Click_CancelButton
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to add Configuration item with IsSecure Checked with cancel button")]
        public void ConfigurationItem_WhenAddingConfigItemWithIsSecure_DisplayPopUp_Click_CancelButton()
        {
            try
            {
                //Act
                var actualResult = _configItemPage.AddConfigurationItemWithIsSecurePopUpDisplay_Click_CancelButton();
                
                //Assert
                Assert.IsTrue(actualResult);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Check whether ConfigItem_WhenAddingConfigItem_VersionNumber
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem_WhenAddingConfigItem_VersionNumber")]
        public void ConfigItem_WhenAddingConfigItem_VersionNumber()
        {
            try
            {
                //Act
                var name = _configItemPage.CheckVersionNumberAddConfigItem();

                var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Version #");
                Thread.Sleep(8000);


                //Assert
                Assert.AreEqual(actualResult[0], "1");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #endregion

        #region Test methods for edit config item

        /// <summary>
        /// Test Method to check ConfigurationConfigItem_WhenDoubleClickingRecord_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether ConfigurationConfigItem_WhenDoubleClickingRecord_InGrid ")]
        public void AppNamespace_WhenDoubleClickingRecord_InGrid()
        {
            try
            {
                //Act

                var actualResult = _configItemPage.OpenEditConfigItemPopUpWindow();
                Thread.Sleep(3000);


                //Assert
                Assert.IsTrue(actualResult, "Page Title is incorrect");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Check Configuration item name validation
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check Configuration item name validation")]
        public void ConfigItem_WhenEditConfigItemWithoutName_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredFieldForEditing("Name");

                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_WhenConfigItemWithoutValue_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item Value validation")]
        public void ConfigItem_WhenEditConfigItemWithoutValue_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredFieldForEditing("Value");

                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// ConfigItem_WhenEditConfigItemWithoutConfigItem_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item AppNamespace validation")]
        public void ConfigItem_WhenEditConfigItemWithoutConfigItem_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredFieldForEditing("AppNamespaceId", "Select App Namespace");

                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// ConfigItem_WhenEditConfigItemWithoutType_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item Type validation")]
        public void ConfigItem_WhenEditConfigItemWithoutType_ReturnErrorMessage()
        {
            try
            {
                //Arrange

                var expectedResult = "This is a required field.";

                //Act
                _configItemPage.ValidateRequiredFieldForEditing("Type", "Select Type");

                var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        //<summary>
        //Test Method to check ConfigItem_WhenEditingConfigItemeWithValidFields
        //</summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether Configuration item with valid records while editing")]
        public void ConfigItem_WhenEditingConfigItemeWithValidFields()
        {
            try
            {
                //Act

                var name = _configItemPage.EditConfigItem();
                Thread.Sleep(3000);
                var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");
                Thread.Sleep(8000);


                //Assert
                Assert.AreEqual(actualResult[0], name);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Test Method to check ConfigurationItem_EditConfigItemWithValidFields WithSpecialChar
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether Configuration item Data with Valid Fields with special char")]
        public void ConfigurationItem_EditConfigItemWithValidFieldsWithSpecialChar()
        {
            //Act
            var configItemName = _configItemPage.EditConfigItem(true);
            var acutalResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("Successfully updated");

            //Assert
            Assert.IsTrue(acutalResult);
        }

        /// <summary>
        /// Test Method to check ConfigurationItem_EditConfigItemWith Dangerous Request
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether Configuration item Data with Dangerous Request")]
        public void ConfigurationItem_EditConfigItemWithDangerous_Request()
        {
            //Act
            var acutalResult = _configItemPage.EditConfigItemDangerousRequest();

            //Assert
            Assert.IsTrue(acutalResult);
        }

        /// <summary>
        /// Test Method to check ConfigItem_WhenEditingDuplicateConfigItem_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether the user able to edit Configuration item with duplicate name")]
        public void ConfigItem_WhenEditingDuplicateConfigConfigItem_ReturnErrorMessage()
        {
            //Act
            var actualResult = _configItemPage.EditDuplicateConfigItem();

            //Assert
            Assert.IsTrue(actualResult);
        }
        
        #endregion

        #region Test method for Delete

        /// <summary>
        /// Test Method to Check ConfigItem delete popup opening
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user able to delete the Config Item")]
        public void ConfigItem_Delete_Popup_Opening()
        {
            //Act
            var actualResult = _configItemPage.OpenDeleteConfigItemPopUpWindow();

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to Check ConfigItem delete is working
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user to delete the Config Item")]
        public void ConfigItem_Delete_Working()
        {
            //Act
            _configItemPage.DeleteConfigItem();
            var actualResult = Helper.GetNotificationText().Contains("deactivated successfully");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to Check ConfigItem delete cancel is working
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user to delete cancel in Config Item")]
        public void ConfigItem_Delete_Cancel_Working()
        {
            //Act
            var actualResult = _configItemPage.DeleteCancelConfigItem();

            //Assert
            Assert.IsTrue(actualResult);
        }

        #endregion

        #region Test methods for paging

        /// <summary>
        /// Grid Next button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigurationItem - Grid Next button click")]
        public void ConfigurationItem_GridNextButtonClick()
        {
            //Arrange and Act  
            var currentPage = Helper.GetGridCurrentPage(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
            var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(acutalResult, currentPage + 1);
        }

        /// <summary>
        /// Grid Last button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigurationItem - Grid Last button click")]
        public void ConfigurationItem_GridLastButtonClick()
        {
            //Arrange      
            var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
            var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(acutalResult, pageCount);
        }

        /// <summary>
        /// Grid Previous button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigurationItem - Grid Previous button click")]
        public void ConfigurationItem_GridPreviousButtonClick()
        {
            //Arrange and Act
            var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Previous, Driver.Instance);
            var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(acutalResult, pageCount - 1);
        }

        /// <summary>
        /// Grid First button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigurationItem - Grid First button click")]
        public void ConfigurationItem_GridFirstButtonClick()
        {
            //Arrange and Act
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.First, Driver.Instance);
            var acutalResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(acutalResult, 1);
        }

        /// <summary>
        /// Check Whether Go To is Working.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigurationItem - GO")]
        public void ConfigurationItem_GoTo()
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

        #endregion

        #region sorting related test methods


        /// <summary>
        /// Version #: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Version #: Ascending order")]
        public void AppUser_ItemName_Sort_Ascending()
        {
            //Arrange and Act                
            var actualResult = Helper.SortByColumn(Driver.Instance, "Version #", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Version #: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Version #: Descending order")]
        public void AppUser_ItemName_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "Version #", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check ConfigItem_SortByName 
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigItem_SortByName")]
        public void ConfigItem_SortByName()
        {
            try
            {
                //Act 
                var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Ascending);

                //Assert 
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary> 
        /// Test Method to check ConfigItem_SortByName descending order 
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("ConfigItem_SortByName descending order")]
        public void ConfigItem_SortByName_DescendingOrder()
        {
            try
            {
                //Act 
                var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Descending);

                //Assert 
                Assert.IsTrue(actualResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion

        #region ==Grid Filter==

        ///// <summary>
        ///// Filter the version # column value.
        ///// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the version # column value")]
        public void EnvironmensConfig_GridFirstColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrmyGridConfigItemVersion", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrmyGridConfigItemVersion", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 1);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        #endregion

        #region ==Version Grid==

        /// <summary>
        /// Click version link button.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Click version link button")]
        public void ConfigurationItem_Version_Popup_Load()
        {
            //Assert
            LoadConfigVersionGrid();
            Assert.IsTrue(Driver.Instance.FindElement(By.XPath("//h4")).Text.Contains("Config Version Details"));
        }

        #region ==Column Sorting==

        /// <summary>
        /// Value: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Value: Ascending order")]
        public void ConfigurationItem_Version_Popup_Value_Sort_Ascending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Value", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Value: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Value: Descending order")]
        public void ConfigurationItem_Version_Popup_Value_Sort_Descending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Value", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }


        /// <summary>
        /// Version: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Version: Ascending order")]
        public void ConfigurationItem_Version_Popup_Version_Sort_Ascending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Version #", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Version: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Version: Descending order")]
        public void ConfigurationItem_Version_Popup_Version_Sort_Descending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Version #", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Available From: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Available From: Ascending order")]
        public void ConfigurationItem_Version_Popup_AvailableFrom_Sort_Ascending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Available From", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Available From: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Available From: Descending order")]
        public void ConfigurationItem_Version_Popup_AvailableFrom_Sort_Descending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Available From", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Expires On: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Expires On: Ascending order")]
        public void ConfigurationItem_Version_Popup_ExpiresOn_Sort_Ascending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Expires On", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Expires On: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Expires On: Descending order")]
        public void ConfigurationItem_Version_Popup_ExpiresOn_Sort_Descending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Expires On", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Updated On: Ascending order
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Updated On: Ascending order")]
        public void ConfigurationItem_Version_Popup_UpdatedOn_Sort_Ascending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated On", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Updated On: Descending order.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Updated On: Descending order")]
        public void ConfigurationItem_Version_Popup_UpdatedOn_Sort_Descending()
        {
            //Arrange and Act

            LoadConfigVersionGrid();
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated On", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        #endregion

        #region ==Column Filter==

        ///// <summary>
        ///// Filter the value column value.
        ///// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the value column value")]
        public void ConfigurationItem_Version_Popup_Value_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var firstCellValue = Helper.GetgridCellVaule("popupGridData", 1, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridConfigItemValue", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridConfigItemValue", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("popupGridData", 1, 1);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }



        ///// <summary>
        ///// Filter the Version column value.
        ///// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the Version column value")]
        public void ConfigurationItem_Version_Popup_Version_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var firstCellValue = Helper.GetgridCellVaule("popupGridData", 1, 2);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridConfigItemVersion", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridConfigItemVersion", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("popupGridData", 1, 2);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        ///// <summary>
        ///// Clear Filter.
        ///// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Clear Filter")]
        public void ConfigurationItem_Version_Popup_Version_Clear_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var firstCellValue = Helper.GetgridCellVaule("popupGridData", 1, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridConfigItemVersion", Value = firstCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridConfigItemVersion", Driver.Instance);
            Helper.EnterKey("btnpopupGridUpdateDate", Driver.Instance);
            //Assert
            Assert.AreEqual(Driver.Instance.FindElement(By.Id("txtSgFltrpopupGridConfigItemVersion")).Text, string.Empty);
        }

        /// <summary>
        /// Filter the Available From value.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the Available From column value")]
        public void ConfigurationItem_Version_Popup_Available_From_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var thirdCellValue = Helper.GetgridCellVaule("popupGridData", 1, 3);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridAvailableFrom", Value = thirdCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridAvailableFrom", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("popupGridData", 1, 3);

            //Assert
            Assert.AreEqual(acutalResult, thirdCellValue);
        }



        /// <summary>
        /// Filter the Expires On column value.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the Expires On column value")]
        public void ConfigurationItem_Version_Popup_Expires_On_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var fourthCellValue = Helper.GetgridCellVaule("popupGridData", 1, 4);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridExpiresOn", Value = fourthCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridExpiresOn", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("popupGridData", 1, 4);

            //Assert
            Assert.AreEqual(acutalResult, fourthCellValue);
        }


        /// <summary>
        /// Filter the Updated On column value.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the Updated On column value")]
        public void ConfigurationItem_Version_Popup_Updated_On_Filter()
        {
            //Arrange and Act
            LoadConfigVersionGrid();
            var fifthCellValue = Helper.GetgridCellVaule("popupGridData", 1, 5);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails() { Name = "txtSgFltrpopupGridUpdateDate", Value = fifthCellValue, Type = ControlType.TextBox });
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrpopupGridUpdateDate", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("popupGridData", 1, 5);

            //Assert
            Assert.AreEqual(acutalResult, fifthCellValue);
        }

        #endregion

        #region ==Grid Navigation==

        const string popupGridName = "popupGrid";

        /// <summary>
        /// Grid Next button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Grid Next button click")]
        public void EnvironmentConfigs_Grid_Next_Button_Click()
        {
            LoadConfigVersionGrid();
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst + popupGridName)))
            {
                //Arrange and Act
                var currentPage = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance, popupGridName);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);

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
        public void EnvironmentConfigs_Grid_Last_Button_Click()
        {
            LoadConfigVersionGrid();
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst + popupGridName)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance, popupGridName);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance, popupGridName);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);

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
        public void EnvironmentConfigs_Grid_Previous_Button_Click()
        {
            LoadConfigVersionGrid();
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst + popupGridName)))
            {
                //Arrange and Act
                var pageCount = Helper.GetGridTotalPageCount(Driver.Instance, popupGridName);
                Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance, popupGridName);
                Helper.GridNavigationEvent(NavigateType.Previous, Driver.Instance, popupGridName);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);

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
        public void EnvironmentConfigs_Grid_First_Button_Click()
        {
            LoadConfigVersionGrid();
            //Check paging is available
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst + popupGridName)))
            {
                //Arrange and Act
                Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance, popupGridName);
                Helper.GridNavigationEvent(NavigateType.First, Driver.Instance, popupGridName);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);

                //Assert
                Assert.AreEqual(acutalResult, 1);
            }
        }

        /// <summary>
        /// Check Whether Go To is Working.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("EnvironmentConfigs - GO")]
        public void EnvironmentConfigs_GoTo()
        {
            LoadConfigVersionGrid();
            if (Helper.IsElementPresent(By.Id(Constants.GridFirst + popupGridName)))
            {
                //Arrange and Act
                var page = Helper.GetGridTotalPageCount(Driver.Instance, popupGridName) - 1;
                Helper.GoToNavigation(page, Driver.Instance, popupGridName);
                var acutalResult = Helper.GetGridCurrentPage(Driver.Instance, popupGridName);

                //Assert
                Assert.AreEqual(acutalResult, page);
            }

        }

        #endregion

        private static void LoadConfigVersionGrid()
        {
            //Act
            Helper.GridLinkButtonClick("myGridData", 1, 3, Driver.Instance);
            Thread.Sleep(2000);
        }

        #endregion

        #region == Lock Icon for secured config item ==

        /// <summary>
        /// Check for lock icon when a config item is secured
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check for lock icon when a config item is secured")]
        public void ConfigurationItem_CheckForLockIcon_WhenAConfigItemValue_IsSecured()
        {
            //Act
            var actualResult= _configItemPage.CheckSecuredConfigItemLockIcon(true);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Check display of actual config item when it is not encrypted
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check display of actual config item when it is not encrypted")]
        public void ConfigurationItem_CheckForItemValue_WhenAConfigItemValue_IsNotSecured()
        {
            //Act
            var actualResult = _configItemPage.CheckSecuredConfigItemLockIcon();

            //Assert
            Assert.IsTrue(!actualResult);
        }

        #endregion

        #region == Available From an Expires On date issue ==

        /// <summary>
        /// Display error message when updating config item with increased version and overlapped Available from and Expires on Date
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Display error message when updating config item with increased version and overlapped Available from and Expires on Date")]
        public void ConfigurationItem_WhenAvailableDateOverlapsAndSelectedIncreasedVersion_ReturnErrorMessage()
        {
            //Act
            var actualResult = _configItemPage.
                 EditConfigItemWithAvailableFromAndExpiresOnValidation(DateTime.Now, DateTime.Now, isSelectedIncreasedVersion: true,
                 textToBePresent: "Please modify the date so there is no overlap in date.");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Successfully update the config item when increased version selected and Available from and Expired on Date is not overlapped
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Successfully update the config item when increased version selected and Available from and Expired on Date is not overlapped")]
        public void ConfigurationItem_WhenAvailableDateNotOverlapedAndSelectedIncreasedVersion_ReturnSucessMessage()
        {
            //Act
            var actualResult = _configItemPage.EditConfigItemWithAvailableFromAndExpiresOnValidation(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3),
                true,textToBePresent: "Successfully updated");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Successfully update the config item when increased version is not selected but Available from and Expired on Date is overlapped
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Successfully update the config item when increased version is not selected but Available from and Expired on Date is overlapped")]
        public void ConfigurationItem_WhenAvailableDateOverlapedAndIncreasedVersionNotSelected_ReturnSucessMessage()
        {
            //Act
            var actualResult = _configItemPage.EditConfigItemWithAvailableFromAndExpiresOnValidation(DateTime.Now, DateTime.Now,
                 false,textToBePresent: "Successfully updated");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Show Version dropdown when increase version checkbox  is not selected.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Hide Version dropdown when increase version checkbox is not selected.")]
        public void ConfigurationItem_WhenIncreaseVersionNotSelected_ShowVersionDropDown()
        {
            //Act
            var actualResult = _configItemPage.
                 EditConfigItemWithAvailableFromAndExpiresOnValidation(DateTime.Now, DateTime.Now, checkIncreasedVersionValidation: "False");

            //Assert
            Assert.IsFalse(actualResult);
        }


        /// <summary>
        /// Save Config item without available from and expires on
        /// </summary>
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Available From And Expires On")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US39234")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "439631")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "444039")]
        [TestMethod]
        public void ConfigurationItem_Submit_WithOut_AvailableFrom_ExpiresOn_Pass()
        {

            IWebElement addItem = Driver.Instance.FindElement(By.Id("create-item"));
            if (addItem != null)
            {
                Actions actionAddItem = new Actions(Driver.Instance);
                actionAddItem.Click(addItem).Perform();
                Thread.Sleep(3000);
            }

            var controlName = new List<ControlDetails>();
            string x = "NewDevTest";            

            x = x + (Convert.ToString(Guid.NewGuid()));
            controlName.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = x });
            Helper.AssignValuesToControl(controlName, Driver.Instance);


            string y = "NewDevTestValue";
            y = y + (Convert.ToString(Guid.NewGuid()));

            var controlValue = new List<ControlDetails>();
            controlValue.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = y });
            Helper.AssignValuesToControl(controlValue, Driver.Instance);

            var controlAppNamespace = new List<ControlDetails>();
            controlAppNamespace.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Config.Test.Dev" });
            Helper.AssignValuesToControl(controlAppNamespace, Driver.Instance);

            var controlType = new List<ControlDetails>();
            controlType.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            Helper.AssignValuesToControl(controlType, Driver.Instance);



            IWebElement availableFrom = Driver.Instance.FindElement(By.Id("AvailableFromDate"));
            IWebElement expiresOn = Driver.Instance.FindElement(By.Id("ExpiresOnDate"));
            IWebElement btnSave = Driver.Instance.FindElement(By.Id("modalSubmit"));
            if (availableFrom != null && expiresOn != null)
            {
                var controlAvailable = new List<ControlDetails>();
                controlAvailable.Add(new ControlDetails { Name = "AvailableFromDate", Type = ControlType.TextBox, Value = "" });
                Helper.AssignValuesToControl(controlAvailable, Driver.Instance);

                var controlExpires = new List<ControlDetails>();
                controlExpires.Add(new ControlDetails { Name = "ExpiresOnDate", Type = ControlType.TextBox, Value = "" });
                Helper.AssignValuesToControl(controlExpires, Driver.Instance);


                
                IWebElement modalBody = Driver.Instance.FindElement(By.ClassName("modal-body"));
                modalBody.Click();
                Actions action = new Actions(Driver.Instance);
                action.Click(btnSave).Perform();                
                Thread.Sleep(5000);                
                IWebElement notifyElement = Driver.Instance.FindElement(By.Id("notification"));
                if (notifyElement != null)
                {
                    string[] strSplit = notifyElement.Text.Split('\n');
                    if (strSplit.Length > 1)
                    {
                        string[] strSplitValues = x.Split('-');
                        string concatenate=string.Empty;
                        for(int s = 0; s < strSplitValues.Length; s++)
                        {
                            concatenate = concatenate + strSplitValues[s];
                        }
                        string message = "Configuration Item Name: " + "'" + concatenate + "'" + " config item value is " + "'" + y + "'" + ", Saved Successfully";
                        Assert.AreEqual(strSplit[1], message);
                        Thread.Sleep(1000);
                        Helper.GridDoubleClick("myGrid", 1, Driver.Instance);
                        Thread.Sleep(1000);
                        Assert.AreEqual(Helper.IsElementPresent(By.Id("IsVersionRequired")), false); var dateTimeAvf = DateTime.Parse("01/14/2018").AddYears(1).ToShortDateString();
                        var controlAvailableFrom = new List<ControlDetails>();
                        controlAvailableFrom.Add(new ControlDetails { Name = "AvailableFromDate", Type = ControlType.TextBox, Value = dateTimeAvf });                        
                        Helper.AssignValuesToControl(controlAvailableFrom, Driver.Instance);
                        IWebElement SaveButton = Driver.Instance.FindElement(By.Id("modalSubmit"));
                        IWebElement modal = Driver.Instance.FindElement(By.ClassName("modal-body"));
                        modal.Click();
                        Actions actionNoExp = new Actions(Driver.Instance);
                        actionNoExp.Click(SaveButton).Perform();
                        Thread.Sleep(5000);     
                        IWebElement error = Driver.Instance.FindElement(By.Id("error"));
                        Assert.AreEqual(error.Text, "You must enter both an Available From and Expires On Date");                           
                    }
                }
            }
        }

        /// <summary>
        /// Save Config item with available from and expires on
        /// </summary>
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("With Available From And Expires On")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US41982")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "461895")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "482681")]
        [TestMethod]
        public void ConfigurationItem_Submit_With_AvailableFrom_ExpiresOn_Pass()
        {
            ConfigurationItem_Submit_WithOut_AvailableFrom_ExpiresOn_Pass();
            IWebElement availTimes = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[9]/div[2]/div/span/span"));
            availTimes.Click();
            IWebElement availDate = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div[3]/table/tbody/tr[6]/td[3]"));
            availDate.Click();
            IWebElement availTime = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div[2]/table/tbody/tr/td/span[20]"));
            availTime.Click();
            IWebElement availTimePerMinute = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div[1]/table/tbody/tr/td/span[34]"));
            availTimePerMinute.Click();
            IWebElement TimeExpires = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[10]/div[2]/div/span/span"));
            TimeExpires.Click();
            IWebElement Expires = Driver.Instance.FindElement(By.XPath("/html/body/div[5]/div[3]/table/tbody/tr[6]/td[4]"));
            Expires.Click();
            IWebElement ExpiresValue = Driver.Instance.FindElement(By.XPath("/html/body/div[5]/div[2]/table/tbody/tr/td/span[20]"));
            ExpiresValue.Click();
            IWebElement ExpiresPerMinute = Driver.Instance.FindElement(By.XPath("/html/body/div[5]/div[1]/table/tbody/tr/td/span[10]")); 
            ExpiresPerMinute.Click();
            IWebElement SaveButton = Driver.Instance.FindElement(By.Id("modalSubmit"));
            IWebElement modal = Driver.Instance.FindElement(By.ClassName("modal-body"));
            modal.Click();
            Actions actionNoExp = new Actions(Driver.Instance);
            actionNoExp.Click(SaveButton).Perform();
            Thread.Sleep(5000);
            IWebElement notifyElement = Driver.Instance.FindElement(By.Id("notification"));
            if (notifyElement != null)
            {
                string[] strSplit = notifyElement.Text.Split('\n');
                if (strSplit.Length > 1)
                {
                    Assert.IsTrue(strSplit[1].Contains("Successfully updated the config item"));
                }
            }
        }

        /// <summary>
        /// UAT:Exception message is displayed if user tries to modify the existing unsecured config item
        /// </summary>

        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Exception message is displayed if user tries to modify the existing unsecured config item")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "DE13429")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "468544")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "480023")]
        [TestMethod]
        public void Configuration_Check_Exception_Existing_UnSecured_Item()
        {
            ConfigurationItem_Submit_With_AvailableFrom_ExpiresOn_Pass();
            Actions action = new Actions(Driver.Instance);
            IWebElement clickConfigItem = Driver.Instance.FindElement(By.Id("myGridRow0"));
            action.DoubleClick(clickConfigItem).Perform();
            Thread.Sleep(5000);
            IWebElement chkVersion = Driver.Instance.FindElement(By.Id("IsVersionRequired"));
            chkVersion.Click();
            IWebElement chkIsSecure = Driver.Instance.FindElement(By.Id("IsSecure"));
            chkIsSecure.Click();
            IWebElement btnSubmit = Driver.Instance.FindElement(By.Id("modalSubmit"));
            btnSubmit.Click();
            Thread.Sleep(5000);
            IWebElement divError = Driver.Instance.FindElement(By.Id("error"));
            Assert.IsTrue(divError.Text.Contains("Please modify the date"));
            Thread.Sleep(5000);
            IWebElement chkVersionCheckBox = Driver.Instance.FindElement(By.Id("IsVersionRequired"));
            chkVersionCheckBox.Click();
            IWebElement submitButton = Driver.Instance.FindElement(By.Id("modalSubmit"));
            submitButton.Click();
            Thread.Sleep(5000);
            IWebElement notifyElement = Driver.Instance.FindElement(By.Id("notification"));
            if (notifyElement != null)
            {
                string[] strSplit = notifyElement.Text.Split('\n');
                if (strSplit.Length > 1)
                {
                    Assert.IsTrue(strSplit[1].Contains("Successfully updated the config item"));
                }
            }
        }

        /// <summary>
        /// UAT:Exception message is displayed if user tries to modify the existing unsecured config item
        /// </summary>

        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("UAT:'Is Secured' checkbox is disabled if user tries to modify the existing unsecured config item")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "DE13429")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "468544")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "480023")]
        [TestMethod]
        public void Configuration_Check_Is_Secured_Disabled()
        {
            ConfigurationItem_Submit_With_AvailableFrom_ExpiresOn_Pass();
            IWebElement firstRecord = Driver.Instance.FindElement(By.Id("myGridRow0"));
            Actions action = new Actions(Driver.Instance);
            action.DoubleClick(firstRecord).Perform();
            Thread.Sleep(5000);
            IWebElement chkVersion = Driver.Instance.FindElement(By.Id("IsVersionRequired"));
            chkVersion.Click();
            Thread.Sleep(5000);
            IWebElement chkIsSecure = Driver.Instance.FindElement(By.Id("IsSecure"));
            chkIsSecure.Click();
            Thread.Sleep(5000);
            IWebElement btnSubmit = Driver.Instance.FindElement(By.Id("modalSubmit"));
            btnSubmit.Click();
            Thread.Sleep(5000);
            IWebElement divError = Driver.Instance.FindElement(By.Id("error"));
            Assert.IsTrue(divError.Text.Contains("Please modify the date"));
            var containsDisabled = Driver.Instance.FindElement(By.XPath("//*[@id='IsSecure']"));
            Assert.AreEqual(containsDisabled.Enabled, true);
        }


        /// <summary>
        /// [Config Platform] make grid view more responsive
        /// </summary>

        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Pagination is separated from the grid view")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US41154")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "454481")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "482696")]
        [TestMethod]
        public void Configuration_Pagination_Separated_From_Grid()
        {
            IWebElement checkForFooterText = Driver.Instance.FindElement(By.ClassName("supergrid-grid-footer-row"));
            Assert.AreEqual(checkForFooterText.Text, string.Empty);
            var checkElement = Driver.Instance.FindElement(By.Id("pagecontrol-myGrid"));
            Assert.IsNotNull(checkElement);
        }

        /// <summary>
        /// [Config Platform] make grid view more responsive
        /// </summary>        
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Scroll bars show based on how much data needs to be provided/is available.")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US41154")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "454481")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "482696")]
        [TestMethod]
        public void Configuration_Scroll_Position_In_Grid()
        {
            var driver = Driver.Instance;
            ((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(1024, 768);");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(500, 200);");
            Assert.IsTrue(Helper.IsElementPresent(By.ClassName("table-responsive")));
        }

        /// <summary>
        /// [Config Platform] make grid view more responsive
        /// </summary>        
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Each column width has a minimum width that would look good.IE buttons are legible and within the column")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US41154")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "454481")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "482696")]
        [TestMethod]
        public void Configuration_Check_Width_Is_Set_For_Columns()
        {
            IWebElement checkSuperGridFirstColumn = Driver.Instance.FindElement(By.XPath("//*[@id='myGridRow0']/td[1]"));
            var getAttribute1 = checkSuperGridFirstColumn.GetAttribute("style");
            Assert.AreEqual(getAttribute1, "width: 20% !important;");
            IWebElement checkSuperGridSecondColumn = Driver.Instance.FindElement(By.XPath("//*[@id='myGridRow0']/td[2]"));
            var getAttribute2 = checkSuperGridSecondColumn.GetAttribute("style");
            Assert.AreEqual(getAttribute2, "width: 20% !important;");
            IWebElement checkSuperGridThirdColumn = Driver.Instance.FindElement(By.XPath("//*[@id='myGridRow0']/td[3]"));
            var getAttribute3 = checkSuperGridThirdColumn.GetAttribute("style");
            Assert.AreEqual(getAttribute3, "font-weight: bolder; width: 10%;");
        }

        /// <summary>
        /// [Config Platform] make grid view more responsive
        /// </summary>        
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Text stays within column and doesn't overlap into the next")]
        [TestProperty(TestAttributes.TestProperties.UseCaseID, "US41154")]
        [TestProperty(TestAttributes.TestProperties.TFSTaskID, "454481")]
        [TestProperty(TestAttributes.TestProperties.TestCaseID, "482696")]
        [TestMethod]
        public void Configuration_Check_Column_Text_Overlap()
        {
            ConfigurationItem_Submit_With_AvailableFrom_ExpiresOn_Pass();
            IWebElement TruncateContent = Driver.Instance.FindElement(By.ClassName("supergrid-grid-column-cell-wrapContent"));
            Assert.AreEqual(TruncateContent.GetCssValue("word-wrap"), "break-word");
        }
        #endregion



        #region AppNamespace Column Sorting

        /// <summary>
        /// ConfigurationItem_AppNamespace_Sort_Ascending
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("App Namespace: Ascending order")]
        public void ConfigurationItem_AppNamespace_Sort_Ascending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "App Namespace", SortOrder.Ascending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// ConfigurationItem_AppNamespace_Sort_Descending
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("App Namespace: Descending order")]
        public void ConfigurationItem_AppNamespace_Sort_Descending()
        {
            //Arrange and Act
            var actualResult = Helper.SortByColumn(Driver.Instance, "App Namespace", SortOrder.Descending);

            //Assert
            Assert.IsTrue(actualResult);
        }

        #endregion AppNamespace Column Sorting

        #region Delete Config Item 

        /// <summary>
        /// Test Method to Check whether the user to deactivate the Config Item
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user to deactivate the Config Item")]
        public void ConfigAppNamespace_Delete_Working()
        {
            //Act
            _configItemPage.DeleteConfigItem();
            var actualResult = Helper.GetNotificationText().Contains("deactivated successfully");

            //Assert
            Assert.IsTrue(actualResult);
        }




        /// <summary>
        /// Config_Grid_Name_ColumnFilter
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the version # column value")]
        public void Config_Grid_Name_ColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 1);
            var controlDetails = new List<ControlDetails>
            {
                new ControlDetails() {Name = "txtSgFltrmyGridName", Value = firstCellValue, Type = ControlType.TextBox}
            };
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrmyGridName", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 1);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }


        /// <summary>
        /// Config_Grid_Version_ColumnFilter
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the version # column value")]
        public void Config_Grid_Version_ColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 3);
            var controlDetails = new List<ControlDetails>
            {
                new ControlDetails() {Name = "txtSgFltrmyGridConfigItemVersion", Value = firstCellValue, Type = ControlType.TextBox }
            };
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrmyGridConfigItemVersion", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 3);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        /// <summary>
        /// Config_Grid_Value_ColumnFilter
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the version # column value")]
        public void Config_Grid_Value_ColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 2);
            var controlDetails = new List<ControlDetails>
            {
                new ControlDetails() {Name = "txtSgFltrmyGridValue", Value = "1", Type = ControlType.TextBox }
            };
            Helper.AssignValuesToControl(controlDetails, Driver.Instance);
            Helper.EnterKey("txtSgFltrmyGridValue", Driver.Instance);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1,2);

            //Assert
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        /// <summary>
        /// Filter the ExpiresOn column value
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the ExpiresOn column value")]
        public void Config_Grid_ExpiresOn_ColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 7);
            var result = Helper.FilterByColumnConfigItem(Driver.Instance, "Expires On", firstCellValue.Split(' ')[0], isDateFilter: true);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 7);

            //Assert
            if (result)
            Assert.AreEqual(acutalResult, firstCellValue);
        }


        /// <summary>
        /// Filter the Available_From  column value
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Filter the Available_From  column value")]
        public void Config_Grid_Available_From_ColumnFilter()
        {
            //Arrange and Act
            var firstCellValue = Helper.GetgridCellVaule("myGridData", 1, 6);
            var result = Helper.FilterByColumnConfigItem(Driver.Instance, "Available From", firstCellValue.Split(' ')[0], isDateFilter: true);
            var acutalResult = Helper.GetgridCellVaule("myGridData", 1, 6);

            //Assert
            if (result)
            Assert.AreEqual(acutalResult, firstCellValue);
        }

        #endregion
    }

}

