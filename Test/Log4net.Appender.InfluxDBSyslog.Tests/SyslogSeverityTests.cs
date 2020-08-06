using Log4net.Appender.InfluxDBSyslog;
using Moq;
using System;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Tests
{
    public class SyslogSeverityTests : IDisposable
    {
        private MockRepository mockRepository;



        public SyslogSeverityTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private SyslogSeverity CreateSyslogSeverity()
        {
            return new SyslogSeverity();
        }

        [Fact]
        public void PropertySeverity_StoresCorrectly()
        {
            // Arrange
            var unitUnderTest = this.CreateSyslogSeverity();

            // Act
            unitUnderTest.Severity = "crit";

            // Assert
            Assert.Equal("crit", unitUnderTest.Severity);
        }

        [Fact]
        public void PropertySeverityCode_StoresCorrectly()
        {
            // Arrange
            var unitUnderTest = this.CreateSyslogSeverity();

            // Act
            unitUnderTest.SeverityCode = 1;

            // Assert
            Assert.Equal(1, unitUnderTest.SeverityCode);
        }

        [Fact]
        public void GetSyslogSeverity_Should_GetSyslogSeverityFromLog4netLevel()
        {
            // Arrange

            // Act
            var severity = Log4netSyslogSeverityConvertor.GetSyslogSeverity(log4net.Core.Level.Warn);

            // Assert
            Assert.Equal("warning", severity.Severity);
            Assert.Equal(4, severity.SeverityCode);
        }
    }
}
