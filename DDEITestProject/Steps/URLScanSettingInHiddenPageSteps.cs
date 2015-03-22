using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using LibPages;
using DDEITestProject.Util;
using NUnit.Framework;

namespace DDEITestProject.Steps
{
    [Binding]
    public class URLScanSettingInHiddenPageSteps
    {
        private static IWebDriver driver;
        private static string DDEIIP;
         
        private static HiddenMainPage HiddenMain;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            //Clean database
            DDEITestCommon.CleanUp();

            //create web driver instance
            driver = DDEITestCommon.InitWebDriver();

            //Get DDEI IP
            DDEIIP = Utils.ReadAppSettings("DDEIIP");

        }

        [AfterScenario]
        public static void AfterScenario()
        {

            //set back URL scan to enable!
            Console.WriteLine("Set back URL scan function to enable");
            HiddenMain.SwitchToURLScanSettingPage().EnableURLScan();

            Console.WriteLine("Quit() the driver! Bye");
            driver.Quit();
        }

        [Given(@"I have login DDEI hidden page")]
        public void GivenIHaveLoginDDEIHiddenPage()
        {
            driver.Navigate().GoToUrl(string.Format(@"https://{0}/hidden/rdqa.php", DDEIIP));
            HiddenLoginPage HiddenLogin = new HiddenLoginPage(driver);
            HiddenMain = HiddenLogin.LoginValidUser(Utils.ReadAppSettings("DDEIHIddenPagePassword"));
            Console.WriteLine("Login to hidden page");
        }
        
        [Given(@"I enable the function of URL scanning")]
        public void GivenIEnalbeTheFunctionOfURLScaning()
        {
            if (HiddenMain == null)
            {
                GivenIHaveLoginDDEIHiddenPage();
            }
            HiddenURLScanSettingPage urlscanSetting = HiddenMain.SwitchToURLScanSettingPage();
            urlscanSetting.EnableURLScan();

            Console.WriteLine("Enable URL scan function");
        }
        
        [Given(@"I disable the function of URL scanning")]
        public void GivenIDisalbeTheFunctionOfURLScaning()
        {
            if (HiddenMain == null)
            {
                GivenIHaveLoginDDEIHiddenPage();
            }
            HiddenURLScanSettingPage urlscanSetting = HiddenMain.SwitchToURLScanSettingPage();
            urlscanSetting.DisableURLScan();

            Console.WriteLine("Disable URL scan function");
        }
        
        [When(@"DDEI process mail with malicious URL")]
        public void WhenDDEIProcessMailWithMaliciousURL()
        {
            string sender = Utils.ReadAppSettings("Sender");
            string recipient = Utils.ReadAppSettings("Recipient");
            int port = Int32.Parse(Utils.ReadAppSettings("Port"));
            MailOperation mail = new MailOperation(DDEIIP, port, sender, recipient);
            mail.MailPath = Utils.ReadAppSettings("MailSamplePath") + @"/MailiciousURL.eml";
            if (!mail.SendMail()) { Assert.Fail(); }

            Console.WriteLine("Mail [{0}] has been sent!", mail.MailPath);
        }
        
        [Then(@"The URL will be scanned")]
        public void ThenTheURLWillBeScanned()
        {
            int timeout = Int32.Parse(Utils.ReadAppSettings("TimeoutForWaitPolicyEvent"));
            Assert.AreEqual(true, ExpectedCondition.PolicyEventHasRecord(timeout));
        }
        
        [Then(@"The URL will not be scanned")]
        public void ThenTheURLWillNotBeScanned()
        {
            int timeout = Int32.Parse(Utils.ReadAppSettings("TimeoutForWaitPolicyEvent"));
            Assert.AreEqual(false, ExpectedCondition.PolicyEventHasRecord(timeout));

        }
    }
}
