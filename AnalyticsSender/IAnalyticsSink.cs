using System;
using System.Collections.Generic;

namespace AnalyticsSender
{
    public interface IAnalyticsSink
    {
        void TrackEvent(string eventName, IDictionary<string, object> properties);
        void TrackError(Exception exception, IDictionary<string, object> properties);

        void SetUserId(string userId);

        void SetCustomProperties(IDictionary<string, object> properties);
        void ClearCustomProperties();

        void SetCustomProperty(string name, object value);
        void RemoveCustomProperty(string name);
    }
}
