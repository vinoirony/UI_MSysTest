using System;
using System.Collections.Generic;
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
            Thread.Sleep(5000);
            var From = WebDriver.FindElement(By.XPath(POM.FROM));
            From.SendKeys("CHENNAI EGMORE - MS");
            var To = WebDriver.FindElement(By.XPath(POM.TO));
            To.SendKeys("KANYAKUMARI - CAPE");           
            WebDriver.FindElement(By.XPath(POM.BUTTON_SEARCH)).Click();
            Thread.Sleep(5000);
            Assert.AreEqual(WebDriver.FindElement(By.XPath(POM.BUTTON_SEARCH)).Text, " MS GURUVAYUR EX (06127)");            
        }

        [TestMethod]
        public void Test2()
        {
            CorrespondingTrains();
            WebDriver.FindElement(By.XPath(POM.TrainSchedule)).Click();
            Assert.AreEqual(WebDriver.FindElement(By.XPath(POM.BUTTON_SEARCH)).Text, " MS GURUVAYUR EX (06127)");
            // xpath of html table
            var elemTable = WebDriver.FindElement(By.XPath("//*[@id='divMain']/div/app-train-list/p-dialog/div/div/div[2]/app-check-train-schedule/div[2]/div/div[2]"));

            // Fetch all Row of the table
            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            String strRowData = "";

            // Traverse each row
            foreach (var elemTr in lstTrElem)
            {
                // Fetch the columns from a particuler row
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    // Traverse each column
                    foreach (var elemTd in lstTdElem)
                    {
                        // "\t\t" is used for Tab Space between two Text
                        strRowData = strRowData + elemTd.Text + "\t\t";
                    }
                }
                else
                {
                    // To print the data into the console
                    Console.WriteLine("This is Header Row");
                    Console.WriteLine(lstTrElem[0].Text.Replace(" ", "\t\t"));
                }
                Console.WriteLine(strRowData);
                strRowData = String.Empty;
            }
        }

        [TestCleanup]
        public void TestCleaup()
        {                        
            WebDriver.Close();
            WebDriver.Quit();
        }

    }
}
