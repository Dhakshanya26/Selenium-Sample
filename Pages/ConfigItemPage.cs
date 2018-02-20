using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Linq;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;


namespace Serve.Platform.Configuration.UI.SeleniumTest.Pages
{
    /// <summary>
    /// ConfigItemPage
    /// </summary>
    public class ConfigItemPage
    {
        private static IWebDriver _driver;

        /// <summary>
        /// ConfigItemPage
        /// </summary>
        /// <param name="driver"></param>
        public ConfigItemPage(IWebDriver driver)
        {
            _driver = driver;
            Helper.GoToPage(Constants.ConfigItemLink, _driver, "ads%5Ccpt_testacc_1:CupAta6a@");
        }

        #region Methods for config item search screen

        /// <summary>
        /// LoadConfigItemSearchScreen
        /// </summary>
        /// <returns>bool</returns>
        public bool LoadConfigItemSearchScreen()
        {
            return Helper.IsTextPresent(Constants.SearchByName, _driver);
        }

        /// <summary>
        /// CheckAppNamespaceDropDownValues
        /// </summary>
        /// <returns>int</returns>
        public int CheckAppNamespaceDropDownValues()
        {
            return Helper.GetDropDownOptionCount(Constants.ConfigItemAppNameSpaceDllXPath, _driver);
        }

        /// <summary>
        /// CheckRecordsInConfigItemGrids
        /// </summary>
        /// <returns>int</returns>
        public int CheckRecordsInConfigItemGrids()
        {
            var totalPageCount = Convert.ToInt32(_driver.FindElement(By.Id(string.Format(Constants.GridTotalPageCount, Constants.GridId))).Text);
            return totalPageCount;
        }


        /// <summary>
        /// GetConfigItemForSelectedAppNamespace
        /// </summary>
        /// <returns>bool</returns>
        public bool GetConfigItemForSelectedAppNamespace()
        {
            //Add New configuration Item            
            var appNamespaceDropDown = new SelectElement(_driver.FindElement(By.XPath(Constants.ConfigItemAppNameSpaceDllXPath)));
            if ((appNamespaceDropDown.Options).Count > 1)
            {
                appNamespaceDropDown.SelectByIndex(1);
            }
            var selectedValue = appNamespaceDropDown.SelectedOption.GetAttribute("text");
            Thread.Sleep(3000);
            var configItem = Helper.ValidateGridColumnValue(Constants.GridId, _driver, "App Namespace", null);

            if (string.Equals(configItem[0], selectedValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// CheckShowInActiveConfigItemCheckBox
        /// </summary>
        /// <returns>bool</returns>
        public bool CheckShowInActiveConfigItemCheckBox(bool isActive = false)
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
        public bool SearchConfigurationItemByName()
        {
            var isRecordsExisting = false;

            var appNamespaceDropDown = new SelectElement(_driver.FindElement(By.Id("ddlSearchPattern")));
            if ((appNamespaceDropDown.Options).Count > 1)
            {
                appNamespaceDropDown.SelectByIndex(1);
            }
            _driver.FindElement(By.Id(Constants.SearchByNameTextBoxId)).SendKeys(string.Format("Con*{0}", OpenQA.Selenium.Keys.Enter));
            Thread.Sleep(2000);
            var result = Helper.ValidateGridColumnValue(Constants.GridId, _driver, "Name", null);
            result.ForEach(item =>
            {
                isRecordsExisting = item.Contains(Constants.NoRecordsFoundMessage);
            });
            return !isRecordsExisting;
        }


        /// <summary>
        /// SearchConfigItemByInvalidName
        /// </summary>
        /// <returns>bool</returns>
        public bool SearchConfigItemByInvalidName()
        {
            var isRecordsExisting = false;
            _driver.FindElement(By.Id(Constants.SearchByNameTextBoxId)).SendKeys(string.Format("{0}{1}",
                          Constants.InvalidItemName, OpenQA.Selenium.Keys.Enter));
            Thread.Sleep(3000);
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
        public void ClickTextInSearchByNameTextBox()
        {
            SearchConfigurationItemByName();
            IWebElement hiddenWebElement = _driver.FindElement(By.XPath(Constants.ClearImageIconXPath));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", hiddenWebElement);

        }

        /// <summary>
        /// ClickInActiveCheckBoxSearchByInactiveRecords
        /// </summary>
        /// <returns>string</returns>
        public string ClickInActiveCheckBoxSearchByInactiveRecords()
        {
            Random random = new Random();
            ClickAddNewConfigItemButton();
            var name = string.Format("ConfigItemTestCase{0}", random.Next(1000));
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Config.Test.Dev" });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
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
        /// ClickAddNewConfigItemButton
        /// </summary>
        /// <returns>bool</returns>
        public bool ClickAddNewConfigItemButton()
        {
            Helper.SaveButtonClickEvent(Constants.AddNewButtonId, _driver);
            return Helper.IsTextPresent(Constants.AddNewConfigItemTitle, _driver);
        }


        /// <summary>
        /// AddNewConfigItem
        /// </summary>
        /// <returns>string</returns>
        public string AddNewConfigItem()
        {
            Random random = new Random();
            ClickAddNewConfigItemButton();
            var configItemName = string.Format("ConfigItemTestCase{0}", random.Next(1000));
            Helper.AssignValuesToControl(CreateControlDetails(configItemName), _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return configItemName;
        }

        /// <summary>
        /// AddNewConfigItem
        /// </summary>
        /// <returns>string</returns>
        public string AddNewConfigItemWithSpecialChar()
        {
            Random random = new Random();
            ClickAddNewConfigItemButton();
            var configItemName = string.Format("ConfigItemTestCase{0}", random.Next(1000)) + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
            Helper.AssignValuesToControl(CreateControlDetails(configItemName), _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return configItemName;
        }

        /// <summary>
        /// CreateControlDetails
        /// </summary>
        /// <param name="configItemName"></param>
        /// <returns>ControlDetails list</returns>
        private List<ControlDetails> CreateControlDetails(string configItemName = null)
        {
            Random random = new Random();
            var name = string.IsNullOrEmpty(configItemName) ? string.Format("ConfigItemTestCase{0}", random.Next(1000)) : configItemName;
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = name });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Config.Test.Dev" });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            return controlDetails;
        }


        /// <summary>
        /// AddDuplicateConfigurationItem
        /// </summary>
        public bool AddDuplicateConfigurationItem()
        {
            var configItemName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            ClickAddNewConfigItemButton();
            var controlDetails = CreateControlDetails(configItemName[0]);
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);

            var expectedResult = string.Format("Configuration item name : '{0}' already exists, please edit.", configItemName[0]);
            var errorMessageElement = _driver.FindElement(By.Id(Constants.ErrorMessageDateId));
            if (errorMessageElement.Text.Equals(expectedResult))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// AddConfigurationItemDangerousRequest
        /// </summary>
        public bool AddConfigurationItemDangerousRequest()
        {
            var configItemName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            ClickAddNewConfigItemButton();
            List<ControlDetails> controlDetails = CreateControlDetails(configItemName[0].ToString());
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
            ClickAddNewConfigItemButton();
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
        /// AddConfigItemWithAvailableDateGreaterThanExpiryDate
        /// </summary>
        /// <returns>bool</returns>
        public bool AddConfigItemWithAvailableDateGreaterThanExpiryDate()
        {
            ClickAddNewConfigItemButton();
            var controlDetails = CreateControlDetails();
            Helper.AssignValuesToControl(controlDetails, _driver);

            SetCreateScreenAvailableAndExpiresOnDate(5, 2, 5, 1);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            const string expectedResult = "'Expires On' date should be greater than or equal to'Available From' date";
            var errorMessageElement = _driver.FindElement(By.Id(Constants.ErrorMessageDateId));
            if (errorMessageElement.Text.Equals(expectedResult))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// AddConfigurationItemWithInValidInternalType
        /// </summary>
        /// <returns>bool</returns>
        public bool AddConfigurationItemWithInValidInternalType()
        {
            ClickAddNewConfigItemButton();
            Random random = new Random();
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Config.Test.Dev" });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "Bool" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            var expectedResult = "Please enter a valid 'Bool' value";
            Thread.Sleep(2000);
            if (_driver.FindElement(By.Id(Constants.ErrorMessageDateId), 10, true).Text.Equals(expectedResult))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// AddConfigurationItemWithIsSecurePopUpDisplay
        /// </summary>
        /// <returns>bool</returns>
        public bool AddConfigurationItemWithIsSecurePopUpDisplay()
        {
            ClickAddNewConfigItemButton();
            Random random = new Random();
            List<ControlDetails> controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Config.Test.Dev" });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsSecureId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            return Helper.IsTextPresent(Constants.IsSecurePopUpContent, _driver);
        }

        /// <summary>
        /// AddConfigurationItemWithIsSecurePopUpDisplay_Click_OkButton
        /// </summary>
        /// <returns>string</returns>
        public string AddConfigurationItemWithIsSecurePopUpDisplay_Click_OkButton()
        {
            ClickAddNewConfigItemButton();
            var random = new Random();            
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = Constants.ConfigTestAppNamespaceName });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsSecureId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            var action = new Actions(_driver);
            var element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[0];
            action.Click(element).Perform();
            return controlDetails[0].Value.ToString();
        }

        /// <summary>
        /// AddConfigurationItemWithIsSecurePopUpDisplay_Click_CancelButton
        /// </summary>
        /// <returns>bool</returns>
        public bool AddConfigurationItemWithIsSecurePopUpDisplay_Click_CancelButton()
        {
            ClickAddNewConfigItemButton();
            var random = new Random();
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = Constants.ConfigTestAppNamespaceName });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsSecureId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            var action = new Actions(_driver);
            var element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[1];
            action.Click(element).Perform();
            return Helper.IsTextPresent(Constants.AddNewConfigItemTitle, _driver);
        }



        #endregion

        #region Edit Config Item


        /// <summary>
        /// OpenEditConfigAppNamespacePopUpWindow
        /// </summary>
        /// <returns>bool</returns>
        public bool OpenEditConfigItemPopUpWindow()
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigItemGridXPath);
            if (count > 0)
            {
                var action = new Actions(_driver);
                var element = _driver.FindElement(By.Id("myGridRow0"));
                //Double click
                action.DoubleClick(element).Perform();
                Thread.Sleep(3000);
            }
            return Helper.IsTextPresent(Constants.AddNewConfigItemTitle, _driver);
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
        /// ValidateRequiredField
        /// </summary>
        public void ValidateRequiredFieldForEditing(string controlName = null, string value = "")
        {
            OpenEditConfigItemPopUpWindow();
            var controlDetails = CreateControlDetails();
            controlDetails.ForEach(item =>
            {
                if (string.Equals(item.Name, controlName, StringComparison.OrdinalIgnoreCase))
                {
                    item.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                }

            });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Thread.Sleep(1000);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
        }

        /// <summary>
        /// EditConfigItem
        /// </summary>
        /// <returns>string</returns>
        public string EditConfigItem(bool UseSpecialChar = false)
        {
            Thread.Sleep(8000);
            OpenEditConfigItemPopUpWindow();
            var configItemDetails = CreateControlDetails();
            if (UseSpecialChar)
            {
                configItemDetails[0].Value = configItemDetails[0].Value + @"~!@#$%^&*()_+`-={}|[]\:;',./<>?";
            }
            Helper.AssignValuesToControl(configItemDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return configItemDetails[0].Value;
        }

        /// <summary>
        /// EditDuplicateConfigItem
        /// </summary>
        public bool EditDuplicateConfigItem()
        {
            const string expectedResult = "An exception has occurred";
            var configItem = Helper.ValidateGridColumnDuplicateValue("myGrid", _driver, "Name");
            var appnamepace = Helper.ValidateGridColumnDuplicateValue("myGrid", _driver, "App Namespace");
            AddNewConfigItem();
            OpenEditConfigItemPopUpWindow();
            _driver.FindElement(By.Id("Name")).Clear();
            _driver.FindElement(By.Id("Name")).SendKeys(configItem[0]);
            var dropdown = new SelectElement(_driver.FindElement(By.Id("AppNamespaceId"), 20, true));
            dropdown.SelectByText(appnamepace[0]);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            return _driver.FindElement(By.Id(Constants.ErrorMessageDateId), 20, true).Text.Contains(expectedResult);
        }

        /// <summary>
        /// EditConfigItemDangerousRequest
        /// </summary>
        public bool EditConfigItemDangerousRequest()
        {
            var ConfigItemMetaDataName = Helper.ValidateGridColumnValue("myGrid", _driver, "Name");
            OpenEditConfigItemPopUpWindow();
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
        public bool OpenDeleteConfigItemPopUpWindow()
        {
            var count = GetNumberOfRowsInATable(Constants.ConfigItemGridXPath);
            if (count > 0)
            {
                Actions action = new Actions(_driver);

                IWebElement element = _driver.FindElement(By.Id("myGridRow0")).FindElement(By.ClassName(Constants.DeleteButtonClass));
                action.Click(element).Perform();
            }
            return Helper.IsTextPresent(Constants.DeleteTitle, _driver);
        }

        /// <summary>
        /// DeleteConfigItem
        /// </summary>
        public void DeleteConfigItem()
        {
            if (OpenDeleteConfigItemPopUpWindow())
            {
                Actions action = new Actions(_driver);
                IWebElement element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[0];
                action.Click(element).Perform();

            }
        }

        /// <summary>
        /// DeleteCancelConfigItem
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteCancelConfigItem()
        {
            if (OpenDeleteConfigItemPopUpWindow())
            {
                Actions action = new Actions(_driver);
                IWebElement element = _driver.FindElement(By.ClassName("ui-dialog-buttonset")).FindElements(By.TagName("Button"))[1];
                action.Click(element).Perform();
                return true;
            }
            return false;
        }


        #endregion

        /// <summary>
        /// CheckVersionNumberAddConfigItem
        /// </summary>
        /// <param name="isSecured"></param>
        /// <returns>bool</returns>
        public bool CheckVersionNumberAddConfigItem(bool isSecured = true)
        {
            AddNewConfigItem();
            OpenEditConfigItemPopUpWindow();
            return Helper.IsTextPresent("Version Number", _driver) && (!_driver.FindElement(By.Id("IsSecure")).Selected);

        }

        /// <summary>
        /// CheckVersionNumberEditConfigItem
        /// </summary>
        public void CheckVersionNumberEditConfigItem()
        {
            var configItemVersionNumber = Helper.ValidateGridColumnDuplicateValue("myGrid", _driver, "Version #");
            OpenEditConfigItemPopUpWindow();

            if (Helper.IsTextPresent("Version Number", _driver))
            {
                IWebElement versionNumberSelected = _driver.FindElement(By.Id("IsVersionRequired"));
                versionNumberSelected.Click();
                Helper.SaveButtonClickEvent("modalSubmit", _driver);


            }
        }

        /// <summary>
        /// CheckSecuredConfigItemLockIcon
        /// </summary>
        /// <returns>bool</returns>
        public bool CheckSecuredConfigItemLockIcon(bool isSecured = false)
        {
            try
            {
                if (isSecured)
                {
                    AddConfigurationItemWithIsSecurePopUpDisplay_Click_OkButton();
                }
                else
                {
                    AddNewConfigItem();
                }
                Thread.Sleep(2000);
                if (_driver.FindElement(By.XPath("//tr[@id='myGridRow0']/td[2]/h5/a/span")) != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Set available date and expires on date for edit screen
        /// </summary>
        public void SetEditScreenAvailableAndExpiresOnDate()
        {
            var availTimes = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[10]/div[2]/div/span/span"));
            availTimes.Click();
            var availDate = Driver.Instance.FindElement(By.XPath(".//div[3]/table/tbody/tr[6]/td[2]"));
            availDate.Click();
            var availTime = Driver.Instance.FindElement(By.XPath("//div[2]/table/tbody/tr/td/span[18]"));
            availTime.Click();
            var availTimePerMinute = Driver.Instance.FindElement(By.XPath("//span[23]"));
            availTimePerMinute.Click();
            var timeExpires = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[11]/div[2]/div/span/span"));
            timeExpires.Click();
            var expires = Driver.Instance.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[6]/td[2]"));
            expires.Click();
            var expiresValue = Driver.Instance.FindElement(By.XPath("//div[3]/div[2]/table/tbody/tr/td/span[18]"));
            expiresValue.Click();
            var expiresPerMinute = Driver.Instance.FindElement(By.XPath("//div[3]/div/table/tbody/tr/td/span[54]"));
            expiresPerMinute.Click();
        }

        /// <summary>
        /// Edit configItem with Available From and ExpiresOn validation
        /// </summary>
        /// <param name="availableFrom"></param>
        /// <param name="expiresOnDate"></param>
        /// <param name="isSelectedIncreasedVersion"></param>
        /// <param name="increasedVersionIndexNumber"></param>
        /// <param name="textToBePresent"></param>
        /// <param name="checkIncreasedVersionValidation"></param>
        /// <returns>bool</returns>
        public bool EditConfigItemWithAvailableFromAndExpiresOnValidation(DateTime availableFrom,
                      DateTime expiresOnDate, bool isSelectedIncreasedVersion = false, int increasedVersionIndexNumber = 1,
            string textToBePresent = null, string checkIncreasedVersionValidation = null)
        {
            Helper.SaveButtonClickEvent(Constants.AddNewButtonId, _driver);
            var random = new Random();
            //Create New Config Items
            SetCreateScreenAvailableAndExpiresOnDate(6, 1, 6, 1);
            var controlDetails = new List<ControlDetails>();
            controlDetails.Add(new ControlDetails { Name = "Name", Type = ControlType.TextBox, Value = string.Format("ConfigItemTT{0}", random.Next(10000)) });
            controlDetails.Add(new ControlDetails { Name = "Value", Type = ControlType.TextBox, Value = string.Format("ConfigItemTestCase{0}", random.Next(1000)) });
            controlDetails.Add(new ControlDetails { Name = "AppNamespaceId", Type = ControlType.DropDownList, Value = "Serve.Admin" });
            controlDetails.Add(new ControlDetails { Name = "Type", Type = ControlType.DropDownList, Value = "String" });
            controlDetails.Add(new ControlDetails { Name = Constants.IsEnabledCheckBoxId, Type = ControlType.CheckBox, Value = "true" });
            Helper.AssignValuesToControl(controlDetails, _driver);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            
            Thread.Sleep(2000);

            //Update existing config items
            OpenEditConfigItemPopUpWindow();
            SetEditScreenAvailableAndExpiresOnDate();  
            if (!string.IsNullOrEmpty(checkIncreasedVersionValidation))
            {
                controlDetails = new List<ControlDetails>();
                controlDetails.Add(new ControlDetails
                {
                    Name = "IsVersionRequired",
                    Type = ControlType.CheckBox,
                    Value = checkIncreasedVersionValidation
                });
                Helper.AssignValuesToControl(controlDetails, _driver);
                var result = _driver.FindElement(By.Id("vesrionchange")).Displayed;
                return !result;
            }
          
            controlDetails = new List<ControlDetails>();
           if (isSelectedIncreasedVersion)
            {
                controlDetails.Add(new ControlDetails
                {
                    Name = "IsVersionRequired",
                    Type = ControlType.CheckBox,
                    Value = "True"
                });
            }
            Helper.AssignValuesToControl(controlDetails, _driver);
            Thread.Sleep(2000);
            Helper.SaveButtonClickEvent("modalSubmit", _driver);
            Thread.Sleep(3000);
            return Helper.IsTextPresent(textToBePresent, _driver);
        }

        private void SetCreateScreenAvailableAndExpiresOnDate(int availableDateRow = 4,
            int availableDateColumn = 5 ,int expiresOnDateRow = 4,int expiresOnDateColumn = 2)
        {
            var availTimes = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[8]/div[2]/div/span/span"));
            availTimes.Click();
            var availDate = Driver.Instance.FindElement(By.XPath(string.Format("//div[3]/table/tbody/tr[{0}]/td[{1}]", availableDateRow, availableDateColumn)));
            availDate.Click();
            var availTime = Driver.Instance.FindElement(By.XPath("//div[2]/table/tbody/tr/td/span[18]"));
            availTime.Click();
            var availTimePerMinute = Driver.Instance.FindElement(By.XPath("//span[23]"));
            availTimePerMinute.Click();
            var timeExpires = Driver.Instance.FindElement(By.XPath("//*[@id='addConfig-form']/div[9]/div[2]/div/span/span"));
            timeExpires.Click();
            var expires = Driver.Instance.FindElement(By.XPath(string.Format("//div[3]/div[3]/table/tbody/tr[{0}]/td[{1}]", expiresOnDateRow, expiresOnDateColumn)));
            expires.Click();
            var expiresValue = Driver.Instance.FindElement(By.XPath("//div[3]/div[2]/table/tbody/tr/td/span[18]"));
            expiresValue.Click();
            var expiresPerMinute = Driver.Instance.FindElement(By.XPath("//div[3]/div/table/tbody/tr/td/span[54]"));
            expiresPerMinute.Click();
        }
       
    }
}
