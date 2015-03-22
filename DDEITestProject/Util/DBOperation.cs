using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDEITestProject.Util
{
    public class DBOperation
    {
        //Postgresql bin location
        static string Postgre = @"/opt/trend/ddei/PostgreSQL/bin/psql ddei sa -t -c ";

        private SSHOperation ssh;

        public DBOperation(string IP, string sshUsername, string sshPassword)
        {
            Console.WriteLine("DDEI:{0}", IP);
            ssh = new SSHOperation(IP, sshUsername, sshPassword);

        }

        public string ExecuteSQLFromSSH(string sql)
        {
            string cmd = string.Format(@"{0} '{1}'", Postgre, sql);

            return ssh.ExecuteCMD(cmd);
        }
    }
}
