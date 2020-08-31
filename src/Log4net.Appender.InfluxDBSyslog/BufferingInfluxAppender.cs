using InfluxData.Net.InfluxDb.Infrastructure;
using log4net.Appender;
using log4net.Core;
using log4net.Util.TypeConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Log4net.Appender.InfluxDBSyslog
{
    public class BufferingInfluxAppender : BufferingAppenderSkeleton
    {
        public string Scheme { get; set; } = "http";
        public string Host { get; set; } = "localhost";
        /// <summary>
        /// Gets or sets the TCP port number of the remote host or multicast group to which 
        /// the underlying <see cref="TcpClient" /> should sent the logging event.
        /// </summary>
        /// <value>
        /// An integer value in the range <see cref="IPEndPoint.MinPort" /> to <see cref="IPEndPoint.MaxPort" /> 
        /// indicating the TCP port number of the remote host or multicast group to which the logging event 
        /// will be sent.
        /// </value>
        /// <remarks>
        /// The underlying <see cref="TcpClient" /> will send messages to this TCP port number
        /// on the remote host or multicast group.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The value specified is less than <see cref="IPEndPoint.MinPort" /> or greater than <see cref="IPEndPoint.MaxPort" />.</exception>
        public int RemotePort
        {
            get { return this._remotePort; }
            set
            {
                if (value < IPEndPoint.MinPort || value > IPEndPoint.MaxPort)
                {
                    throw log4net.Util.SystemInfo.CreateArgumentOutOfRangeException("value", value,
                        "The value specified is less than " +
                        IPEndPoint.MinPort.ToString(NumberFormatInfo.InvariantInfo) +
                        " or greater than " +
                        IPEndPoint.MaxPort.ToString(NumberFormatInfo.InvariantInfo) + ".");
                }
                else
                {
                    this._remotePort = value;
                }
            }
        }
        /// <summary>
        /// The TCP port number of the remote host or multicast group to 
        /// which the logging event will be sent.
        /// </summary>
        private int _remotePort;
        public Facility Facility { get; set; }
        public AppName AppName { get; set; }


        private readonly HttpClient HttpClient;

        public BufferingInfluxAppender()
        {
            ConverterRegistry.AddConverter(typeof(AppName), new ConvertStringToAppName());
            ConverterRegistry.AddConverter(typeof(Facility), new ConvertStringToFacility());
            //https://github.com/dotnet/extensions/issues/1345
            HttpClient = new HttpClient();
        }
        public BufferingInfluxAppender(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }


        /// <summary>
        /// Initialize the appender based on the options set.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is part of the <see cref="IOptionHandler"/> delayed object
        /// activation scheme. The <see cref="ActivateOptions"/> method must 
        /// be called on this object after the configuration properties have
        /// been set. Until <see cref="ActivateOptions"/> is called this
        /// object is in an undefined state and must not be used. 
        /// </para>
        /// <para>
        /// If any of the configuration properties are modified then 
        /// <see cref="ActivateOptions"/> must be called again.
        /// </para>
        /// <para>
        /// The appender will be ignored if no <see cref="Host" /> was specified or 
        /// an invalid remote or local TCP port number was specified.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">The required property <see cref="Host" /> was not specified.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The TCP port number assigned to <see cref="RemotePort" /> is less than <see cref="IPEndPoint.MinPort" /> or greater than <see cref="IPEndPoint.MaxPort" />.</exception>
        public override void ActivateOptions()
        {
            base.ActivateOptions();

            if (this.Host == null)
            {
                throw new ArgumentNullException(this.Host);
            }

            if (this.RemotePort < IPEndPoint.MinPort || this.RemotePort > IPEndPoint.MaxPort)
            {
                throw log4net.Util.SystemInfo.CreateArgumentOutOfRangeException("RemotePort", this.RemotePort,
                    "The RemotePort is less than " +
                    IPEndPoint.MinPort.ToString(NumberFormatInfo.InvariantInfo) +
                    " or greater than " +
                    IPEndPoint.MaxPort.ToString(NumberFormatInfo.InvariantInfo) + ".");
            }
        }

        protected override void SendBuffer(LoggingEvent[] events)
        {
           

            InfluxDbClientConfiguration config = new InfluxDbClientConfiguration(
                new UriBuilder(Scheme, Host, RemotePort).Uri,
                string.Empty,
                string.Empty,
                InfluxData.Net.Common.Enums.InfluxDbVersion.Latest,
                InfluxData.Net.Common.Enums.QueryLocation.FormData,
                HttpClient);
            InfluxData.Net.InfluxDb.InfluxDbClient client = new InfluxData.Net.InfluxDb.InfluxDbClient(config);

            foreach (var loggingEvent in events)
            {
                SyslogSeverity severity = Log4netSyslogSeverityConvertor.GetSyslogSeverity(loggingEvent.Level);

                var fields = new Dictionary<string, object>();
                fields.Add("facility_code", 16);
                fields.Add("message", loggingEvent.MessageObject);
                fields.Add("procid", "1234");
                fields.Add("severity_code", severity.SeverityCode);
                fields.Add("timestamp", DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1000000);
                fields.Add("version", 1);

                var tags = new Dictionary<string, object>();
                AppName.FormatValue(loggingEvent);
                tags.Add("appname", AppName.Value);
                tags.Add("facility", Facility);
                tags.Add("host", Environment.MachineName);
                tags.Add("hostname", Environment.MachineName);
                tags.Add("severity", severity.Severity);
            }
            List<InfluxData.Net.InfluxDb.Models.Point> points = new List<InfluxData.Net.InfluxDb.Models.Point>();
            try
            {
                var apiResp = Task.Run(() => client.Client.WriteAsync(points, "_internal")).GetAwaiter().GetResult();

                if (!apiResp.Success)
                {
                    base.ErrorHandler.Error($"{nameof(InfluxAppender)} Write - {apiResp.Body}");
                }
            }
            catch (Exception ex)
            {
                base.ErrorHandler.Error($"{nameof(InfluxAppender)} Emit - {ex.Message}");
            }
        }
    }
}
