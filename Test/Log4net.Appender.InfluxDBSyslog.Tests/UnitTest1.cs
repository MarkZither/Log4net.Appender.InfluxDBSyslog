using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Moq;
using System;
using System.Reflection;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Test
{
    public class UnitTest1
    {
        private static InfluxAppender _appender;
        private static ILog _log;

        public UnitTest1()
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
                Host = "http://localhost:8086"
            };
            appender.ActivateOptions();

            var diagAppender = new TraceAppender
            {
                Layout = layout,
                Name = "InfluxAppenderDiagLogger",
            };
            diagAppender.ActivateOptions();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepository, diagAppender, appender);

            _appender = appender;
            _log = LogManager.GetLogger(typeof(InfluxAppender));
        }

        [Fact]
        public void AppendTest()
        {
            // Arrange            
            Mock<InfluxAppender> mock = new Mock<InfluxAppender>() { CallBase = true };
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            mock.Object.Host = "http://localhost:8086";
            BasicConfigurator.Configure(logRepository, mock.Object);

            var log = LogManager.GetLogger(typeof(InfluxAppender));
            // Act
            log.Info("message");

            //Assert
            Assert.True(true);
        }
    }
}
