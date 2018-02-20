using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Serve.Internal.BusinessServices.Shared.Tests.DataContracts;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;
using Serve.Platform.Configuration.UI.SeleniumTest.Pages;
using System.Threading;
using System.Collections.Generic;
using System;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium.Interactions;





namespace Serve.Platform.Configuration.UI.SeleniumTest.FunctionalTests
{
    /// <summary>
    /// ConfigItemMetadata end to end testing
    /// </summary>
    [TestClass]
    [Ignore]
    public class ConfigItemMetadataUITest : SeleniumTestBase
    {
        private static ConfigItemMetaDataPage _configItemMetadataPage;

        #region Setup and Cleanup methods

        /// <summary>
        /// Cleanup
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _configItemMetadataPage = new ConfigItemMetaDataPage(Driver.Instance);
        }

        #endregion

        #region  Test methods for ConfigItem Metadata search screen

        /// <summary>
        /// Check whether the ConfigItem Metadata search screen is loaded
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the ConfigItem Metadata search screen is loaded")]
        public void ConfigItem_Metadata_LoadSearchScreen()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.LoadConfigItemMetaDataSearchScreen();
            Thread.Sleep(500);

            //Assert
            Assert.IsTrue(actualResult, "Page Title is incorrect");
        }

        /// <summary>
        /// Check whether the  ConfigItem Metadata search screen loaded with  ConfigItem Metadata grid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether  ConfigItem Metadata search screen loaded with  ConfigItem Metadata grid")]
        public void ConfigItem_Metadata_LoadConfigItemGrid()
        {
            Thread.Sleep(500);
            //Act
            var configItemMetadataGridRows = _configItemMetadataPage.CheckRecordsInConfigItemMetaDataGrids();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(configItemMetadataGridRows > 0);
        }

        /// <summary>
        /// Check whether No records found message displayed when searched with invalid name.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether No records found message displayed when searched with invalid name.")]
        public void ConfigItem_Metadata_WhenSearchedByInvalidName_ReturnNoRecordsFoundMessage()
        {
            Thread.Sleep(1000);
            //Act
            var actualResult = _configItemMetadataPage.SearchConfigItemMetaDataByInvalidName();
            Thread.Sleep(1000);
            //Assert
            Assert.IsTrue(actualResult, "Did not return correct response.");
        }

        /// <summary>
        /// Check whether ConfigItem Metadata for the given name is loaded 
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata for the given name is loaded")]
        public void ConfigItem_Metadata_WhenSearchedByName_LoadConfigItemOfGivenName()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.SearchConfigurationItemMetaByName();
            Thread.Sleep(1000);
            //Assert
            Assert.IsTrue(actualResult, "Did not return correct response.");
        }


        /// <summary>
        /// Check whether all ConfigItem Metadata are loaded when clear image button in the search by name text box is clicked.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether all ConfigItem Metadata are loaded when clear image button in the search by name text box is clicked.")]
        public void ConfigItem_Metadata_WhenSearchedByNameClearIconClicked_LoadAllConfigItem()
        {
            Thread.Sleep(500);
            //Act
            var expected = _configItemMetadataPage.ClickTextInSearchByNameTextBox();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(expected);
        }

        #region Add New Metadata Working

        /// <summary>
        /// Test method to check the ConfigItem_Metadata_WhenSearchByInActiveCheckBox_Checked_InActiveRecordsPresent_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether all ConfigItem Metadata inactive records present in grid when inactive check box is clicked.")]
        public void ConfigItem_Metadata_WhenSearchByInActiveCheckBox_Unchecked_InActiveRecordsPresent_InGrid()
        {
            Thread.Sleep(500);
            //Act
            var name = _configItemMetadataPage.ClickInActiveCheckBoxSearchByInactiveRecords();
            Thread.Sleep(500);
            var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");

            //Assert
            Assert.AreEqual(actualResult[0], name);
        }

        /// <summary>
        /// ConfigItem_Metadata_WhenSearchByInActiveCheckBox_UnChecked_ActiveRecordsPresent_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether all ConfigItem Metadata active records present in grid when inactive check box is Unchecked.")]
        public void ConfigItem_Metadata_WhenSearchByInActiveCheckBox_Checked_ActiveRecordsPresent_InGrid()
        {
            Thread.Sleep(500);
            //Act
            var name = _configItemMetadataPage.AddNewConfigItemMetaData();
            Thread.Sleep(500);
            var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");

            //Assert
            Assert.AreEqual(actualResult[0], name);
        }

        #endregion

        #endregion

        #region  Test methods for add config item

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_WhenAddNewItemLinkClicked_DisplayAddScreen
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether Add new config item Metadata button clicked properly")]
        public void ConfigItem_Metadata_WhenAddNewItemLinkClicked_DisplayAddScreen()
        {
            Thread.Sleep(500);
            var actualResult = _configItemMetadataPage.ClickAddNewConfigItemMetaDataButton();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult, "Page Title is incorrect");
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_AddWithValidFields
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Data with Valid Fields")]
        public void ConfigItem_Metadata_AddWithValidFields()
        {
            Thread.Sleep(500);
            //Act
            var configItemMetadatName = _configItemMetadataPage.AddNewConfigItemMetaData();
            Thread.Sleep(500);
            var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");

            //Assert
            Assert.AreEqual(actualResult[0], configItemMetadatName);
        }


        /// <summary>
        /// Test Method to check ConfigItem_Metadata_AddWithSpecialChar
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Data with Valid Fields with special char")]
        public void ConfigItem_Metadata_AddWithSpecialChar()
        {
            Thread.Sleep(500);
            //Act
            var configItemName = _configItemMetadataPage.AddNewConfigItemMetaData(true);
			//var waitForTable = (new WebDriverWait(driver, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementExists(By.Id(gridDivId)));                       
            Thread.Sleep(500);
			string text = Helper.GetNotificationText();
			var actualResult = Helper.GetNotificationText().Contains("has been created");
			//var actualResult = Driver.Instance.FindElements(By.Id("notification"))[0].Text.Contains("Saved Successfully");

            //Assert
			Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata With Dangerous Request
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Data with  Dangerous Request")]
        public void ConfigItem_Metadata_AddWithDangerousRequest()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.AddConfigurationMetadataWithDangerousRequest();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to check whether the user able to add ConfigItem Metadata with duplicate name
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user able to add ConfigItem Metadata with duplicate name")]
        public void ConfigItem_Metadata_WhenAddingDuplicateConfigItemMetadata_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.AddDuplicateConfigurationMetadata();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult);
        }


        /// <summary>
        /// Check ConfigItem Metadata name validation
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check ConfigItem Metadata name validation")]
        public void ConfigItem_Metadata_WhenConfigItemWithoutName_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Arrange
            var expectedResult = "This is a required field.";
            Thread.Sleep(500);
            //Act
            _configItemMetadataPage.ValidateRequiredField("Name");
            var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_WhenConfigItemWithoutDescription_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Description validation")]
        public void ConfigItem_Metadata_WhenConfigItemWithoutValue_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Arrange
            var expectedResult = "This is a required field.";
            Thread.Sleep(500);
            //Act
            _configItemMetadataPage.ValidateRequiredField("Description");
            var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata When ConfigItem_Metada Without Fully Qualified Type Name_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Fully Qualified Type Name validation")]
        public void ConfigItem_Metadata_WhenConfigItemMetadata_FullyQualified_Type_Name_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Arrange
            var expectedResult = "This is a required field.";
            Thread.Sleep(500);
            //Act
            _configItemMetadataPage.ValidateRequiredField("FullyQualifiedTypeName");
            var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_WhenConfigItemWithout InternalType_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata InternalType validation")]
        public void ConfigItem_Metadata_WhenConfigItemMetadataWithoutInternalType_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Arrange
            var expectedResult = "This is a required field.";
            
            //Act
            _configItemMetadataPage.ValidateRequiredField("InternalType");
            Thread.Sleep(500);
            var actualResult = Driver.Instance.FindElement(By.ClassName(Constants.ErrorMessageId)).Text;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }



        #endregion

        #region Test methods for edit config item

        /// <summary>
        /// Test Method to check ConfigItem_Metadata WhenDoubleClickingRecord_InGrid
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem_Metadata_WhenDoubleClickingRecord_InGrid ")]
        public void AppNamespace_WhenDoubleClickingRecord_InGrid()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.OpenEditConfigItemMetaDataPopUpWindow();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult, "Page Title is incorrect");
        }


        //<summary>
        //Test Method to check ConfigItem_WhenEditing ConfigItem_Metadata WithValidFields
        //</summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [TestProperty("Workitem", "285815")]
        [Description("Check whether ConfigItem Metadata with valid records while editing")]
        public void ConfigItem_WhenEditing_ConfigItem_MetadataWithValidFields()
        {
            Thread.Sleep(500);
            //Act
            var name = _configItemMetadataPage.EditConfigItemMetaData();
            Thread.Sleep(500);
            var actualResult = Helper.ValidateGridColumnValue("myGrid", Driver.Instance, "Name");
            //Assert
            Assert.AreEqual(actualResult[0], name);
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_Edit With SpecialChar
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Data with special char")]
        public void ConfigItem_Metadata_EditWithSpecialChar()
        {
            Thread.Sleep(500);
            //Act
            var configItemName = _configItemMetadataPage.EditConfigItemMetaData(true);
            Thread.Sleep(500);
            var actualResult = Helper.GetNotificationText().Contains("has been updated");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to check ConfigItem_Metadata_Edit With Dangerous Request
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether ConfigItem Metadata Data with Dangerous Request")]
        public void ConfigItem_Metadata_EditConfigItemWithDangerous_Request()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.EditConfigurationMetadataWithDangerousRequest();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to check ConfigItemMetadata_When Editing DuplicateConfigItemMetadata_ReturnErrorMessage
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user able to edit ConfigItem Metadata with duplicate name")]
        public void ConfigItem_WhenEditingDuplicateConfigItemMetadata_ReturnErrorMessage()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.EditDuplicateConfigItemMetaData();

            Thread.Sleep(500);

            //Assert
            Assert.IsTrue(actualResult);
        }


        #endregion

        #region Test method for Delete

        /// <summary>
        /// Test Method to Check ConfigItemMetadata delete popup opening
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user able to delete the Config Item Metadata")]
        public void ConfigItemMetadata_Delete_Popup_Opening()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.OpenDeleteConfigItemMetaDataPopUpWindow();
            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to Check ConfigItemMetadata delete is working
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user to delete the Config Item Metadata")]
       // [Ignore]
        public void ConfigItemMetadata_Delete_Working()
        {
            Thread.Sleep(500);
            //Act
            _configItemMetadataPage.DeleteConfigItemMetadata();
			//TODO: need to find better solution of waiting for the wait icon to disappear.
			Thread.Sleep(3000);
            var actualResult = Serve.Platform.Configuration.UI.SeleniumTest.Common.Helper.GetNotificationText().Contains("config appnamespace deactivated successfully.");

            //Assert
            Assert.IsTrue(actualResult);
        }

        /// <summary>
        /// Test Method to Check ConfigItemMetadata delete cancel is working
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("Check whether the user to delete cancel in Config Item Metadata")]
        public void ConfigItemMetadata_Delete_Cancel_Working()
        {
            Thread.Sleep(500);
            //Act
            var actualResult = _configItemMetadataPage.DeleteCancelConfigItemMetadata();
            Thread.Sleep(500);
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
        [Description("ConfigItem_Metadata - Grid Next button click")]
        public void ConfigItem_Metadata_GridNextButtonClick()
        {
            //Arrange and Act 
            Thread.Sleep(500);
            var currentPage = Helper.GetGridCurrentPage(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
            var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(actualResult, currentPage + 1);
        }

        /// <summary>
        /// Grid Last button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItem_Metadata - Grid Last button click")]
        public void ConfigItem_Metadata_GridLastButtonClick()
        {
            Thread.Sleep(500);
            //Arrange      
            var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
            Thread.Sleep(6000);
            var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(actualResult, pageCount);
        }

        /// <summary>
        /// Grid Previous button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItem_Metadata - Grid Previous button click")]
        public void ConfigItem_Metadata_GridPreviousButtonClick()
        {
            Thread.Sleep(500);
            //Arrange and Act
            var pageCount = Helper.GetGridTotalPageCount(Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Last, Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Previous, Driver.Instance);
            var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(actualResult, pageCount - 1);
        }

        /// <summary>
        /// Grid First button click.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItem_Metadata - Grid First button click")]
        public void ConfigItem_Metadata_GridFirstButtonClick()
        {
            //Arrange and Act
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.Next, Driver.Instance);
            Thread.Sleep(3000);
            Helper.GridNavigationEvent(NavigateType.First, Driver.Instance);
            var actualResult = Helper.GetGridCurrentPage(Driver.Instance);

            //Assert
            Assert.AreEqual(actualResult, 1);
        }
        /// <summary>
        /// Check Whether Go To is Working.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigurationItem_Metadata - GO")]
        public void ConfigurationItem_Metadata_GoTo()
        {
            Thread.Sleep(500);
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

        #region sorting related test methods

        /// <summary> 
        /// Test Method to check ConfigItemMetadata_SortByName 
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata_SortByName")]
        public void ConfigItemMetadata_SortByName()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Ascending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check ConfigItemMetadata_SortByName descending order 
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata_SortByName descending order")]
        public void ConfigItemMetadata_SortByName_DescendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Name", SortOrder.Descending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by updatedOn in Ascending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should be Sorted By UpdatedOn AscendingOrder")]
        public void ConfigItemMetadata_SortByUpdatedOn_AscendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated On", SortOrder.Ascending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by updatedOn in Descending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should Sorted By UpdatedOn descending order")]
        public void ConfigItemMetadata_SortByUpdatedOn_DescendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated On", SortOrder.Descending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by Description in Ascending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should be Sorted By Description AscendingOrder")]
        public void ConfigItemMetadata_SortByDescription_AscendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Description", SortOrder.Ascending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by Description in Descending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should Sorted By Description descending order")]
        public void ConfigItemMetadata_SortByDescription_DescendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Description", SortOrder.Descending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by UpdatedBy in Ascending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should be Sorted By UpdatedBy AscendingOrder")]
        public void ConfigItemMetadata_SortByUpdatedBy_AscendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated By", SortOrder.Ascending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by UpdatedBy in Descending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should Sorted By UpdatedBy descending order")]
        public void ConfigItemMetadata_SortByUpdatedBy_DescendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "Updated By", SortOrder.Descending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by InternalType in Ascending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should be Sorted By InternalType AscendingOrder")]
        public void ConfigItemMetadata_SortByInternalType_AscendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "InternalType", SortOrder.Ascending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is sorted by InternalType in Descending Order
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should Sorted By InternalType descending order")]
        public void ConfigItemMetadata_SortByInternalType_DescendingOrder()
        {
            Thread.Sleep(3000);
            //Act 
            var actualResult = Helper.SortByColumn(Driver.Instance, "InternalType", SortOrder.Descending);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is filtered by Name
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should filtered by Name")]
        public void ConfigItemMetadata_FilterByName()
        {
            Thread.Sleep(3000);
            //Act
             var result = Driver.Instance.FindElement(By.Id("myGridRow0"));
             Thread.Sleep(3000);
             var actualResult = Helper.FilterByColumn(Driver.Instance, "Name", result.Text.Split(' ')[0]);
             Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is filtered by Description
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should filtered by Description")]
        public void ConfigItemMetadata_FilterByDescription()
        {
            Thread.Sleep(3000);
            //Act
            var result = Driver.Instance.FindElement(By.Id("myGridRow0"));
            Thread.Sleep(3000);
            var actualResult = Helper.FilterByColumn(Driver.Instance, "Description", result.Text.Split(' ')[1]);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is filtered by InternalType
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should filtered by InternalType")]
        public void ConfigItemMetadata_FilterByInternalType()
        {
            Thread.Sleep(3000);
            //Act
            var result = Driver.Instance.FindElement(By.Id("myGridRow0"));
            Thread.Sleep(3000);
            var actualResult = Helper.FilterByColumn(Driver.Instance, "InternalType", result.Text.Split(' ')[2]);
            Thread.Sleep(3000); 
            //Assert 
            Assert.IsTrue(actualResult);
        }


        /// <summary> 
        /// Test Method to check Configuration Meta data is filtered by UpdatedBy
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should filtered by UpdatedBy")]
        public void ConfigItemMetadata_FilterByUpdatedBy()
        {
            Thread.Sleep(3000);
            //Act
            var result = Driver.Instance.FindElement(By.Id("myGridRow0"));
            Thread.Sleep(3000);
            var actualResult = Helper.FilterByColumn(Driver.Instance, "Updated By", result.Text.Split(' ')[4]);
            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /// <summary> 
        /// Test Method to check Configuration Meta data is filtered by UpdatedOn
        /// </summary> 
        [TestMethod]
        [TestCategory(TestCategoryType.FunctionalTest)]
        [Description("ConfigItemMetadata should filtered by UpdatedOn")]
        public void ConfigItemMetadata_FilterByUpdatedOn()
        {
            Thread.Sleep(3000);
            //Act
            var result = Driver.Instance.FindElement(By.Id("myGridRow0"));
            Thread.Sleep(3000);
            var actualResult = FilterByUpdatedOn(Driver.Instance, "Updated On", result.Text.Split(' ')[5], false, true);

            Thread.Sleep(3000);
            //Assert 
            Assert.IsTrue(actualResult);
        }

        /* To do: Control name of Updated on is txtSgFltrmyGridUpdateDate. Columnname is Updated On. In the FilterByColumn Method system appends the column name to 
         * "txtSgFltrmyGrid", which is not the control name therefore the test cases to filter Updated On always fail. Temporarily we have created the following.*/
        /// <summary>
        /// grid filter by Updated on only
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="filterByTest"></param>
        /// <param name="isClearFilter"></param>
        /// <param name="isDateFilter"></param>
        /// <param name="gridDivId"></param>
        /// <returns>bool</returns>
        private bool FilterByUpdatedOn(IWebDriver driver,
                 string columnName, string filterByTest, bool isClearFilter = false, bool isDateFilter = false, string gridDivId = Constants.GridId)
        {
            var columnList = BuildTable(gridDivId, driver);
            var index = columnList.FindIndex(0, item => item == columnName);
            if (!isDateFilter)
            {
                //To send values to textbox
                driver.FindElement(By.XPath(string.Format("//tr[@id='myGridfilterRow']/td[{0}]/input", index + 1)))
                    .SendKeys(filterByTest);
            }
            else
            {
                //To send value to a textbox which is currently disabled.
                var textbox = driver.FindElement(By.Id(string.Format("txtSgFltrmyGrid{0}", "UpdateDate")));
                ((IJavaScriptExecutor)driver).ExecuteScript(string.Format("arguments[0].value='{0}'", filterByTest),
                    textbox);

            }
            if (isClearFilter)
            {
                //To clear the column filter values
                driver.FindElement(By.Id("btnmyGridUpdatedBy")).Click();
                var textBoxValue = driver.FindElement(
                    By.XPath(string.Format("//tr[@id='myGridfilterRow']/td[{0}]/input", index + 1)))
                    .GetAttribute("value");
                return string.IsNullOrEmpty(textBoxValue);
            }

            //To click on the filter image icon.
            driver.FindElement(By.XPath(string.Format("//tr[@id='myGridfilterRow']/td[{0}]/{1}a", index + 1, isDateFilter ? "div/" : ""))).Click();
            var record = ValidateGridColumnValue(gridDivId, driver, columnName).FirstOrDefault();

            //To convert output string to MM/dd/yyyy format date string
            if (isDateFilter)
            {
                record = Convert.ToDateTime(record).ToString("MM/dd/yyyy");
            }

            //If record is null then return true . else if the record has value then compare with input.
            return record == null || (record.IndexOf(filterByTest, StringComparison.Ordinal) != -1);

        }

        /// <summary>
        /// Builds Table
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <returns>List</returns>
        private List<string> BuildTable(string gridDivId, IWebDriver driver)
        {
            // Thread.Sleep(2000);
            var waitForTable = (new WebDriverWait(driver, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementExists(By.Id(gridDivId)));

            return waitForTable.FindElements(By.XPath(string.Format("//div[@id='{0}']//table/tbody/tr/th", gridDivId))).Select(x => x.Text).ToList();
        }

        /// <summary>
        /// ValidateGridColumnValue
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="action"></param>
        /// <returns>string list</returns>
        private List<string> ValidateGridColumnValue(string gridDivId, IWebDriver driver, string columnName = null, Actions action = null, bool IsDuplicate = false)
        {
            List<string> result = new List<string>();
            if (action == null)
            {
                action = new Actions(driver);
                var columnHeader = BuildTable(gridDivId, driver);
                int columnIndex = (columnName == null) ? 1 : columnHeader.IndexOf(columnName) + 1;
                if (columnIndex >= 1)
                {
                    string s = string.Format(Constants.GridFirstPage, gridDivId);
                    var waitForTable = (new WebDriverWait(driver, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementExists(By.Id(gridDivId)));
                    var row = waitForTable.FindElements(By.XPath(string.Format("//div[@id='{0}']//table/tbody/tr[4]/td[{1}]", gridDivId, columnIndex)));
                    result = row.Where(x => !string.IsNullOrEmpty(x.Text)).Select(x => x.Text).ToList();
                }
            }
            return result;
        }
        #endregion

    }
}
