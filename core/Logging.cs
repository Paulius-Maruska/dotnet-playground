using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class Logging
    {
        #region (public) types

        public enum Priority
        {
            Debug = 0,
            Info = 1,
            Warning = 2,
            Error = 3
        }

        #endregion

        #region (private, static) console helpers

        private static object consoleLock = new object();

        private static void WriteConsoleMessage(Priority priority, String source, String message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(DateTime.UtcNow.ToString("o"));

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("][");

                switch (priority)
                {
                    case Priority.Debug:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("DBG");
                        break;
                    case Priority.Info:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("INF");
                        break;
                    case Priority.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("WRN");
                        break;
                    case Priority.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("ERR");
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("] ");

                if (!String.IsNullOrEmpty(source))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("{");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(source);

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("} ");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(message);

                Console.ResetColor();
                Console.WriteLine();
            }
        }

        #endregion

        #region (private) fields

        private String _source;

        #endregion

        #region (public) constructors

        public Logging(String source)
        {
            this._source = source;
        }

        public Logging()
            : this("")
        { }

        public Logging Sub(String subSource)
        {
            String source = "";
            if (!String.IsNullOrEmpty(subSource))
            {
                source = subSource;
                if (!String.IsNullOrEmpty(_source))
                {
                    source = String.Format("{0}.{1}", _source, subSource);
                }
            }
            else
            {
                source = _source;
            }
            return new Logging(source);
        }

        public Logging Sub()
        {
            return Sub("");
        }

        #endregion

        #region (public) main logging functions

        public void LogMessage(Priority priority, String source, String message)
        {
            WriteConsoleMessage(priority, source, message);
        }

        public void LogMessage(Priority priority, String message)
        {
            LogMessage(priority, _source, message);
        }

        #endregion

        #region (public) helper logging functions

        public void Debug(String source, String message)
        {
            LogMessage(Priority.Debug, source, message);
        }

        public void Debug(String message)
        {
            Debug(_source, message);
        }

        public void Info(String source, String message)
        {
            LogMessage(Priority.Info, source, message);
        }

        public void Info(String message)
        {
            Info(_source, message);
        }

        public void Warning(String source, String message)
        {
            LogMessage(Priority.Warning, source, message);
        }

        public void Warning(String message)
        {
            Warning(_source, message);
        }

        public void Error(String source, String message)
        {
            LogMessage(Priority.Error, source, message);
        }

        public void Error(String message)
        {
            Error(_source, message);
        }

        #endregion
    }
}
