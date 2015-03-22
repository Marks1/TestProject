using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace DDEITestProject.Util
{
    public class MailOperation
    {
        /**
         * Mail parameters 
        */
        private string SmtpServer;
        private int SmtpPort;
        private string SmtpSender;
        private string SmtpRecipient;
        private string maillocation;
        private string attachmentlocation;

        private static string TeMailBin = Utils.ReadAppSettings("TeMailBin");

        public MailOperation(string SmtpServer, int SmtpPort, string SmtpSender, string SmtpRecipient)
        {
            this.SmtpServer = SmtpServer;
            this.SmtpPort = SmtpPort;
            this.SmtpSender = SmtpSender;
            this.SmtpRecipient = SmtpRecipient;
            
        }

        public string MailPath
        {
            get { return maillocation; }
            set { maillocation = value; }
        }

        public string Attachment
        {
            get { return attachmentlocation; }
            set { attachmentlocation = value; }
        }

        public bool SendMail()
        {
            Process TeMailProcess = new Process();

            if (maillocation.Length == 0)
            {
                Console.WriteLine("Must assign mail location");
                return false;
            }

            string TemailParameters = string.Format(@"/smtp={0} /from={1} /to={2} {3}",
                                                SmtpServer,
                                                SmtpSender,
                                                SmtpRecipient,
                                                maillocation);

            try
            {
               
                TeMailProcess.StartInfo.UseShellExecute = false;
                TeMailProcess.StartInfo.FileName = TeMailBin;
                TeMailProcess.StartInfo.Arguments = TemailParameters;
                TeMailProcess.StartInfo.CreateNoWindow = true;
                return TeMailProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool SendMailWithAttachment()
        {
            Process TeMailProcess = new Process();

            if (maillocation.Length == 0 || attachmentlocation.Length ==0)
            {
                Console.WriteLine("Must assign mail location and attachment location");
                return false;
            }

            string TemailCommand = string.Format(@"%s /smtp=%s /from=%s /to=%s %s /Attach=%s",
                                                TeMailBin,
                                                SmtpServer,
                                                SmtpSender,
                                                SmtpRecipient,
                                                maillocation,
                                                attachmentlocation);

            try
            {
                TeMailProcess.StartInfo.UseShellExecute = false;
                TeMailProcess.StartInfo.FileName = TemailCommand;
                TeMailProcess.StartInfo.CreateNoWindow = true;
                return TeMailProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
