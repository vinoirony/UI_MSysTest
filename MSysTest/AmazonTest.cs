using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MSysTest
{
    [TestClass]
    public class AmazonTest
    {
        public TestContext TestContext { get; set; }
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

        public static IEnumerable<object[]> ReadExcel()
        {
            //Create Worksheet object
            using (ExcelPackage package = new ExcelPackage(new FileInfo("Data.xlsx")))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                int rowcount = worksheet.Dimension.End.Row;
                for (int i = 2; i <= rowcount; i++)
                {
                    yield return new object[]{
                        worksheet.Cells[i,1].Value?.ToString().Trim(),
                        worksheet.Cells[i,2].Value?.ToString().Trim(),
                        worksheet.Cells[i,3].Value?.ToString().Trim()
                    };
                    }
                }

            }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\data.csv", "data#csv", DataAccessMethod.Sequential)]
        [DynamicData(nameof(ReadExcel), DynamicDataSourceType.Method)]
        [TestMethod]
        public void Amazon(string URL, string SearchType, string MobileBrand)
        {
            //Launch the browser             
            LaunchBrowser(URL, POM.PATH_DRIVER, POM.PATH_DOWNLOAD);                                   
            
            WebDriver.FindElement(By.XPath(POM.BUTTON_SIGNIN)).Click();                        
            
            var Username = WebDriver.FindElement(By.Id(POM.USERNAME));
            Username.SendKeys(TestContext.DataRow[1].ToString());            
            WebDriver.FindElement(By.Id(POM.BUTTON_CONTINUE)).Click();
           
            var Password = WebDriver.FindElement(By.Id(POM.PASSWORD));
            Password.SendKeys(TestContext.DataRow[2].ToString());            
            WebDriver.FindElement(By.Id(POM.BUTTON_SUBMIT)).Click();           
            
            //Select class to hadel the List Items
            SelectElement oSelection = new SelectElement(WebDriver.FindElement(By.Id("searchDropdownBox")));

            IList<IWebElement> oSize = oSelection.Options;

            int iListSize = oSize.Count;
            // Setting up the loop to print all the options
            for (int i = 0; i < iListSize; i++)
            {
                // Storing the value of the option	
                String sValue = oSelection.Options.ElementAt(i).Text;
                // Printing the stored value
                Console.WriteLine("Value of the Select item is : " + sValue);

                // Putting a check on each option that if any of the option is equal to 'Africa" then select it 
                //if (sValue.Equals("Electronics"))
                if (sValue.Equals(SearchType))
                {
                    oSelection.SelectByIndex(i);
                    break;
                }
            }

            var SearchInput = WebDriver.FindElement(By.Id(POM.INPUTBOX_SEARCH));
          //  SearchInput.SendKeys("One Plus Mobile");
            SearchInput.SendKeys(MobileBrand);
            WebDriver.FindElement(By.Id(POM.BUTTON_SEARCH)).Click();
            WebDriver.FindElement(By.XPath(POM.CHECKBOX_BRAND)).Click();
            WebDriver.FindElement(By.XPath(POM.BUTTON_CHOOSE)).Click();

            var Price = WebDriver.FindElement(By.Id(POM.GET_PRICE)).Text;            
            Console.WriteLine("Retrived Mobile Price is: ", Price);

            WebDriver.FindElement(By.Id(POM.BUTTON_ADDTOCART)).Click();
        }

        [TestCleanup]
        public void TestCleaup()
        {
            WebdriverClose();            
        }

    }
}
