using log4net.Core;
using System.Diagnostics.CodeAnalysis;

namespace Log4net.Appender.InfluxDBSyslog
{

#pragma warning disable S101 // Types should be named in PascalCase
    [SuppressMessage(
 "not my naming convention",
 "S101:Types should be named in PascalCase"
)]
    internal static class Log4netSyslogSeverityConvertor
#pragma warning restore S101 // Types should be named in PascalCase
    {
        internal static SyslogSeverity GetSyslogSeverity(Level level)
        {
            switch (level.Name)
            {
                case "FATAL":
                    return new SyslogSeverity() { Severity = "emerg", SeverityCode = 0 };

                case "ERROR":
                    return new SyslogSeverity() { Severity = "err", SeverityCode = 3 };

                case "WARN":
                    return new SyslogSeverity() { Severity = "warning", SeverityCode = 4 };

                case "INFO":
                    return new SyslogSeverity() { Severity = "info", SeverityCode = 4 };

                case "DEBUG":
                    return new SyslogSeverity() { Severity = "debug", SeverityCode = 6 };

                default:
                    return new SyslogSeverity() { Severity = "notice", SeverityCode = 7 };
            }
        }
    }
}