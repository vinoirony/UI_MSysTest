using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSysTest
{
    public class POM
    {
        public static string URL = "https://www.irctc.co.in/nget/train-search";        
        public static string PATH_DRIVER = @"C:\Program Files\Driver";        
        public static string PATH_DOWNLOAD = @"C:\Users\Downloads\";
        public static string BUTTON_COOKIES_ACCEPT = "/html/body/div[5]/div[3]/button";       
        public static string BUTTON_OK = "//button[text()='OK']";
        public static string FROM = "//*[@id='origin']/span/input";
        public static string TO = "//*[@id='destination']/span/input";
        public static string BUTTON_SEARCH = "//button[text()='Search']";
    }
}
