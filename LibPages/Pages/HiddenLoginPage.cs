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
    public class HiddenLoginPage:PageBase
    {
        /*
         * Hidden page locater
        */
        public static string Hidden_PageHead = "Internal Support and Testing";
        public static string Hidden_Login_page_Title = "Deep Discovery Email Inspector Login";
        public static By Hidden_Login_Password = By.Id("password");
        public static By Hidden_Login_btn_submit = By.Id("login_btn");

        private IWebDriver driver;

        public HiddenLoginPage(IWebDriver driver)
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

            if (!driver.Title.StartsWith(Hidden_Login_page_Title))
            {
                throw new NotFoundException("The Hidden page is not ready for testing, current page is " + driver.Url);
            }
            else
            {
                if (!driver.PageSource.Contains(Hidden_PageHead))
                {
                    throw new NotFoundException("The Hidden page is not ready for testing, current page is " + driver.Url);
                }
            }
        }

        /**
         * Login hidden page with valid user
         * 
         * @param username
         * @param password
         * @return HiddenMainPage object
         * */
        public HiddenMainPage LoginValidUser(string password)
        {
            Input(Hidden_Login_Password, password);
            return SubmitLogin();
        }

        /**
         * Login hidden page with invalid user
         * 
         * @param username
         * @param password
         * @return HiddenMainPage object
         * */
        public HiddenLoginPage LoginInvalidUser(string password)
        {
            Input(Hidden_Login_Password, password);
            return SubmitFailedLogin();
        }

        /**
         * click the login button
         * 
         * @param null
         * @return HiddenMainPage object
         * */
        private HiddenMainPage SubmitLogin()
        {
            Click(Hidden_Login_btn_submit);
            return new HiddenMainPage(driver);
        }

        private HiddenLoginPage SubmitFailedLogin()
        {
            Click(Hidden_Login_btn_submit);
            return new HiddenLoginPage(driver);
        }

    }
}
