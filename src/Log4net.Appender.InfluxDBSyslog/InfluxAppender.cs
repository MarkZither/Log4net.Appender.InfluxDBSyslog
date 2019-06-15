using log4net.Appender;
using log4net.Core;
using System;

namespace Log4net.Appender.InfluxDBSyslog
{
    public class InfluxAppender : AppenderSkeleton
    {
        public InfluxAppender()
        {
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            throw new NotImplementedException();
        }
    }
}
