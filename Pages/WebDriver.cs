using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;


namespace Serve.Platform.Configuration.UI.SeleniumTest.Pages
{
    public class WebDriver
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Constructor for initialize the object
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="browser"></param>
        public WebDriver(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["ConfigUIBaseUrl"]);
        }

        /// <summary>
        /// Event for link button click
        /// </summary>
        /// <param name="linkButtonText"></param>
        public void ClickLinkText(string linkButtonText)
        {
            foreach (var item in linkButtonText.Split('/'))
            {
                Thread.Sleep(4000);
                _driver.FindElement(By.LinkText(item)).Click();
                Thread.Sleep(4000);
            }
        }

        /// <summary>
        /// Event for submit button
        /// </summary>
        /// <param name="buttonName"></param>
        public void ClickSubmitButton(string buttonName)
        {
            Thread.Sleep(4000);
            var submitButton = _driver.FindElement(By.Id(buttonName));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", submitButton);
            Thread.Sleep(4000);
        }

        /// <summary>
        /// Validate required field and assign the value
        /// </summary>
        /// <param name="fieldNamesWithValues"></param>
        /// <returns>bool</returns>
        public bool RequiredFieldsValidation(List<ControlDetails> controlDetails)
        {
            try
            {
                foreach (var item in controlDetails)
                {
                    Thread.Sleep(4000);
                    if (!AssignFieldvalue(item))
                        return false;
                    Thread.Sleep(4000);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Assign the control value
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="value">bool</param>
        private bool AssignFieldvalue(ControlDetails controlDetails)
        {
            bool controlResponse = false;
            try
            {
                if (!string.IsNullOrEmpty(controlDetails.Value))
                {
                    if (controlDetails.Type == ControlType.TextBox || controlDetails.Type == ControlType.DateTime)
                    {
                        _driver.FindElement(By.Id(controlDetails.Name)).SendKeys(controlDetails.Value);
                        controlResponse = true;
                    }
                    else if (controlDetails.Type == ControlType.DropDownList)
                    {
                        var dropdown = new SelectElement(_driver.FindElement(By.Id(controlDetails.Name)));
                        dropdown.SelectByText(controlDetails.Value);
                        controlResponse = true;
                    }
                    else if (controlDetails.Type == ControlType.CheckBox)
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("document.getElementById('IsEnabled').checked=" + controlDetails.Value + "");
                        controlResponse = true;
                    }
                }
            }
            catch
            {
                return controlResponse;
            }

            return controlResponse;
        }

    }
}
