using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace LibPages
{
    public class MainPage : PageBase
    {
        /*
         * Main page locater
        */
        public static string Main_page_Title = "Deep Discovery Email Inspector Login";
        private IWebDriver driver;


        public MainPage(IWebDriver driver)
            : base(driver)
        {
            if (driver != null)
            {
                this.driver = driver;
            }
            else
            {
                throw new WebDriverException("No webdriver instance can be operated");
            }

            if (!driver.Title.StartsWith(Main_page_Title))
            {
                throw new NotFoundException("The home page is not ready for testing, current page is " + driver.Url);
            }
        }
    }
}
