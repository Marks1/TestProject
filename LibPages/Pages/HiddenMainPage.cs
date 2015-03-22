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
    public class HiddenMainPage:PageBase
    {  
        /*
         * Hidden main page locater
        */
        public static string Hidden_Main_page_Title = "Deep Discovery Email Inspector";
        public static string Hidden_Main_Logoff = "Log off";      
  
        public static By Hidden_Frame_Left = By.Id("left");
        public static By Hidden_Frame_Right = By.Id("right");

        public static By Hidden_Tab_URL_Scan_Setting = By.Id("url_scan");
        public static By Hidden_URL_Setting_Page_Enable = By.Id("wrs_enable");

        public static By Hidden_Tab_URL_Filter = By.Id("url_filter");


        private IWebDriver driver;

        public HiddenMainPage(IWebDriver driver)
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

            if (!driver.Title.StartsWith(Hidden_Main_page_Title))
            {
                throw new NotFoundException("The Hidden page is not ready for testing, current page is " + driver.Url);
            }
        }

        private  HiddenURLScanSettingPage SwitchToHiddenTab(By TabLocation)
        {
            driver.SwitchTo().DefaultContent();

            SwitchToFrame(Hidden_Frame_Left);
            Click(TabLocation);
            driver.SwitchTo().DefaultContent();

            SwitchToFrame(Hidden_Frame_Right);

            return new HiddenURLScanSettingPage(driver);
        }

        /**
         * Switch to URL scan setting tab
         * 
         * @param null
         * @return HiddenURLScanSettingPage object
         * */
        public HiddenURLScanSettingPage SwitchToURLScanSettingPage()
        {
            SwitchToHiddenTab(Hidden_Tab_URL_Scan_Setting);
            WaitForElement(Hidden_URL_Setting_Page_Enable);
            return new HiddenURLScanSettingPage(driver);
        }
    }

    public class HiddenURLScanSettingPage : HiddenMainPage
    {
        
        public static By Hidden_URL_Setting_Page_Save = By.Id("btn_save");
        public static By Hidden_URL_Setting_Page_Save_Status = By.Id("flag");
        public static string Hidden_URL_Setting_Page_Save_Status_OK = "Saved OK.";

        private IWebDriver driver;

        public HiddenURLScanSettingPage(IWebDriver driver)
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

            if (!IsElementPresent(Hidden_URL_Setting_Page_Enable))
            {
                throw new NotFoundException("The Hidden page is not ready for testing, current page is " + driver.Url);
            }
        }

        /**
         * Enable URL scan
         * 
         * @param null
         * @return HiddenURLScanSettingPage object
         * */
        private HiddenURLScanSettingPage EnableURLScanCheckBox()
        {
            if (!driver.FindElement(Hidden_URL_Setting_Page_Enable).Selected)
            {
                Click(Hidden_URL_Setting_Page_Enable);
            }
            return new HiddenURLScanSettingPage(driver);
        }

        /**
         * Disable URL scan
         * 
         * @param null
         * @return HiddenURLScanSettingPage object
         * */
        private HiddenURLScanSettingPage DisableURLScanCheckBox()
        {
            if (driver.FindElement(Hidden_URL_Setting_Page_Enable).Selected)
            {
                Click(Hidden_URL_Setting_Page_Enable);
            }
            return new HiddenURLScanSettingPage(driver);
        }

        /**
         * Save
         * 
         * @param null
         * @return HiddenURLScanSettingPage object
         * */
        private HiddenURLScanSettingPage SaveURLScanSetting()
        {
            Click(Hidden_URL_Setting_Page_Save);
            WaitForElementTitle(Hidden_URL_Setting_Page_Save_Status, Hidden_URL_Setting_Page_Save_Status_OK);
            
            return new HiddenURLScanSettingPage(driver);
        }

        public HiddenURLScanSettingPage EnableURLScan()
        {
            EnableURLScanCheckBox();
            SaveURLScanSetting();

            return new HiddenURLScanSettingPage(driver);
        }

        public HiddenURLScanSettingPage DisableURLScan()
        {
            DisableURLScanCheckBox();
            SaveURLScanSetting();

            return new HiddenURLScanSettingPage(driver);
        }
    }

}
