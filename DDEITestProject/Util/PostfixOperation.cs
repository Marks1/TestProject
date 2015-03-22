using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDEITestProject.Util
{
    class PostfixOperation
    {

        private SSHOperation ssh;

        public PostfixOperation(string IP, string sshUsername, string sshPassword)
        {
            Console.WriteLine("DDEI:{0}", IP);
            ssh = new SSHOperation(IP, sshUsername, sshPassword);

        }

        public string GetRuntimePostfixConf(string option)
        {
            WaitPostfixReload();
            string value = "";
            string cmd = @"/opt/trend/ddei/postfix/usr/sbin/postconf | grep ";
            option = "'" + option + " ='";
            cmd = cmd + option + @" | awk -F= '{print $2}'";
            value = ssh.ExecuteCMD(cmd);
            return value.Trim();
        }

        public string GetRuntimePostfixPort()
        {
            string cmd = @"cat  /opt/trend/ddei/postfix/etc/postfix/master.cf |grep smtpd |  wc -l";
            int lines = Int32.Parse(ssh.ExecuteCMD(cmd));

            string line = "";
            string[] arr = { "" };
            for (int i = 1; i <= lines; i++)
            {
                cmd = @"cat  /opt/trend/ddei/postfix/etc/postfix/master.cf |grep smtpd |  grep inet | head -n " + i.ToString() + " | tail -n 1";
                line = ssh.ExecuteCMD(cmd);

                if (line.Length != 0)
                {

                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    else
                    {
                        char[] separator = { ' ' };
                        arr = line.Split(separator);
                        break;
                    }
                }
            }
            return arr[0];

        }

        public string getRuntimePostfixConn()
        {
            string cmd = @"cat  /opt/trend/ddei/postfix/etc/postfix/master.cf |grep smtpd |  wc -l";
            int lines = Int32.Parse(ssh.ExecuteCMD(cmd));

            string line = "";
            string[] arr = { "" };
            for (int i = 1; i <= lines; i++)
            {
                cmd = @"cat  /opt/trend/ddei/postfix/etc/postfix/master.cf |grep smtpd |  grep inet | head -n " + i.ToString() + " | tail -n 1";
                line = ssh.ExecuteCMD(cmd);

                if (line.Length != 0)
                {

                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    else
                    {
                        char[] separator = { ' ' };
                        arr = line.Split(separator);
                        break;
                    }
                }
            }
            return arr[36];

        }

        public void WaitPostfixReload()
        {
            //TBC
        }
    }
}
