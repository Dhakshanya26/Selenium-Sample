using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Serve.Platform.Configuration.UI.SeleniumTest.Common;
using System.Configuration;

namespace Serve.Platform.Configuration.UI.SeleniumTest.SeleniumBase
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        private static string JobName { get; set; }

        [TestInitialize]
        public static void Initialize(string jobName="CPTTest")
        {
            var isJenkins = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["JenkinsEnvironment"]);
            Instance = GetBrowserDriver(BrowserTypeEnum.Chrome, isJenkins);
            System.Diagnostics.Debug.WriteLine(string.Format("Initialize - {0}",DateTime.Now));
            JobName = jobName;
            TurnOnWait();
        }


        private static IWebDriver GetBrowserDriver(BrowserTypeEnum browserType, bool isJenkinsEnvironment = false)
        {
            IWebDriver driver = null;
            //BasicAuth = new BasicAuthWebDriverWrapper();
            //BasicAuthSettings.BrowserType = BrowserType.IE;
            //BasicAuthSettings.Domain = "ads";
            //BasicAuthSettings.Username = "ads_servetest14";
            //BasicAuthSettings.Password = "?6kuwrAn";

            switch (browserType)
            {
                case BrowserTypeEnum.Chrome:
                    if (!isJenkinsEnvironment)
                    {
                        DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("test-type");
                        capabilities.SetCapability(ChromeOptions.Capability, chromeOptions);
                        driver = new ChromeDriver(chromeOptions);
                    }
                    else
                    {
                        DesiredCapabilities desiredCapabilites = new DesiredCapabilities(EnvironmentVariableByKey("SELENIUM_BROWSER"),
                                EnvironmentVariableByKey("SELENIUM_VERSION"), OpenQA.Selenium.Platform.CurrentPlatform); // set the desired browser 

                        desiredCapabilites.SetCapability("username", EnvironmentVariableByKey("SAUCE_USER_NAME")); // supply sauce labs username 
                        desiredCapabilites.SetCapability("accessKey", EnvironmentVariableByKey("SAUCE_API_KEY"));  // supply sauce labs account key 

                        desiredCapabilites.SetCapability("name", JobName);  // Job Name

                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_HOST")), "SELENIUM_HOST: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_HOST"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_PORT")), "SELENIUM_PORT: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_PORT"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_PLATFORM")), "SELENIUM_PLATFORM: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_PLATFORM"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_VERSION")), "SELENIUM_VERSION: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_VERSION"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_BROWSER")), "SELENIUM_BROWSER: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_BROWSER"));
                        ////System.Diagnostics.Debug.WriteLineIf (!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DEVICE")),"SELENIUM_DEVICE: " + GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DEVICE"));
                        ////System.Diagnostics.Debug.WriteLineIf (!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DEVICE_TYPE")),"SELENIUM_DEVICE_TYPE: " + GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DEVICE_TYPE"));
                        ////System.Diagnostics.Debug.WriteLineIf (!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DRIVER")),"SELENIUM_DRIVER: " + GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_DRIVER"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_ONDEMAND_BROWSERS")), "SAUCE_ONDEMAND_BROWSERS: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_ONDEMAND_BROWSERS"));
                        ////System.Diagnostics.Debug.WriteLineIf (!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_URL")),"SELENIUM_URL: " + GenericCSTFunctionalities.EnvironmentVariableByKey ("SELENIUM_URL"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_USER_NAME")), "SAUCE_USER_NAME: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_USER_NAME"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_API_KEY")), "SAUCE_API_KEY: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SAUCE_API_KEY"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_STARTING_URL")), "SELENIUM_STARTING_URL: " + GenericCSTFunctionalities.EnvironmentVariableByKey("SELENIUM_STARTING_URL"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("APP_USERNAME")), "APP_USERNAME: " + GenericCSTFunctionalities.EnvironmentVariableByKey("APP_USERNAME"));
                        //System.Diagnostics.Debug.WriteLineIf(!String.IsNullOrEmpty(GenericCSTFunctionalities.EnvironmentVariableByKey("APP_PASSWORD")), "APP_PASSWORD: IS NOT NULL");


                        string strURI = String.Format("http://{0}:{1}@{2}", EnvironmentVariableByKey("SAUCE_USER_NAME"),
                            EnvironmentVariableByKey("SAUCE_API_KEY"), EnvironmentVariableByKey("SELENIUM_STARTING_URL"));
                        System.Diagnostics.Debug.WriteLine("URI:   " + strURI);


                        Uri commandExecutorUri = new Uri(strURI);

                        driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
                        driver.Manage().Window.Maximize();
                    }
                    break;
                case BrowserTypeEnum.Firefox:
                    driver = new FirefoxDriver() { };
                    
                    throw new Exception("Driver not ready yet");
                    break;
                case BrowserTypeEnum.InternetExplorer:
                    if (!isJenkinsEnvironment)
                    {
                        var options = new InternetExplorerOptions
                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            IgnoreZoomLevel = true,
                            UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Ignore,
                        };
                        
                        driver = new InternetExplorerDriver(options);
                        //driver = (InternetExplorerDriver) BasicAuth.AuthenticatedDriver;
                    }
                    else
                    {
                        var desiredCapabilities = DesiredCapabilities.InternetExplorer();
                        driver = new RemoteWebDriver(new Uri(Convert.ToString(ConfigurationManager.AppSettings["RemoteWebDriverURL"])),
                            desiredCapabilities);
                    }

                    break;
            }

            //            driver.Manage().Window.Maximize();

            return driver ?? null;
        }



        public static void Close()
        {
            Instance.Close();
            Instance.Quit();
            //BasicAuth.Quit();
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
        }

        public static void NoWait(Action action)
        {
            TurnOffWait();
            action();
            TurnOnWait();
        }

        private static void TurnOnWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        private static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        }

        public static string EnvironmentVariableByKey(string key)
        {
            string envVaribale = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(envVaribale))
            {
                throw new ArgumentNullException();
            }
            else
            {
                return envVaribale;
            }

            //throw new ArgumentException(envVaribale);
        }
    }
}
