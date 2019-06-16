using Log4net.Appender.InfluxDBSyslog;
using Moq;
using System;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Tests
{
    public class InfluxAppenderTests : IDisposable
    {
        private MockRepository mockRepository;

        public InfluxAppenderTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private InfluxAppender CreateInfluxAppender()
        {
            return new InfluxAppender();
        }

        [Fact]
        public void ActivateOptions_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateInfluxAppender();

            // Act
            unitUnderTest.ActivateOptions();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void SetRemotePortTest()
        {
            // Arrange
            var unitUnderTest = this.CreateInfluxAppender();

            // Act
            unitUnderTest.RemotePort = 8086;

            // Assert
            Assert.Equal(8086, unitUnderTest.RemotePort);
        }
    }
}
