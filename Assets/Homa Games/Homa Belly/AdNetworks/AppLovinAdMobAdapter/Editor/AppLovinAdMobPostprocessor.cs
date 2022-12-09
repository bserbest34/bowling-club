using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace HomaGames.HomaBelly
{
    /// <summary>
    /// Creates the configuration json file for AppLovin AdMob Adapter
    ///
    /// See: https://dash.applovin.com/documentation/mediation/unity/mediation-adapters?network=ADMOB_NETWORK
    /// </summary>
    public class AppLovinAdMobPostprocessor : IPreprocessBuildWithReport
    {
        [InitializeOnLoadMethod]
        static void Configure()
        {
            if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
                return;

            HomaBellyEditorLog.Debug($"Configuring AppLovin AdMob Ad Network");

            if (HomaBellyManifestConfiguration.TryGetString(out var androidAppId, AppLovinAdMobAdapterConstants.ID,
                    "s_android_app_id") || string.IsNullOrEmpty(androidAppId))
            {
                try
                {
                    AppLovinSettings.Instance.AdMobAndroidAppId = androidAppId;
                    HomaBellyEditorLog.Debug($"AppLovin Android AdMob Ad Network configured");
                }
                catch (Exception e)
                {
                    HomaBellyEditorLog.Error(
                        $"Could not replace ADMOB_APP_ID: {e.Message}. Please visit https://dash.applovin.com/documentation/mediation/unity/mediation-adapters?network=ADMOB_NETWORK");
                }
            }

            if (HomaBellyManifestConfiguration.TryGetString(out var iosAppId, AppLovinAdMobAdapterConstants.ID,
                    "s_ios_app_id") || string.IsNullOrEmpty(iosAppId))
            {
                try
                {
                    AppLovinSettings.Instance.AdMobIosAppId = iosAppId;
                    HomaBellyEditorLog.Debug($"AppLovin Android AdMob Ad Network configured");
                }
                catch (Exception e)
                {
                    HomaBellyEditorLog.Error(
                        $"Could not replace ADMOB_APP_ID: {e.Message}. Please visit https://dash.applovin.com/documentation/mediation/unity/mediation-adapters?network=ADMOB_NETWORK");
                }
            }
        }

        public int callbackOrder => -1;
        public void OnPreprocessBuild(BuildReport report)
        {
            Configure();
        }
    }
}