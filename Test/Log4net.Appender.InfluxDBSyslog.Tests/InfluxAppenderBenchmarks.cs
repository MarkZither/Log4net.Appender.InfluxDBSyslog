using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Xunit;

namespace Log4net.Appender.InfluxDBSyslog.Tests
{
    public class InfluxAppenderBenchmarks
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InfluxAppenderBenchmarks));

        [Params(1000)]
        public int N;

        [GlobalSetup(Targets = new[] { nameof(LogSomethingInfluxWithLayout), nameof(LogSomethingInfluxWithLayoutInterp) })]
        public void SetupWithLayout()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netInfluxWithLayout.config"));
        }

        [Benchmark]
        public void LogSomethingInfluxWithLayout() => log.Error("Error Console");

        [Benchmark]
        public void LogSomethingInfluxWithLayoutInterp() => log.Error($"Error Console{N}");
    }

    [ExcludeFromCodeCoverage]
    public class InfluxAppenderBenchmarkTests : IDisposable
    {
        private bool disposedValue;

        [Fact]
        public void BenchmarkTest()
        {
            var summary = BenchmarkRunner.Run<InfluxAppenderBenchmarks>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~InfluxAppenderBenchmarkTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}