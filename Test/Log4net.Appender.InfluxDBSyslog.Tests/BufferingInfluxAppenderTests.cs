using log4net;
using log4net.Config;
using log4net.Layout;
using Log4net.Appender.InfluxDBSyslog;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Tests
{
    [ExcludeFromCodeCoverage]
    public class BufferingInfluxAppenderTests : IDisposable
    {
        private MockRepository mockRepository;

        public BufferingInfluxAppenderTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            var layout = new PatternLayout("%.255message");
            layout.ActivateOptions();

            var appender = new BufferingInfluxAppender()
            {
                Name = "InfluxAppender",
                Host = "localhost"
            };
            appender.ActivateOptions();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepository);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private BufferingInfluxAppender CreateBufferingInfluxAppender()
        {
            return new BufferingInfluxAppender();
        }

        [Fact]
        public void AppendTest()
        {
            // Arrange         
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://localhost:8086/*")
                    .Respond(HttpStatusCode.NoContent, "application/json", "{}"); // Respond with JSON
            mockHttp.Fallback.Respond(new HttpClient());
            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            Mock<InfluxAppender> mock = new Mock<InfluxAppender>(client) { CallBase = true };
            mock.Object.Host = "localhost";
            mock.Object.RemotePort = 8086;
            mock.Object.Facility = new Facility("MyTestFacility");
            mock.Object.AppName = new AppName("MyTestApp");

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            BasicConfigurator.Configure(logRepository, mock.Object);

            var log = LogManager.GetLogger(typeof(InfluxAppender));
            // Act
            log.Info("message");

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void AppendFormattedAppNameTest()
        {
            // Arrange         
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://localhost:8086/*")
                    .Respond(HttpStatusCode.NoContent, "application/json", "{}"); // Respond with JSON
            mockHttp.Fallback.Respond(new HttpClient());
            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            Mock<InfluxAppender> mock = new Mock<InfluxAppender>(client) { CallBase = true };
            mock.Object.Host = "localhost";
            mock.Object.RemotePort = 8086;
            mock.Object.Facility = new Facility();
            mock.Object.Facility.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%date MyTestFacility"));
            mock.Object.AppName = new AppName();
            mock.Object.AppName.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%date MyTestApp"));

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            BasicConfigurator.Configure(logRepository, mock.Object);

            var log = LogManager.GetLogger(typeof(InfluxAppender));
            // Act
            log.Info("message");

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void ActivateOptions_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateBufferingInfluxAppender();

            // Act
            unitUnderTest.ActivateOptions();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void PropertyRemotePort_StoresCorrectly()
        {
            // Arrange
            var unitUnderTest = this.CreateBufferingInfluxAppender();

            // Act
            unitUnderTest.RemotePort = 8086;

            // Assert
            Assert.Equal(8086, unitUnderTest.RemotePort);
        }

        [Fact]
        public void PropertyHost_StoresCorrectly()
        {
            // Arrange
            var unitUnderTest = this.CreateBufferingInfluxAppender();

            // Act
            unitUnderTest.Host = "localhost";

            // Assert
            Assert.Equal("localhost", unitUnderTest.Host);
        }

        [Fact]
        public void PropertyScheme_StoresCorrectly()
        {
            // Arrange
            var unitUnderTest = this.CreateBufferingInfluxAppender();

            // Act
            unitUnderTest.Scheme = "https";

            // Assert
            Assert.Equal("https", unitUnderTest.Scheme);
        }
    }
}
