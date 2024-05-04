using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    internal class Logger
    {
        readonly string _path = @"Log.txt";
        public void FileLog(string message, params object[] args)
        {
            string logMessage = string.Format(message, args);
            File.AppendAllLines(_path,new[] {logMessage});
        }
        
    }
}