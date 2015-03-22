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
    public class LoginPage : PageBase
    {
        /*
            * Login page locater
        */
        public static string Login_page_Title = "Deep Discovery Email Inspector Login";
        public static By Login_Username = By.Id("userid");
        public static By Login_Password = By.Id("password");
        public static By Login_btn_submit = By.Id("login_btn");

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
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

            //check the page status before starting operating on it
            if (!driver.Title.StartsWith(Login_page_Title))
            {
                throw new NotFoundException("The Login page is not ready for operation, current page is " + driver.Url);
            }
        }

        /**
            * Login with valid user
            * 
            * @param username
            * @param password
            * @return Homepage object
            * */
        public MainPage LoginValidUser(string username, string password)
        {
            InputUsername(username);
            InputPassword(password);
            return SubmitLogin();
        }

        /**
            * Login with invalid user
            * 
            * @param username
            * @param password
            * @return LoginPage object
            * */
        public LoginPage LoginInvalidUser(string username, string password)
        {
            InputUsername(username);
            InputPassword(password);
            return SubmitFailureLogin();
        }

        /**
            * Type in username
            * 
            * @param username
            * @return LoginPage object
            * */
        private LoginPage InputUsername(string username)
        {
            Input(Login_Username, username);

            return this;
        }

        /**
            * Type in password
            * 
            * @param password
            * @return LoginPage object
            * */
        private LoginPage InputPassword(string password)
        {
            Input(Login_Password, password);

            return this;
        }

        /**
            * click the login button
            * 
            * @param null
            * @return HomePage object
            * */
        private MainPage SubmitLogin()
        {
            Click(Login_btn_submit);
            return new MainPage(driver);
        }

        /**
            * click the login button with invalid username and password
            * 
            * @param null
            * @return HomePage object
            * */
        private LoginPage SubmitFailureLogin()
        {
            Click(Login_btn_submit);
            return new LoginPage(driver);
        }
    }

}
