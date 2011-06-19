using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace XNAHelpers
{
    public class Logger
    {
        public delegate void MessageAddedEventHandler(string message);
        public event MessageAddedEventHandler MessageAdded;

        public StringBuilder Messages { get; private set; }

        /// <summary>{0} = DateTime, {1} = Message</summary>
        public string MessageFormat { get; set; }

        /// <summary>{0} = Message, {1} = Exception</summary>
        public string ExceptionFormat { get; set; }

        /// <summary>{0} = Message, {1} = Elapsed miliseconds</summary>
        public string TimerMessageFormat { get; set; }

        private readonly object thisLock = new object();

        private int saveIndex;

        public Logger()
        {
            Messages = new StringBuilder(1024);
            MessageFormat = "{0:yyyy-MM-dd HH:mm:ss.fff} - {1}";
            ExceptionFormat = "{0}:\r\n{1}";
            TimerMessageFormat = "{0} - {1}ms";
        }

        public void WriteLine(string message = null)
        {
            lock (thisLock)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = string.Format(MessageFormat, FastDateTime.Now, message);
                }

                Messages.AppendLine(message);
                Debug.WriteLine(message);
                OnMessageAdded(message);
            }
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public void WriteException(Exception exception, string message = "Exception")
        {
            WriteLine(string.Format(ExceptionFormat, message, exception));
        }

        public LoggerTimer StartTimer(string startMessage = null)
        {
            if (!string.IsNullOrEmpty(startMessage))
            {
                WriteLine(startMessage);
            }

            return new LoggerTimer(this, TimerMessageFormat);
        }

        public void SaveLog(string filepath)
        {
            lock (thisLock)
            {
                string messages = GetNewMessages();

                if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(messages))
                {
                    string directory = Path.GetDirectoryName(filepath);

                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.AppendAllText(filepath, messages, Encoding.UTF8);

                    saveIndex = Messages.Length;
                }
            }
        }

        private string GetNewMessages()
        {
            if (Messages != null && Messages.Length > 0)
            {
                return Messages.ToString(saveIndex, Messages.Length - saveIndex);
            }

            return null;
        }

        protected void OnMessageAdded(string message)
        {
            if (MessageAdded != null)
            {
                MessageAdded(message);
            }
        }

        public override string ToString()
        {
            return Messages.ToString().Trim();
        }
    }

    public class LoggerTimer
    {
        private Logger logger;
        private string format;
        private Stopwatch timer;

        public LoggerTimer(Logger logger, string format)
        {
            this.logger = logger;
            this.format = format;
            timer = Stopwatch.StartNew();
        }

        public void WriteLineTime(string message = "Timer")
        {
            logger.WriteLine(format, message, timer.ElapsedMilliseconds);
        }
    }
}