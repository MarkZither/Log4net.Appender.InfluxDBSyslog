using log4net;
using log4net.Config;
using System;
using System.Reflection;

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
            log.Error("Hello Console");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
