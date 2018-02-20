using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Linq;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;


namespace Serve.Platform.Configuration.UI.SeleniumTest.Pages
{
    /// <summary>
    /// ConfigItemMetaDataPage
    /// </summary>
    public class ConfigItemMetaDataPage
    {
        private static IWebDriver _driver;

        /// <summary>
        /// ConfigItemMetaDataPage
        /// </summary>
        /// <param name="driver"></param>
        public ConfigItemMetaDataPage(IWebDriver driver)
        {
            _driver = driver;
            Helper.GoToPage(Constants.ConfigItemMetaDataLink, _driver, "ads%5Ccpt_testacc_1:CupAta6a@");
        }

        #region Methods for config item search screen

        /// <summary>
        /// LoadConfigItemMetaDataSearchScreen
        /// </summary>
        /// <returns>bool</returns>
        public bool LoadConfigItemMetaDataSearchScreen()
        {
            return Helper.IsTextPresent(Constants.SearchByName, _driver);
        }

        /// <summary>
        /// CheckRecordsInConfigItemMetaDataGrids
        /// </summary>
        /// <returns>int</returns>
        public int CheckRecordsInConfigItemMetaDataGrids()
        {
            var totalPageCount = Convert.ToInt32(_driver.FindElement(By.Id(string.Format(Constants.GridTotalPageCount, Constants.GridId))).Text);
            return totalPageCount;
        }


        /// <summary>
        /// CheckShowInActiveConfigItemMetaDataCheckBox
        /// </summary>
        /// <returns>bool</returns>
        public bool CheckShowInActiveConfigItemMetaDataCheckBox(bool isActive = false)
        {
            IWebElement showInactiveItemControl = _driver.FindElement(By.XPath(Constants.ShowInactiveItemsXPath));
            if (!showInactiveItemControl.Selected)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", showInactiveItemControl);
            }
            Thread.Sleep(2000);
            return true;
        }


        /// <summary>
        /// SearchConfigurationItemByName
        /// </summary>
        /// <returns>bool</returns>
        public bool SearchConfigurationItemMetaByName()
        {
            var isRecordsExisting = false;
            _driver.FindElement(By.Id(Constants.SearchByNameTextBoxId)).SendKeys(string.Format("Con{0}", OpenQA.Selenium.Keys.Enter));
            Thread.Sleep(2000);
            var result = Helper.ValidateGridColumnValue(Constants.GridId, _driver, "Name", null);
            result.ForEach(item =>
            {
                isRecordsExisting = item.Contains(Constants.NoRecordsFoundMessage);
            });
            return !isRecordsExisting;
        }


        /// <summary>
        /// SearchConfigItemMetaDataByInvalidName
        /// </summary>
        /// <returns>bool</returns>
        public bool SearchConfigItemMetaDataByInvalidName()
        {
            var isRecordsExisting = false;
            _driver.FindElement(By.Id(Constants.SearchByNameTextBoxId)).SendKeys(string.Format("{0}{1}",
                          Constants.InvalidItemName, OpenQA.Selenium.Keys.Enter));
            Thread.Sleep(2000);
            var result = Helper.ValidateGridColumnValue(Constants.GridId, _driver, null, null);
            result.ForEach(item =>
            {
                isRecordsExisting = item.Contains(Constants.NoRecordsFoundMessage);
            });
            return isRecordsExisting;
        }


        /// <summary>
        /// ClickTextInSearchByNameTextBox
        /// </summary>       
        public bool ClickTextInSearchByNameTextBox()
        {
            SearchConfigurationItemMetaByName();
            IWebElement hiddenWebElement = _driver.FindElement(By.ClassName("clearIcon"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", hiddenWebElement);
            return true;
        }

        /// <summary>
        /// ClickInActiveCheckBoxSearchByInactiveRecords
        /// </summary>
        /// <returns>string</returns>
        public string ClickInActiveCheckBoxSearchByInactiveRecords()
        {
            Random random = new Random();
            ClickAddNewConfigItemMetaDataButton();
            var name = string.Format("ConfigItemMetaDataTestCase{0}", random.Next(1000));
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "Description", Type = ControlType.TextBox, Value = string.Format("ConfigItemMetaDataTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "InternalType", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = "FullyQualifiedTypeName", Type = ControlType.TextBox, Value = "System.String" });
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

        #region Add New config Items

        /// <summary>
        /// ClickAddNewConfigItemMetaDataButton
        /// </summary>
        /// <returns>bool</returns>
        public bool ClickAddNewConfigItemMetaDataButton()
        {
            Helper.SaveButtonClickEvent(Constants.AddNewButtonId, _driver);
            return Helper.IsTextPresent(Constants.AddNewConfigItemMetadataTitle, _driver);
        }


        /// <summary>
        /// AddNewConfigItemMetaData
        /// </summary>
        /// <returns>string</returns>
        public string AddNewConfigItemMetaData(bool UseSpecialChar = false)
        {
            Random random = new Random();
            ClickAddNewConfigItemMetaDataButton();
            var ConfigItemMetaDataName = string.Format("ConfigItemMetaDataTestCase{0}", random.Next(1000));
            var ConfigItemMetaDataDetails = CreateControlDetails(ConfigItemMetaDataName);
            if (UseSpecialChar)
            {
                ConfigItemMetaDataDetails[0].Value = ConfigItemMetaDataDetails[0].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
            }
            Helper.AssignValuesToControl(ConfigItemMetaDataDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return ConfigItemMetaDataName;
        }

        /// <summary>
        /// CreateControlDetails
        /// </summary>
        /// <param name="ConfigItemMetaDataName"></param>
        /// <returns>ControlDetails list</returns>
        private List<ControlDetails> CreateControlDetails(string ConfigItemMetaDataName = null)
        {
            Random random = new Random();
            var name = string.IsNullOrEmpty(ConfigItemMetaDataName) ? string.Format("ConfigItemMetaDataTestCase{0}", random.Next(1000)) : ConfigItemMetaDataName;
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "Description", Type = ControlType.TextBox, Value = string.Format("ConfigItemMetaDataDesc{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "InternalType", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = "FullyQualifiedTypeName", Type = ControlType.TextBox, Value = "System.String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            return controlDetails;
        }


        /// <summary>
        /// AddDuplicateConfigurationMetadata
        /// </summary>
        public bool AddDuplicateConfigurationMetadata()
        {
            var ConfigItemMetaDataName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            ClickAddNewConfigItemMetaDataButton();
            List<ControlDetails> controlDetails = CreateControlDetails(ConfigItemMetaDataName[0].ToString());
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);

            var expectedResult = string.Format("Configuration Metadata name : '{0}' already exists, please edit.", ConfigItemMetaDataName[0].ToString());
            if (_driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Equals(expectedResult))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// AddConfigurationMetadataWithDangerousRequest
        /// </summary>
        public bool AddConfigurationMetadataWithDangerousRequest()
        {
            var ConfigItemMetaDataName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            ClickAddNewConfigItemMetaDataButton();
            List<ControlDetails> controlDetails = CreateControlDetails();
            controlDetails[1].Value = "</>";
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);

            if (_driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Contains("A potentially dangerous Request"))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// ValidateRequiredField
        /// </summary>
        public void ValidateRequiredField(string controlName)
        {
            ClickAddNewConfigItemMetaDataButton();
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

        #endregion

        #region Edit Config Item


        /// OpenEditConfigAppNamespacePopUpWindow
        /// </summary>
        /// <returns>bool</returns>
        public bool OpenEditConfigItemMetaDataPopUpWindow()
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigItemGridXPath);
            if (count > 0)
            {
                Actions action = new Actions(_driver);

                IWebElement element = _driver.FindElement(By.Id("myGridRow0"));
                //Double click
                action.DoubleClick(element).Perform();
            }
            return Helper.IsTextPresent(Constants.AddNewConfigItemMetadataTitle, _driver);
        }

        /// <summary>
        /// GetNumberOfRowsInATable
        /// </summary>
        /// <param name="tableXPath"></param>
        /// <returns>int</returns>
        private int GetNumberOfRowsInATable(string tableXPath)
        {
            var waitForTable = (new WebDriverWait(_driver, TimeSpan.FromSeconds(30))).Until(ExpectedConditions.ElementExists(By.XPath(tableXPath)));

            var table = waitForTable.FindElements(By.XPath(tableXPath));

            return table
                   .Select(webElement => webElement.FindElements(By.XPath("//tr")))
                       .Select(rows => rows.Count)
                       .FirstOrDefault();
        }


        /// <summary>
        /// EditConfigItemMetaData
        /// </summary>
        /// <returns>string</returns>
        public string EditConfigItemMetaData(bool UseSpecialChar = false)
        {
            Thread.Sleep(5000);
            OpenEditConfigItemMetaDataPopUpWindow();
            Thread.Sleep(8000);
            var ConfigItemMetaDataDetails = CreateControlDetails();
            if (UseSpecialChar)
            {
                ConfigItemMetaDataDetails[0].Value = ConfigItemMetaDataDetails[0].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
            }
            Helper.AssignValuesToControl(ConfigItemMetaDataDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return ConfigItemMetaDataDetails[0].Value;
        }

        /// <summary>
        /// EditDuplicateConfigItemMetaData
        /// </summary>
        public bool EditDuplicateConfigItemMetaData()
        {
            var ConfigItemMetaData = Helper.ValidateGridColumnDuplicateValue("myGrid", _driver, "Name");
            OpenEditConfigItemMetaDataPopUpWindow();
            var element = _driver.FindElement(By.Id("Name"), 20, true);
            element.Clear();
            element.SendKeys(ConfigItemMetaData[0].ToString());            
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            var expectedResult = string.Format("Configuration Metadata name : '{0}' already exists, please edit.", ConfigItemMetaData[0].ToString());
            Thread.Sleep(3000);
            return _driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Equals(expectedResult);
        }

        /// <summary>
        /// EditConfigurationMetadataWithDangerousRequest
        /// </summary>
        public bool EditConfigurationMetadataWithDangerousRequest()
        {
            var ConfigItemMetaDataName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            OpenEditConfigItemMetaDataPopUpWindow();
            Thread.Sleep(8000);
            List<ControlDetails> controlDetails = CreateControlDetails();
            controlDetails[1].Value = "</>";
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);

            if (_driver.FindElement(By.Id(Constants.ErrorMessageDateId)).Text.Contains("A potentially dangerous Request"))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Delete Config Item Metadata

        /// <summary>
        /// OpenDeleteConfigItemMetaDataPopUpWindow
        /// </summary>
        /// <returns>bool</returns>
        public bool OpenDeleteConfigItemMetaDataPopUpWindow()
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigItemGridXPath);
            if (count > 0)
            {
                Actions action = new Actions(_driver);

                IWebElement element = _driver.FindElement(By.Id("myGridRow0"), 20, true).FindElement(By.ClassName(Constants.DeleteButtonClass), 20, true);
                action.Click(element).Perform();
            }
            Thread.Sleep(5000);
            return Helper.IsTextPresent(Constants.DeleteTitle, _driver);
        }

        /// <summary>
        /// DeleteConfigItemMetadata
        /// </summary>
        public void DeleteConfigItemMetadata()
        {
            if (OpenDeleteConfigItemMetaDataPopUpWindow())
            {
                Actions action = new Actions(_driver);
                IWebElement element = _driver.FindElement(By.ClassName("ui-dialog-buttonset"), 20, true).FindElements(By.TagName("Button"))[0];
                action.Click(element).Perform();
            }
        }

        /// <summary>
        /// DeleteCancelConfigItemMetadata
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteCancelConfigItemMetadata()
        {
            if (OpenDeleteConfigItemMetaDataPopUpWindow())
            {
                Actions action = new Actions(_driver);
                IWebElement element = _driver.FindElement(By.ClassName("ui-dialog-buttonset"), 20, true).FindElements(By.TagName("Button"))[1];
                action.Click(element).Perform();
                return true;
            }
            return false;
        }


        #endregion
    }
}
