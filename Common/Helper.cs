using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase;

namespace Serve.Platform.Configuration.UI.SeleniumTest.Common
{
    public static class Helper
    {
        private const string CheckTitle = "certificate error: navigation blocked";

        /// <summary>
        /// SelectGridItem
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <param name="action"></param>
        public static void SelectGridItem(string gridDivId, IWebDriver driver, Actions action = null)
        {
            if (action == null)
            {
                action = new Actions(driver);
            }

            var element = driver.FindElement(By.XPath(string.Format("//div[@id='{0}']//table[1]/tbody/tr/td[1]/label[1][.='ApprovalSubject']", gridDivId)));
            action.MoveToElement(element).DoubleClick().Build().Perform();

        }

        /// <summary>
        /// FindElement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <param name="displayed"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this ISearchContext context, By by, uint timeout, bool displayed = false)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = TimeSpan.FromSeconds(timeout);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            IWebElement returnElement = null;
            try
            {
                returnElement = wait.Until(ctx =>
                {
                    var elem = ctx.FindElement(by);
                    if (displayed && !elem.Displayed)
                        return null;

                    return elem;
                });
            }
            catch (Exception ex)
            {
                //
            }
            return returnElement;
        }

        /// <summary>
        /// SelectGridItemByValue
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IWebElement SelectGridItemByValue(string gridDivId, IWebDriver driver, string columnName, string value)
        {
            IWebElement element = null;
            var columns = BuildTable(gridDivId, driver);
            int columnIndex = columns.IndexOf(columnName) + 1;
            if (columnIndex >= 1)
            {
                var sw = new Stopwatch();
                sw.Start();

                string s = string.Format(Constants.GridFirstPage, gridDivId);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

                var totalPageCount =
                    Convert.ToInt32(
                        driver.FindElement(By.Id(string.Format(Constants.GridTotalPageCount, gridDivId))).Text);
                var nextPageLink = driver.FindElement(By.Id(string.Format(Constants.GridPageNext, gridDivId)));

                for (int i = 1; i <= totalPageCount; i++)
                {

                    //var column = driver.FindElements(By.XPath(string.Format("//div[@id='{0}']//table[1]/tbody/tr/td[{1}]/label[.='{2}']", gridDivId, columnIndex, value)));
                    var column = driver.FindElement(
                        By.XPath(string.Format("//div[@id='{0}']//table[1]/tbody/tr/td[{1}]/label[.='{2}']",
                                               gridDivId,
                                               columnIndex, value)), 2, true);
                    if (column != null)
                    {
                        sw.Stop();
                        var elapsed = sw.Elapsed;
                        element = column;
                        break;
                    }
                    else
                    {
                        nextPageLink = driver.FindElement(By.Id(string.Format(Constants.GridPageNext, gridDivId)), 0, true);
                        nextPageLink.Click();
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
                    }
                }
            }
            else
            {
                //TODO: Throw exception

            }
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            //var element = driver.FindElement(By.XPath(string.Format("//div[@id='{0}']//table[1]/tbody/tr/td[1]", gridDivId)));

            return element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> BuildTable(string gridDivId, IWebDriver driver)
        {
            // Thread.Sleep(2000);
            var waitForTable = (new WebDriverWait(driver, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementExists(By.Id(gridDivId)));

            return waitForTable.FindElements(By.XPath(string.Format("//div[@id='{0}']//table/tbody/tr/th", gridDivId))).Select(x => x.Text).ToList();
        }

        #region Common methods

        /// <summary>
        /// GoToPage
        /// </summary>
        /// <param name="linkButtonText"></param>
        /// <param name="driver"></param>
        public static void GoToPage(string linkButtonText, IWebDriver driver, string userPass = "")
        {
            if (!string.IsNullOrEmpty(userPass))
            {
                driver.Navigate().GoToUrl(string.Format("https://{0}configmanagerf14.serve.com", userPass));
            }
            else
            {
                driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["ConfigUIBaseUrl"]);
            }



            var action = new Actions(driver);

            if (string.Equals(driver.Title, CheckTitle, StringComparison.OrdinalIgnoreCase) ||
                                                       driver.Title.ToLower().Contains(CheckTitle) ||
                                                        CheckTitle.Contains(driver.Title.ToLower()))
            {
                driver.Navigate().GoToUrl("javascript:document.getElementById('overridelink').click();");
            }

            if (System.Configuration.ConfigurationManager.AppSettings["ManualLogin"] == "true")
            {
                ManualLogin(driver);
            }

            foreach (var item in linkButtonText.Split('/'))
            {
                Thread.Sleep(2000);
                driver.FindElement(By.LinkText(item)).Click();
            }
        }

        /// <summary>
        /// ManualLogin
        /// </summary>
        /// <param name="driver"></param>
        public static void ManualLogin(IWebDriver driver)
        {
            Thread.Sleep(1000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(System.Configuration.ConfigurationManager.AppSettings["AdsUser"]);
            Thread.Sleep(1000);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(2000);
            SendKeys.SendWait(System.Configuration.ConfigurationManager.AppSettings["AdsPassword"]);
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(2000);
        }

        /// <summary>
        /// IsTextPresent
        /// </summary>
        /// <param name="textToBeVerified"></param>
        /// <param name="driver"></param>
        /// <returns>bool</returns>
        public static bool IsTextPresent(string textToBeVerified, IWebDriver driver)
        {
            if (driver.FindElement(By.XPath("//*[contains(.,'" + textToBeVerified + "')]")) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// GetDropDownOptionCount
        /// </summary>
        /// <param name="dropDownXPath"></param>
        /// <param name="driver"></param>
        /// <returns>int</returns>
        public static int GetDropDownOptionCount(string dropDownXPath, IWebDriver driver)
        {
            var dropDown = driver.FindElement(By.XPath(dropDownXPath));
            var selectElement = new SelectElement(dropDown);
            var values = selectElement.Options;
            return values.Count;
        }


        public static string GetNotificationText()
        {
            //TODO: Find a better way to wait until the loading...icon goes away
            Thread.Sleep(3000);
            return Driver.Instance.FindElement(By.Id("notification"), 20, true).Text.ToLower();
        }

        /// <summary>
        /// ValidateGridColumnValue
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="action"></param>
        /// <returns>string list</returns>
        public static List<string> ValidateGridColumnValue(string gridDivId, IWebDriver driver, string columnName = null, Actions action = null, bool IsDuplicate = false)
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

        /// <summary>
        /// ValidateGridColumnDuplicateValue
        /// </summary>
        /// <param name="gridDivId"></param>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="action"></param>
        /// <returns>string list</returns>
        public static List<string> ValidateGridColumnDuplicateValue(string gridDivId, IWebDriver driver, string columnName = null, Actions action = null, bool IsDuplicate = false)
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
                    var row = waitForTable.FindElements(By.XPath(string.Format("//div[@id='{0}']//table/tbody/tr[6]/td[{1}]", gridDivId, columnIndex)));
                    result = row.Where(x => !string.IsNullOrEmpty(x.Text)).Select(x => x.Text).ToList();
                }
            }
            return result;
        }

        /// <summary>
        /// SaveButtonClickEvent
        /// </summary>
        /// <param name="driver"></param>
        public static void SaveButtonClickEvent(string controlId, IWebDriver driver)
        {
            Thread.Sleep(2000);
            var submitButton = driver.FindElement(By.Id(controlId), 20, true);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", submitButton);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// AssignValuesToControl
        /// </summary>
        /// <param name="controlDetails"></param>
        /// <param name="driver"></param>
        public static void AssignValuesToControl(List<ControlDetails> controlDetails, IWebDriver driver)
        {
            foreach (var item in controlDetails)
            {
                if (item.Type == ControlType.TextBox || item.Type == ControlType.DateTime)
                {
                    var element = driver.FindElement(By.Id(item.Name), 20, true);
                    element.Clear();
                    element.SendKeys(item.Value);
                    //driver.FindElement(By.Id(item.Name),20,true).Clear();
                    //driver.FindElement(By.Id(item.Name)).SendKeys(item.Value);
                }
                else if (item.Type == ControlType.DropDownList && (!string.IsNullOrEmpty(item.Value)))
                {
                    var dropdown = new SelectElement(driver.FindElement(By.Id(item.Name), 20, true));
                    dropdown.SelectByText(item.Value);
                }
                else if (item.Type == ControlType.CheckBox && (!string.IsNullOrEmpty(item.Value)))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('" + item.Name + "').checked='" + item.Value + "'");
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// EnterKey
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="driver"></param>
        public static void EnterKey(string controlName, IWebDriver driver)
        {
            driver.FindElement(By.Id(controlName), 20, true).SendKeys(OpenQA.Selenium.Keys.Enter);
        }

        /// <summary>
        /// SaveButtonClickEvent
        /// </summary>
        /// <param name="driver"></param>
        public static void ButtonClickEvent(string controlId, IWebDriver driver)
        {
            Thread.Sleep(2000);
            var submitButton = driver.FindElement(By.Id(controlId), 20, true);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", submitButton);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Grid Double Click event
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="rowNumber"></param>
        /// <param name="driver"></param>
        public static void GridDoubleClick(string gridName, int rowNumber, IWebDriver driver)
        {
            if (GetGridRecordCount(gridName, driver) > 0)
            {
                DoubleClick(string.Format(Constants.GridRowName, rowNumber - 1), driver);
            }
        }

        /// <summary>
        /// Get grid record count
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static int GetGridRecordCount(string gridName, IWebDriver driver)
        {
            var table = driver.FindElements(By.XPath(string.Format(Constants.GridXPath, gridName)));
            return table
                  .Select(webElement => webElement.FindElements(By.XPath("//tr")))
                      .Select(rows => rows.Count)
                      .FirstOrDefault();
        }

        /// <summary>
        /// Double click event
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="driver"></param>
        private static void DoubleClick(string controlName, IWebDriver driver)
        {
            Thread.Sleep(2000);
            var action = new Actions(driver);
            var element = driver.FindElement(By.Id(controlName), 20, true);
            action.DoubleClick(element).Perform();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Grid Navigation events
        /// </summary>
        /// <param name="navigateType"></param>
        /// <param name="driver"></param>
        public static void GridNavigationEvent(NavigateType navigateType, IWebDriver driver, string gridName = "myGrid")
        {
            switch (navigateType)
            {
                case NavigateType.First:
                    if (IsElementPresent(By.Id(Constants.GridFirst + gridName)))
                        ButtonClickEvent(Constants.GridFirst + gridName, driver);
                    break;
                case NavigateType.Last:
                    if (IsElementPresent(By.Id(Constants.GridLast + gridName)))
                        ButtonClickEvent(Constants.GridLast + gridName, driver);
                    break;
                case NavigateType.Previous:
                    if (IsElementPresent(By.Id(Constants.GridPrevious + gridName)))
                        ButtonClickEvent(Constants.GridPrevious + gridName, driver);
                    break;
                case NavigateType.Next:
                    if (IsElementPresent(By.Id(Constants.GridNext + gridName)))
                        ButtonClickEvent(Constants.GridNext + gridName, driver);
                    break;
                default:
                    break;
            }
        }

        public static void GoToNavigation(int pagenumber, IWebDriver driver, string gridId = "myGrid")
        {
            if (IsElementPresent(By.Id(Constants.GridGoToText+gridId)))
            {
                var element = driver.FindElement(By.Id(Constants.GridGoToText + gridId), 20, true);
                element.Clear();
                element.SendKeys(pagenumber.ToString());
                ButtonClickEvent(Constants.GridGo + gridId, driver);
            }
        }

        /// <summary>
        /// Checking If An Element Is Present With WebDriver
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns>bool</returns>
        public static bool IsElementPresent(By locator)
        {
            ////Set the timeout to something low            
            ////Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(100));

            //try
            //{
            //    Driver.Instance.FindElement(locator, 20, true);
            //    //If element is found set the timeout back and return true

            //    //Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            //    return true;
            //}
            //catch (NoSuchElementException)
            //{
            //    //If element isn't found, set the timeout and return false
            //    //Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            //    return false;
            //}
            //Set the timeout to something low            
            //Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(100));

            try
            {
                Driver.Instance.FindElement(locator);
                //If element is found set the timeout back and return true
                Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                return true;
            }
            catch (NoSuchElementException)
            {
                //If element isn't found, set the timeout and return false
                Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                return false;
            }
        }

        /// <summary>
        /// Get the grid cell values
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="rowNumber"></param>
        /// <param name="cellNumber"></param>
        /// <param name="driver"></param>
        /// <returns>string</returns>
        public static string GetgridCellVaule(string gridName, int rowNumber, int cellNumber)
        {
            var tdlist = Driver.Instance.FindElements(By.CssSelector("table[id='" + gridName + "'] tbody tr"));
            return tdlist[rowNumber + 2].FindElements(By.TagName("td"))[cellNumber - 1].Text;
        }

        /// <summary>
        /// Grid Row select
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="rowNumber"></param>
        /// <param name="cellNumber"></param>
        /// <returns></returns>
        public static void GridRowSelect(string gridName, int rowNumber, int cellNumber)
        {
            var tdlist = Driver.Instance.FindElements(By.CssSelector("table[id='" + gridName + "'] tbody tr"));
            tdlist[rowNumber + 2].FindElements(By.TagName("td"))[cellNumber - 1].Click();
        }

        /// <summary>
        /// Get checkbox status in grid row
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="rowNumber"></param>
        /// <param name="driver"></param>
        /// <returns>bool</returns>
        public static bool IsGridCheckBoxEnabled(string gridName, int rowNumber)
        {
            var tdlist = Driver.Instance.FindElements(By.CssSelector("table[id='" + gridName + "'] tbody tr"));
            foreach (var item in tdlist)
            {
                return item.FindElements(By.XPath("//input[@type='checkbox']"))[rowNumber].Selected;
            }
            return true;
        }

        /// <summary>
        /// Get current grid page number
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>int</returns>
        public static int GetGridCurrentPage(IWebDriver driver, string gridName = "myGrid")
        {
            if (IsElementPresent(By.Id(Constants.GridPrevious + gridName)))
                return int.Parse(driver.FindElement(By.Id("superGridCurrentPage" + gridName), 20, true).Text);
            return -1;
        }

        /// <summary>
        /// Get Total page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>int</returns>
        public static int GetGridTotalPageCount(IWebDriver driver, string gridName = "myGrid")
        {
            if (IsElementPresent(By.Id(Constants.GridPrevious + gridName)))
                return int.Parse(driver.FindElement(By.Id("superGridPageTotalCount"+gridName), 20, true).Text);
            return -1;
        }

        #endregion

        #region Sorting

        /// <summary>
        /// SortByColumn
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="gridDivId"></param>
        /// <returns>bool</returns>
        public static bool SortByColumn(IWebDriver driver, string sortBy, System.Windows.Forms.SortOrder sortOrder, string gridDivId = Constants.GridId)
        {
            var actualResult = new List<string>();
            var expectedResult = new List<string>();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnName = BuildTable(gridDivId, driver);
                if (columnName.IndexOf(sortBy) > -1)
                {
                    var element = driver.FindElement(By.LinkText(sortBy), 20, true);
                    ExecuteJavaScriptClickEvent(element, driver);
                    if (sortOrder == System.Windows.Forms.SortOrder.Descending)
                    {
                        var elementDesc = driver.FindElement(By.LinkText(sortBy), 20, true);
                        ExecuteJavaScriptClickEvent(elementDesc, driver); //To sort by descending, click the link button twice. 
                    }
                    actualResult = ValidateGridColumnValue(gridDivId, driver, sortBy).Where(item => !(item.Contains("Go to page"))).ToList();
                    actualResult = AppendValueForSorting(actualResult, sortOrder == SortOrder.Ascending);
                    expectedResult = (sortOrder == SortOrder.Descending) ? actualResult.OrderByDescending(item => item).ToList() :
                    actualResult.OrderBy(item => item).ToList();
                }
                else
                {
                    return false;
                }
                return actualResult.SequenceEqual(expectedResult);
            }
            return false;
        }

        /// <summary>
        /// Append value for sorting
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        private static List<string> AppendValueForSorting(List<string> listData, bool orderBy)
        {
            var result = new List<string>();
            string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int j = listData.Count + 1;
            for (int i = 0; i < listData.Count; i++)
            {
                if (orderBy)
                    result.Add(string.Format("{0}{1}", alphabetArray[i], listData[i]));
                else
                {
                    result.Add(string.Format("{0}{1}", alphabetArray[j--], listData[i]));
                }
            }

            return result;
        }

        /// <summary> 
        /// ExecuteJavaScriptClickEvent 
        /// </summary> 
        /// <param name="element"></param> 
        public static void ExecuteJavaScriptClickEvent(IWebElement element, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", element);
            Thread.Sleep(10000);
        }

        /// <summary> 
        /// Link button click event 
        /// </summary> 
        /// <param name="element"></param> 
        public static void LinkButtonClick(string linkButtonText, IWebDriver driver)
        {
            foreach (var item in linkButtonText.Split('/'))
            {
                driver.FindElement(By.LinkText(item), 20, true).Click();
            }
        }

        #endregion

        /// <summary>
        /// Grid Link button click
        /// </summary>
        /// <param name="gridid"></param>
        /// <param name="rowNumber"></param>
        /// <param name="cellNumber"></param>
        public static void GridLinkButtonClick(string gridid, int rowNumber, int cellNumber, IWebDriver driver)
        {
            var tdlist = driver.FindElements(By.CssSelector("table[id='" + gridid + "'] tbody tr"));
            var rowElements = tdlist[rowNumber + 2].FindElements(By.TagName("td"))[cellNumber - 1];
            var link = rowElements.FindElement(By.XPath("a"));
            link.Click();
        }

        /// <summary>
        /// Super grid filter by column name
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="filterByTest"></param>
        /// <param name="isClearFilter"></param>
        /// <param name="isDateFilter"></param>
        /// <param name="gridDivId"></param>
        /// <returns>bool</returns>
        public static bool FilterByColumn(IWebDriver driver,
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
                var textbox = driver.FindElement(By.Id(string.Format("txtSgFltrmyGrid{0}", columnName)));
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
        /// Super grid filter by column name for Disabled Config Item 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="columnName"></param>
        /// <param name="filterByTest"></param>
        /// <param name="isClearFilter"></param>
        /// <param name="isDateFilter"></param>
        /// <param name="gridDivId"></param>
        /// <returns>bool</returns>
        public static bool FilterByColumnConfigItem(IWebDriver driver,
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
                var textbox = driver.FindElement(By.Id(string.Format("txtSgFltrmyGrid{0}", columnName.Replace(" ",""))));
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
            driver.FindElement(By.XPath("//td[5]/div/div")).Click();
            var record = ValidateGridColumnValue(gridDivId, driver, columnName).FirstOrDefault();

            //To convert output string to MM/dd/yyyy format date string
            if (isDateFilter)
            {
                record = Convert.ToDateTime(record).ToString("MM/dd/yyyy");
            }

            //If record is null then return true . else if the record has value then compare with input.
            return record == null || (record.IndexOf(filterByTest, StringComparison.Ordinal) != -1);

        }


    }
}
