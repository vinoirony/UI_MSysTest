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
        public static string BUTTON_SCHEDULE = "//button[text()='OK']";
        public static string FROM = "//*[@id='origin']/span/input";
        public static string TO = "//*[@id='destination']/span/input";
        public static string BUTTON_SEARCH = "//button[text()='Search']";
        public static string DATE = "//*[@id='jDate']/span/input";
        public static string CLASS = "//a[@id='cllink-13237-CC-1']";
        public static string BOOKNOW = "//a[@id='13237-3A-GN-0']";
        public static string TrainName = "//*[@id='divMain']/div/app-train-list/div[4]/div/div[5]/div[1]/div[1]/app-train-avl-enq/div[1]/div[1]/div[1]/strong/text()";
        public static string TrainSchedule = "//span[text()='Train Schedule'][1]";
    }
}
