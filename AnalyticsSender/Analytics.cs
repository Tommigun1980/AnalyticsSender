using System;
using System.Collections.Generic;

namespace AnalyticsSender
{
    public static class Analytics
    {
        private static HashSet<IAnalyticsSink> Sinks = new HashSet<IAnalyticsSink>();

        // set ids
        public static void SetUserId(string userId)
        {
            foreach (var sink in Sinks)
                sink.SetUserId(userId);
        }

        // send events
        public static void TrackEvent(string eventName, IDictionary<string, object> properties = null)
        {
            foreach (var sink in Sinks)
                sink.TrackEvent(eventName, properties);
        }

        public static void TrackError(Exception exception, IDictionary<string, object> properties = null)
        {
            foreach (var sink in Sinks)
                sink.TrackError(exception, properties);
        }

        // custom properties
        public static void SetCustomProperties(IDictionary<string, object> properties)
        {
            foreach (var sink in Sinks)
                sink.SetCustomProperties(properties);
        }

        public static void ClearCustomProperties()
        {
            foreach (var sink in Sinks)
                sink.ClearCustomProperties();
        }

        public static void SetCustomProperty(string name, object value)
        {
            foreach (var sink in Sinks)
                sink.SetCustomProperty(name, value);
        }

        public static void RemoveCustomProperty(string name)
        {
            foreach (var sink in Sinks)
                sink.RemoveCustomProperty(name);
        }

        // sink management
        public static void AddSink(IAnalyticsSink sink)
        {
            Sinks.Add(sink);
        }

        public static void RemoveSink(IAnalyticsSink sink)
        {
            Sinks.Remove(sink);
        }

        public static IEnumerable<IAnalyticsSink> GetSinks()
        {
            return Sinks;
        }
    }
}
