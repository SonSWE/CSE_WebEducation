using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommonLib
{
    public class Logger
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly ILog nlog = log4net.LogManager.GetLogger(typeof(Program));
        //public static readonly ILog nlog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
