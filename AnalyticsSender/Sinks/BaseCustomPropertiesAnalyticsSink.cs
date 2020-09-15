using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AnalyticsSender.Sinks
{
    // base class for analytics sinks that don't support custom properties -
    // merges any event properties with any custom properties.
    public abstract class BaseCustomPropertiesAnalyticsSink : IAnalyticsSink
    {
        private IDictionary<string, object> CustomProperties = new Dictionary<string, object>();

        public virtual void SetCustomProperties(IDictionary<string, object> properties)
        {
            this.CustomProperties = new Dictionary<string, object>(properties);
        }

        public virtual void ClearCustomProperties()
        {
            this.CustomProperties.Clear();
        }

        public virtual void SetCustomProperty(string name, object value)
        {
            this.CustomProperties[name] = value;
        }

        public virtual void RemoveCustomProperty(string name)
        {
            this.CustomProperties.Remove(name);
        }

        public virtual void TrackEvent(string eventName, IDictionary<string, object> properties)
        {
            IDictionary<string, object> allProperties = PropertyMergerHelper.MergeProperties(this.CustomProperties, properties);

            this.TrackEventInternal(eventName, allProperties);
        }

        public virtual void TrackError(Exception exception, IDictionary<string, object> properties)
        {
            IDictionary<string, object> allProperties = PropertyMergerHelper.MergeProperties(this.CustomProperties, properties);

            this.TrackErrorInternal(exception, allProperties);
        }

        public virtual void SetUserId(string userId)
        {
            this.SetUserIdInternal(userId);
        }

        // implement these
        protected abstract void TrackEventInternal(string eventName, IDictionary<string, object> properties);
        protected abstract void TrackErrorInternal(Exception exception, IDictionary<string, object> properties);

        protected abstract void SetUserIdInternal(string userId);

        // helpers
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static class PropertyMergerHelper
        {
            public static IDictionary<string, object> MergeProperties(IDictionary<string, object> properties, IDictionary<string, object> toMergeIn)
            {
                if (properties == null && toMergeIn == null)
                    return new Dictionary<string, object>();

                IDictionary<string, object> mergedProperties = new Dictionary<string, object>(properties);
                if (toMergeIn != null)
                {
                    foreach (var property in toMergeIn)
                        mergedProperties[property.Key] = property.Value;
                }
                return mergedProperties;
            }
        }
    }
}
