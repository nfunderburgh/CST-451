using NLog;

namespace Milestone.Utility
{
    public class MyLogger : Ilogger
    {

        private static MyLogger instance;
        private static Logger logger;

        /// <summary>
        /// The function returns an instance of the MyLogger class, creating a new instance if one does not already exist.
        /// </summary>
        /// <returns>
        /// The instance of the MyLogger class.
        /// </returns>
        public static MyLogger GetInstance()
        {
            if(instance == null)
            {
                instance = new MyLogger();
            }
            return instance;
        }

        /// <summary>
        /// The function returns a logger instance, creating it if it doesn't already exist.
        /// </summary>
        /// <returns>
        /// The method is returning an instance of the Logger class.
        /// </returns>
        private Logger GetLogger()
        {
            if(MyLogger.logger == null)
            {
                MyLogger.logger = LogManager.GetLogger("LoginAppLoggerrule");
            }
            return MyLogger.logger;
        }

        /// <summary>
        /// The Debug function logs a debug message using a logger.
        /// </summary>
        /// <param name="message">The message parameter is a string that represents the debug message that you want to
        /// log.</param>
        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        /// <summary>
        /// The function logs an error message using a logger.
        /// </summary>
        /// <param name="message">The message parameter is a string that represents the error message that you want to
        /// log.</param>
        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        /// <summary>
        /// The Info function logs an informational message using a logger.
        /// </summary>
        /// <param name="message">The parameter "message" is a string that represents the information or message that you
        /// want to log.</param>
        public void Info(string message)
        {
            GetLogger().Info(message);
        }

        /// <summary>
        /// The function "Warning" logs a warning message using a logger.
        /// </summary>
        /// <param name="message">The message parameter is a string that represents the warning message that you want to
        /// log.</param>
        public void Warning(string message)
        {
            GetLogger().Warn(message);
        }
    }
}
