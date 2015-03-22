using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System.Drawing.Imaging;

namespace LibPages
{
    public class PageBase
    {
        private IWebDriver driver;
        protected WebDriverWait GlobalWait;

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            GlobalWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(15));
        }

        /**
            * Validate the element on page
            * 
            * @param by
            * @return true: exist; false: not exist
            */
        protected bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }
        }

        /**
            * Input a string into a edit box on page
            * 
            * @param by
            * @param inputStr
            * @return void
            */
        protected void Input(By by, string inputStr)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                element.Clear();
                element.SendKeys(inputStr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /**
            * Click a button on page
            * 
            * @param by
            * @return void
            */
        protected void Click(By by)
        {
            try
            {
                driver.FindElement(by).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /**
            * Switch to frame
            * 
            * @param by
            * @return void
            */
        protected void SwitchToFrame(By by)
        {
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(by));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /**
            * Direct browser to URI
            * 
            * @param uri
            * @return void
            */
        protected void NavigateToUri(Uri uri)
        {
            try
            {
                driver.Navigate().GoToUrl(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /**
            * Direct browser to URI
            * 
            * @param uri
            * @return string on element; 
            */
        protected string GetElementText(By by)
        {
            try
            {
                return driver.FindElement(by).Text;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return null;
            }
        }

        /**
            * Wait for element
            * 
            * @param By
            * @return void; 
            */
        protected void WaitForElement(By by)
        {
            try
            {
                GlobalWait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /**
            * Wait for element txt become
            * 
            * @param By, title
            * @return void; 
            */
        protected void WaitForElementTitle(By by, string title)
        {
            try
            {
                GlobalWait.Until(ExpectedConditions.ElementIsVisible(by));
                GlobalWait.Until(d => driver.FindElement(by).Text.CompareTo(title) == 0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /*
        * Take snapshot 
        */
        protected void TakeSnapshotTo(string imagePath)
        {
            if (((IHasCapabilities)driver).Capabilities.HasCapability(CapabilityType.TakesScreenshot))
            {
                Screenshot sn = ((ITakesScreenshot)driver).GetScreenshot();

                sn.SaveAsFile(imagePath, ImageFormat.Jpeg);
            }
            else
            {
                Console.WriteLine("Cannot take snapshot because system does not support");
            }
        }
    }

}
