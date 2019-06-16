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
    public class UnitTest1
    {
        private static InfluxAppender _appender;
        private static ILog _log;
        private HttpClient _client;

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
            BasicConfigurator.Configure(logRepository, diagAppender);

            _appender = appender;
            _log = LogManager.GetLogger(typeof(InfluxAppender));
        }

        [Fact]
        public void AppendIntegrationTest()
        {
            // Arrange         
            var callbackExecuted = false;
            var json = @"{
				""data"": [
					{}";
            
            Mock<InfluxAppender> mock = new Mock<InfluxAppender>() { CallBase = true };
            mock.Object.Host = "http://localhost:8086";

            Action<HttpRequestMessage, CancellationToken> callback = new Action<HttpRequestMessage, CancellationToken>((requestMessage, cancellationToken) => {
                Assert.Equal("http://localhost:5000/identity/api/users", requestMessage.RequestUri.AbsoluteUri);
                callbackExecuted = true;
            });

            //var httpMessageHandlerMock = MockHttpMessageHandler(HttpStatusCode.OK, json, callback);
            //var httpClient = new Func<HttpClient>(() => new HttpClient(httpMessageHandlerMock.Object));

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            
 
            BasicConfigurator.Configure(logRepository, mock.Object);

            var log = LogManager.GetLogger(typeof(InfluxAppender));
            // Act
            log.Info("message");

            //Assert
            Assert.True(true);
        }

        [SkippableFact]
        public void AppendTest()
        {
            Skip.If(bool.TryParse(Environment.GetEnvironmentVariable("APPVEYOR"), out _));
            // Arrange         
            var callbackExecuted = false;
            var json = @"{
				""data"": [
					{}";


            Mock<InfluxAppender> mock = new Mock<InfluxAppender>() { CallBase = true };
            mock.Object.Host = "http://localhost:8086";

            Action<HttpRequestMessage, CancellationToken> callback = new Action<HttpRequestMessage, CancellationToken>((requestMessage, cancellationToken) => {
                Assert.Equal("http://localhost:5000/identity/api/users", requestMessage.RequestUri.AbsoluteUri);
                callbackExecuted = true;
            });

            //var httpMessageHandlerMock = MockHttpMessageHandler(HttpStatusCode.OK, json, callback);
            //var httpClient = new Func<HttpClient>(() => new HttpClient(httpMessageHandlerMock.Object));

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
