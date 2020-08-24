using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Test
{
    public class SkippableInfluxAppenderTests
    {
        private static InfluxAppender _appender;
        private static ILog _log;

        public SkippableInfluxAppenderTests()
        {
            CreateAppender();
        }

        static void CreateAppender()
        {
            var layout = new PatternLayout("%.255message");
            layout.ActivateOptions();

            var appender = new InfluxAppender()
            {
                Name = "InfluxAppender",
                Host = "localhost"
            };
            appender.ActivateOptions();

            var diagAppender = new TraceAppender
            {
                Layout = layout,
                Name = "InfluxAppenderDiagLogger",
            };
            diagAppender.ActivateOptions();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepository, diagAppender);

            _appender = appender;
            _log = LogManager.GetLogger(typeof(InfluxAppender));
        }

        [SkippableFact]
        public void AppendIntegrationTest()
        {
            Skip.If(bool.TryParse(Environment.GetEnvironmentVariable("APPVEYOR"), out _));
            // Arrange      

            Mock<InfluxAppender> mock = new Mock<InfluxAppender>() { CallBase = true };
            mock.Object.Host = "localhost";
            mock.Object.RemotePort = 8086;
            mock.Object.Facility = "App";
            mock.Object.AppName = new AppName("MyTestApp");

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            
            BasicConfigurator.Configure(logRepository, mock.Object);

            var log = LogManager.GetLogger(typeof(InfluxAppender));
            // Act
            log.Info("message");

            //Assert
            Assert.True(true);
        }
    }
}
