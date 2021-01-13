# AnalyticsSender

NuGet package available at https://www.nuget.org/packages/AnalyticsSender/

App Center analytics sink package available at https://www.nuget.org/packages/AnalyticsSender.Sinks.AppCenterAnalyticsSink/

## Intro

AnalyticsSender is a light-weight sink (plugin) based analytics sender.
The base package contains the SDK and a logging sink, and an App Center analytics sink is available in a separate package.

This package solves two problems:
1) Often you want to send your analytics data to several destinations (local log, App Center, Google Analytics, Facebook Analytics etc.). AnalyticsSender's sink based API requires only one call to send analytics events to all destinations.

2) The App Center SDK doesn't have support for custom properties. This package's App Center sink adds this missing functionality.

## Usage

# API

```c#
using AnalyticsSender;

// add logging sink. optional.
Analytics.AddSink(Sinks.LoggingSink());

// add app center sink. optional.
var appCenterSink = new Sinks.AppCenterSink();
appCenterSink.TurnOnInstallIdSending(); // optional
Analytics.AddSink(appCenterSink);

// set user id - gets merged in with event properties. optional
Analytics.SetUserId("whatever");

// manipulate custom properties - these stick until modified again, and are merged into your events. optional.
Analytics.SetCustomProperties(someDictionary);
Analytics.SetCustomProperty("propertyName", someValue)
Analytics.RemoveCustomProperty("propertyName")
Analytics.ClearCustomProperties();

// send analytics (optionalPropertyDictionary and custom properties are merged in to event)
Analytics.TrackEvent("EventName");
Analytics.TrackEvent("EventName", optionalPropertyDictionary);

Analytics.TrackError("EventName");
Analytics.TrackError("EventName", optionalPropertyDictionary);
```

## Extending

Custom analytics sinks can be easily added by implementing `IAnalyticsSink` or extending `BaseCustomPropertiesAnalyticsSink.cs`.
