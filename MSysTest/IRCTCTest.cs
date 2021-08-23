using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MSysTest
{
    [TestClass]
    public class IRCTCTest
    {
        protected static IWebDriver WebDriver { get; set; }
        protected static Exception Exception { get; set; }
        public static void LaunchBrowser(string AppUrl, string BrowserExePath, string DownloadFilePath)
        {
            GetDriver(BrowserExePath, DownloadFilePath);
            UrlPass(AppUrl);
            MaximizeWindow();
            Thread.Sleep(3000);
        }
     
        public static void UrlPass(string AppUrl)
        {
            WebDriver.Url = AppUrl;
        }
        public static void MaximizeWindow()
        {
            WebDriver.Manage().Window.Maximize();
        }
        public static void WebdriverClose()
        {
            WebDriver.Close();
        }


        protected static IWebDriver GetDriver(string browserpath, string downloadfilepath)
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--disable-notifications");
                chromeOptions.AddUserProfilePreference("download.default_directory", downloadfilepath);
                chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
                chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                chromeOptions.Proxy = null;
                WebDriver = new ChromeDriver(browserpath, chromeOptions);
                return WebDriver;
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return null;
        }

        [TestMethod]
        public void CorrespondingTrains()
        {           
            LaunchBrowser(POM.URL, POM.PATH_DRIVER, POM.PATH_DOWNLOAD);            
            WebDriver.FindElement(By.XPath(POM.BUTTON_OK)).Click();
            var From = WebDriver.FindElement(By.XPath(POM.FROM));
            From.SendKeys("CHENNAI EGMORE - MS");
            var To = WebDriver.FindElement(By.XPath(POM.TO));
            To.SendKeys("KANYAKUMARI - CAPE");
            WebDriver.FindElement(By.XPath(POM.BUTTON_SEARCH)).Click();
            Thread.Sleep(5000);

          
        }
    }
}
