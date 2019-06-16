# Log4net.Appender.InfluxDBSyslog
Log4net appender that posts events to InfluxDB in Syslog format

| Branch   | Status         |
| -------- | -------------- | 
|Master Branch|[![Build status](https://ci.appveyor.com/api/projects/status/dvouxggnqka0ptso/branch/master?svg=true)](https://ci.appveyor.com/project/MarkZither/Log4net.Appender.InfluxDBSyslog/branch/master)|
|Dev Branch|[![Build status](https://ci.appveyor.com/api/projects/status/dvouxggnqka0ptso/branch/dev?svg=true)](https://ci.appveyor.com/project/MarkZither/Log4net.Appender.InfluxDBSyslog/branch/dev)|

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

### InfluxDB and Chronograf
Download InfluxDB 1.7 from [influxdata.com](https://portal.influxdata.com/downloads/)

Run influxd and chronograf and see your logs flowing in.
![syslog data in Chronograf](docs/img/Chronograf_Syslog_From_Log4net.png)

### Dependencies

    - log4net
    - InfluxData.net

### Building the project

    dotnet build

## Contribute

If you have any idea for an improvement or found a bug, do not hesitate to open an issue.

## Thanks

[Get Your Syslog On](https://www.influxdata.com/blog/get-your-syslog-on/)  
[Writing Logs Directly to InfluxDB - DZone](https://dzone.com/articles/writing-logs-directly-to-influxdb)


## License

Log4net.Appenders.Fluentd is distributed under MIT License.