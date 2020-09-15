using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AnalyticsSender.Sinks
{
    public class LoggingSink : BaseCustomPropertiesAnalyticsSink
    {
        public Task Init()
        {
            return Task.CompletedTask;
        }
        public override void SetCustomProperties(IDictionary<string, object> properties)
        {
            base.SetCustomProperties(properties);
            Log("Setting custom properties", properties);
        }

        public override void ClearCustomProperties()
        {
            base.ClearCustomProperties();
            Log("Clearing custom properties");
        }

        public override void SetCustomProperty(string name, object value)
        {
            base.SetCustomProperty(name, value);
            Log($"Setting custom property '{name}' to '{value}'");
        }

        public override void RemoveCustomProperty(string name)
        {
            base.RemoveCustomProperty(name);
            Log($"Removing custom property '{name}'");
        }

        protected override void SetUserIdInternal(string userId)
        {
            Log($"Setting user id to '{userId}'");
        }

        protected override void TrackEventInternal(string eventName, IDictionary<string, object> properties)
        {
            Log($"Tracking event '{eventName}'", properties);
        }

        protected override void TrackErrorInternal(Exception exception, IDictionary<string, object> properties)
        {
            Log($"Tracking error '{exception}'", properties);
        }

        private static void Log(string message, IDictionary<string, object> properties = null)
        {
            string text;
            if (properties == null || properties.Count == 0)
                text = message;
            else
                text = $"{message}, with properties {{{ string.Join(", ", properties.Select(kvp => $"{{'{kvp.Key}': '{kvp.Value}'}}")) }}}";

            Console.WriteLine($"Analytics: {text}");
        }
    }
}
