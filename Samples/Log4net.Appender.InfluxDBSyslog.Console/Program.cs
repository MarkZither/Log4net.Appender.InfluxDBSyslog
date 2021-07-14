using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using log4net;
using log4net.Config;
using log4net.Util.TypeConverters;
using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

namespace Log4net.Appender.InfluxDBSyslog.ConsoleTest
{
    public class LogALot
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogALot));

        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingInfluxWithLayout) })]
        public void SetupWithLayout()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netInfluxWithLayout.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingBuffering1InfluxWithLayout) })]
        public void SetupWithBuffering1Layout()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netBufferingInfluxWithLayoutBuffer1.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingBuffering5InfluxWithLayout) })]
        public void SetupWithBuffering5Layout()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netBufferingInfluxWithLayoutBuffer5.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingBuffering10InfluxWithLayout) })]
        public void SetupWithBuffering10Layout()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netBufferingInfluxWithLayoutBuffer10.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingWithLayoutNoConsole) })]
        public void SetupWithLayoutNoConsole()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netWithLayoutNoConsole.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingNoAppender) })]
        public void SetupNoAppender()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netNoAppender.config"));
        }

        [GlobalSetup(Targets = new[] { nameof(LogSomethingRollingFileAppender), nameof(LogSomethingRollingFileAppenderNoStringInterp) })]
        public void SetupRollingFileAppender()
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4netRollingFileAppender.config"));
        }

        public LogALot()
        {
        }

        [Benchmark]
        public void LogSomethingNoAppender() => log.Error($"Error {N}");
        [Benchmark]
        public void LogSomething() => log.Error($"Error Console {N}");
        [Benchmark]
        public void LogSomethingRollingFileAppender() => log.Error($"Error File {N}");
        [Benchmark]
        public void LogSomethingRollingFileAppenderNoStringInterp() => log.Error("Error File");

        [Benchmark]
        public void LogSomethingInfluxWithLayout() => log.Error($"Error Console {N}");

        [Benchmark]
        public void LogSomethingWithLayoutNoConsole() => log.Error($"Error Console {N} ");

        [Benchmark]
        public void LogSomethingBuffering1InfluxWithLayout() => log.Error($"Error Console {N}");

        [Benchmark]
        public void LogSomethingBuffering5InfluxWithLayout() => log.Error($"Error Console {N}");

        [Benchmark]
        public void LogSomethingBuffering10InfluxWithLayout() => log.Error($"Error Console {N}");
    }

    public class Program
    {
        protected Program()
        { }

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            log.Error("Error Console");
            log.Debug("Debug Console");

#if RELEASE
            var summary = BenchmarkRunner.Run<LogALot>();
#endif
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            GlobalContext.Properties["Environment"] = "envTestTestTest";
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));

            Console.WriteLine("Hello World!");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            Thread.Sleep(50);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
