using System;
using System.Collections.Generic;
using System.Text;

namespace Log4net.Appender.InfluxDBSyslog
{
    public class SyslogSeverity
    {
        public string Severity { get; set; }
        public int SeverityCode { get; set; }
    }
}
