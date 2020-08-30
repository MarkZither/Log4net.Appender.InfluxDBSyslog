# Log4net.Appender.InfluxDBSyslog
Log4net appender that posts events to InfluxDB in Syslog format

| Branch   | Status         |
| -------- | -------------- | 
|Master Branch|[![Build status](https://ci.appveyor.com/api/projects/status/fh0wk0ov2f86u1lw/branch/master?svg=true)](https://ci.appveyor.com/project/MarkZither/log4net-appender-influxdbsyslog/branch/master)|
|Dev Branch|[![Build status](https://ci.appveyor.com/api/projects/status/fh0wk0ov2f86u1lw/branch/master?svg=true)](https://ci.appveyor.com/project/MarkZither/log4net-appender-influxdbsyslog/branch/dev)|

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Log4net.Appender.InfluxDBSyslog&metric=alert_status)](https://sonarcloud.io/dashboard?id=Log4net.Appender.InfluxDBSyslog)


## How to setup
### NuGet Package
Install from nuget 
```
PM> Install-Package Log4net.Appender.InfluxDBSyslog
```

or
```
dotnet add package Log4net.Appender.InfluxDBSyslog --version 0.1.0-alpha0020
```

### Configure the appender
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
  </configSections>
  <log4net>
  <appender name="InfluxAppender" type="Log4net.Appender.InfluxDBSyslog.InfluxAppender, Log4net.Appender.InfluxDBSyslog">
    <Scheme>http</Scheme>
    <Host>localhost</Host>
    <RemotePort>8086</RemotePort>
    <AppName>Console Test</AppName>
    <Facility>Console Test Custom Facility</Facility>
  </appender>
    <root>
      <level value="INFO" />
      <appender-ref ref="InfluxAppender" />
    </root>
  </log4net>
</configuration>
```

### Configure the appender with a layout to support properties 
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
  </configSections>
  <log4net>
  <appender name="InfluxAppender" type="Log4net.Appender.InfluxDBSyslog.InfluxAppender, Log4net.Appender.InfluxDBSyslog">
    <Scheme>http</Scheme>
    <Host>localhost</Host>
    <RemotePort>8086</RemotePort>
    <AppName>
        <layout type="log4net.Layout.PatternLayout">
			  <conversionPattern value="%date AppName2 Test" />
		  </layout>
	  </AppName>
    <Facility>Console Test Custom Facility</Facility>
  </appender>
    <root>
      <level value="INFO" />
      <appender-ref ref="InfluxAppender" />
    </root>
  </log4net>
</configuration>
```

### InfluxDB and Chronograf
Download InfluxDB 1.7 from [influxdata.com](https://portal.influxdata.com/downloads/)

Run influxd and chronograf and see your logs flowing in.
![syslog data in Chronograf](docs/img/Chronograf_Syslog_From_Log4net.png)

### Dependencies

    - log4net
    - InfluxData.net

### Building the project

    dotnet build

## Benchmarks

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-2640M CPU 2.80GHz (Sandy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.400-preview-015203
  [Host]     : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT
  DefaultJob : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT
```
|                                        Method |    N |           Mean |         Error |        StdDev |
|---------------------------------------------- |----- |---------------:|--------------:|--------------:|
|                        LogSomethingNoAppender | 1000 |       620.1 ns |       9.76 ns |      16.04 ns ||                                  LogSomething | 1000 | 2,610,454.3 ns | 179,454.60 ns | 514,888.89 ns |
|               LogSomethingRollingFileAppender | 1000 |    16,017.5 ns |     195.63 ns |     163.36 ns |
| LogSomethingRollingFileAppenderNoStringInterp | 1000 |    15,570.0 ns |     243.98 ns |     228.22 ns |
|                  LogSomethingInfluxWithLayout | 1000 |   467,317.2 ns |  12,506.07 ns |    35,477.63 ns |   460,561.2 ns |
|               LogSomethingWithLayoutNoConsole | 1000 |   498,537.5 ns |  20,056.39 ns |    58,187.22 ns |   511,591.6 ns |

## Contribute

If you have any idea for an improvement or found a bug, do not hesitate to open an issue.

## Thanks

[Get Your Syslog On](https://www.influxdata.com/blog/get-your-syslog-on/)  
[Writing Logs Directly to InfluxDB - DZone](https://dzone.com/articles/writing-logs-directly-to-influxdb)


## License

Log4net.Appenders.Fluentd is distributed under MIT License.