using OnlineCourse.Common.Logger.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Common.Logger.Interface
{
    public interface ILog
    {
        void Log(LogEntry entry);
    }
}
