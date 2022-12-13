using Backend.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccessLayer.Utilities
{
    public static class LoggerExtension
    {
        public static void WriteLogInfo(this object source, ILog log, string function, string message)
        {
            LogInfo logInfo = new LogInfo();

            logInfo.Error = message;
            logInfo.Class = source.GetType().ToString();
            logInfo.Function = function;
            logInfo.IP = Helper.GetLocalIPAddress();

            log.Error(logInfo.Message);
        }
    }
}
