using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;

namespace DDEITestProject.Util
{
    public class SSHOperation
    {
        private SshExec shell;

        public SSHOperation(string DDEI_IP, string SSH_ROOT, string SSH_PASS)
        {
            shell = new SshExec(DDEI_IP, SSH_ROOT, SSH_PASS);
            try
            {
                shell.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happened when SSH connect to DDEI! [{0}]", e.Message);
                throw new Exception("SSH connection fail!");
            }
        }

        ~SSHOperation()
        {
            shell.Close();
        }

        public SshExec SSHInstance
        {
            get
            {
                return shell;
            }
        }

        public string ExecuteCMD(string cmd)
        {
            if (!shell.Connected)
            {
                return "";
            }

            string output = "";

            try
            {
                output = shell.RunCommand(cmd);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when execute command by SSH. [{0}]", e.Message);
            }

            return output;           
        }

        

    }
}
