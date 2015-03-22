using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Configuration;
using LibPages;
using DDEITestProject.Util;
using System.Threading;
using OpenQA.Selenium.Remote;

namespace DDEITestProject.Steps
{
    public class DDEITestCommon
    {
        protected static string DDEIIP = Utils.ReadAppSettings("DDEIIP");
        protected static string SSHUSER = Utils.ReadAppSettings("SSHUSER");
        protected static string SSHPWD = Utils.ReadAppSettings("SSHPWD");

        public static IWebDriver InitWebDriver()
        {
            IWebDriver driver;

            string Browser = Utils.ReadAppSettings("Browser");


            if (Browser.CompareTo("IE") == 0)
            {
                driver = new FirefoxDriver();
            }
            else if (Browser.CompareTo("Chrome") == 0)
            {
                driver = new FirefoxDriver();
            }
            else
            {
                DesiredCapabilities capability = DesiredCapabilities.Firefox();
                driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), capability);
                //Remove all cookie before testing
                driver.Manage().Cookies.DeleteAllCookies();
                
            }
            //maximum the window
            driver.Manage().Window.Maximize();
            
            return driver;
        }

        /**
         * Clean up databases for each scenario case
         * 
        */
        public static void CleanUp()
        {
            DBOperation query = new DBOperation(DDEIIP, SSHUSER, SSHPWD);

            Console.WriteLine("Clean up Database: tb_policy_event");
            query.ExecuteSQLFromSSH("delete from tb_policy_event;");

            Console.WriteLine("Clean up Database: tb_policy_event_total");
            query.ExecuteSQLFromSSH("delete from tb_policy_event_total;");
        }



        
    }

    class ExpectedCondition : DDEITestCommon
    {
        public static bool PolicyEventHasRecord(int timeout)
        {
            string sql = @"select count(*) from tb_policy_event_total;";

            DBOperation query = new DBOperation(DDEIIP, SSHUSER, SSHPWD);
            
            int time = timeout;
            while (time > 0)
            {
                string policyEventNumber = query.ExecuteSQLFromSSH(sql);
                Console.WriteLine("policyEventNumber: {0}", policyEventNumber.Trim());
                if (policyEventNumber != "")
                {
                    if (Int32.Parse(policyEventNumber) == 0) {
                        Thread.Sleep(1000);
                        time = time - 1; 
                        continue;
                    }
                    else { return true; }
                }
            }
            return false;
        }

    }
}
