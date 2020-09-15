using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AppCenter = Microsoft.AppCenter.AppCenter;
using AppCenterAnalytics = Microsoft.AppCenter.Analytics.Analytics;
using AppCenterCrashes = Microsoft.AppCenter.Crashes.Crashes;

namespace AnalyticsSender.Sinks
{
    public class AppCenterSink : BaseCustomPropertiesAnalyticsSink
    {
        private IDictionary<string, object> internalProperties = new Dictionary<string, object>();

        private static class InternalPropertyNames
        {
            public const string UserId = "UserId";
            public const string InstallationId = "InstallationId";
        }

        public async Task TurnOnInstallIdSending()
        {
            this.internalProperties[InternalPropertyNames.InstallationId] = await AppCenter.GetInstallIdAsync();
        }

        // IAnalyticsSink
        protected override void SetUserIdInternal(string userId)
        {
            AppCenter.SetUserId(userId);

            // AppCenter.SetUserId does not set user id in analytics data, only in crash reports, so merge this in to properties
            this.internalProperties[InternalPropertyNames.UserId] = userId;
        }

        protected override void TrackEventInternal(string eventName, IDictionary<string, object> properties)
        {
            var propertiesToSend = this.MergePropertiesToAppCenterFormat(properties);
            AppCenterAnalytics.TrackEvent(eventName, propertiesToSend);
        }

        protected override void TrackErrorInternal(Exception exception, IDictionary<string, object> properties)
        {
            var propertiesToSend = this.MergePropertiesToAppCenterFormat(properties);
            AppCenterCrashes.TrackError(exception, propertiesToSend);
        }

        // helpers
        private IDictionary<string, string> MergePropertiesToAppCenterFormat(IDictionary<string, object> properties)
        {
            return PropertyMergerHelper.MergeProperties(properties, this.internalProperties)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString());
        }
    }
}
