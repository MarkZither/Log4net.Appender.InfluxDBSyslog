using InfluxData.Net.InfluxDb.Infrastructure;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Log4net.Appender.InfluxDBSyslog
{
    public class InfluxAppender : AppenderSkeleton
    {
        public string Host { get; set; }
        private HttpClient HttpClient;
        public InfluxAppender()
        {
            HttpClient = new HttpClient();
        }
        public InfluxAppender(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected override async void Append(LoggingEvent loggingEvent)
        {
            InfluxDbClientConfiguration config = new InfluxDbClientConfiguration(
                new Uri(Host),
                string.Empty,
                string.Empty,
                InfluxData.Net.Common.Enums.InfluxDbVersion.Latest, 
                InfluxData.Net.Common.Enums.QueryLocation.FormData,
                HttpClient);
            InfluxData.Net.InfluxDb.InfluxDbClient client = new InfluxData.Net.InfluxDb.InfluxDbClient(config);

            var fields = new Dictionary<string, object>();
            fields.Add("facility_code", 14);
            fields.Add("message", loggingEvent.MessageObject);
            fields.Add("procid", "1234");
            fields.Add("severity_code", 2);
            fields.Add("timestamp", DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1000000);
            fields.Add("version", 1);

            var tags = new Dictionary<string, object>();
            tags.Add("appname", "testapp1");
            tags.Add("facility", "console");
            tags.Add("host", "testhost");
            tags.Add("hostname", "testhost");
            tags.Add("severity", "crit");
            var x = await client.Client.WriteAsync(new InfluxData.Net.InfluxDb.Models.Point()
            {
                Name = "syslog",
                Fields = fields,
                Tags = tags,
                Timestamp = DateTimeOffset.Now.UtcDateTime
            }, "_internal"
            );

            var body = x.Body;
        }
    }
}
