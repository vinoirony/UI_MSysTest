using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSysTest
{
    public class POM
    {
        public static string URL = "https://www.amazon.com/";        
        public static string PATH_DRIVER = @"E:\Driver\";        
        public static string PATH_DOWNLOAD = @"C:\Users\Downloads\";
        public static string BUTTON_COOKIES_ACCEPT = "/html/body/div[5]/div[3]/button";       
        public static string BUTTON_SIGNIN = "//*[@id='nav-link-accountList-nav-line-1']";
        public static string BUTTON_CONTINUE = "continue";
        public static string USERNAME = "ap_email";
        public static string PASSWORD = "ap_password";
        public static string BUTTON_SUBMIT = "signInSubmit";
        public static string INPUTBOX_SEARCH = "twotabsearchtextbox";
        public static string BUTTON_SEARCH = "nav-search-submit-button";
        public static string CHECKBOX_BRAND = "//span[text()='OnePlus']";
        public static string BUTTON_CHOOSE = "//h2/a/span[contains(text(),'Model BE2011')]";
        public static string GET_PRICE = "priceblock_ourprice";
        public static string BUTTON_ADDTOCART = "add-to-cart-button";
    }
}
