using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Threading;

namespace Log4net.Appender.InfluxDBSyslog.ConsoleTest
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            // Set up a simple configuration that logs on the console.
            // Thanks Stackify https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            //BasicConfigurator.Configure(logRepository);
            Console.WriteLine("Hello World!");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            Thread.Sleep(50);
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            Thread.Sleep(500);
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            Thread.Sleep(150);
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            Thread.Sleep(450);
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            Thread.Sleep(250);
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            Thread.Sleep(150);
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            Thread.Sleep(500);
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            log.Error("Hello Console");
            Thread.Sleep(250);
            log.Error("Error Console");
            log.Debug("Debug Console");
            log.Warn("Warn Console");
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            Thread.Sleep(150);
            log.Error("Hello Console");
            log.Error("Error Console");
            log.Debug("Debug Console");
            Thread.Sleep(750);
            log.Warn("Warn Console");
            log.Info("Info Console");
            log.Fatal("Fatal Console");
            Thread.Sleep(50);
            log.Error("Hello Console");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
