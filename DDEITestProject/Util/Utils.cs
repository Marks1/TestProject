using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace DDEITestProject.Util
{
    public class Utils
    {

        public static string ReadAppSettings(string key)
        {
            string result = "";
            try
            {
                var appSettings = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                Console.WriteLine("Assembly: {0}", Assembly.GetExecutingAssembly().Location);
                result = appSettings.AppSettings.Settings[key].Value;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Message);
            }
            return result;
        }
    }
}
