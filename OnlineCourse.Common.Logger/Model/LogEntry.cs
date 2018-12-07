using System;
using System.Collections.Generic;
using System.Text;
using Validation;

namespace OnlineCourse.Common.Logger.Model
{
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {

            Requires.Defined(severity, "severity");
            Requires.NotNullOrEmpty(message, "message");

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
