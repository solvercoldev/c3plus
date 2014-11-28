using System;
using System.Security.Principal;


namespace Infrastructure.CrossCutting.Logging
{
    public interface ITraceManager
    {
        /// <summary>
        /// Trace information message to trace repository
        /// <param name="message">Information message to trace</param>
        /// </summary>
        void LogError(string message, Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <param name="url"></param>
        /// <param name="ex"></param>
        void LogError(string message, string user, Uri url, Exception ex);

        /// <summary>
        /// Trace warning message to trace repository
        /// </summary>
        /// <param name="message">Warning message to trace</param>
        /// <param name="typeLog"></param>
        void LogInfo(string message,LogType typeLog );

        /// <summary>
        /// Trace error message to trace repository
        /// </summary>
        /// <param name="message">Error message to trace</param>
        /// <param name="ex"></param>
        void LogWarining(string message, Exception ex);

    }
}