using Cal.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Help
{
    /// <summary>
    /// 日志辅助方法
    /// </summary>
    public class Log
    {
        public static string LogPath = Directory.GetCurrentDirectory() + "\\Logs";
        #region 异常日志

        /// <summary>
        /// 写普通操作日志
        /// </summary>
        /// <param name="summary">错误描述</param>
        public static void WriteLog(string summary)
        {
            Write(summary, LogType.Operation);
        }

        /// <summary>
        /// 写异常错误日志
        /// </summary>
        /// <param name="desc">错误描述</param>
        /// <param name="exp">异常</param>
        public static void WriteLog(string desc, Exception exp)
        {
            Write(exp == null ? desc : (desc + "。" + exp.Message + "\r\n" + exp.StackTrace), LogType.Exception);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="summary">日志描述</param>
        /// <param name="logType">日志类型</param>
        public static void Write(string summary, LogType logType)
        {
            if (logType == LogType.Operation)
            {
                LogObject.WriteLogToTxt(summary);
            }
            else
            {
                LogObject.WriteErrorLog(summary);
            }
        }
        
        #endregion
    }
}
