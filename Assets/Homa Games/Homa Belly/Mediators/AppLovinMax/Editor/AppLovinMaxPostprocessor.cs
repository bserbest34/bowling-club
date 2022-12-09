using System;
using UnityEditor;

namespace HomaGames.HomaBelly
{
    /// <summary>
    /// Creates the configuration json file for AppLovin MAX
    /// </summary>
    public class AppLovinMaxPostprocessor
    {
        [InitializeOnLoadMethod]
        static void Configure()
        {
            if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
                return;

            HomaBellyEditorLog.Debug($"Configuring {HomaBellyAppLovinMaxConstants.ID}");

            if (!HomaBellyManifestConfiguration.TryGetString(out var sdkKey, HomaBellyAppLovinMaxConstants.ID,
                    "s_sdk_key") || string.IsNullOrEmpty(sdkKey)) return;
            
            // Applovin Ad Review feature
            try
            {
                AppLovinSettings.Instance.SdkKey = sdkKey;
            }
            catch (Exception e)
            {
                HomaBellyEditorLog.Error($"Could not set SDK Key for Applovin Ad Review: {e.Message}");
            }

            // Disabling auto refresh to prevent preview cache errors (see https://forum.unity.com/threads/clearpreviewcache-error.714896/ )
            AssetDatabase.DisallowAutoRefresh();
            EditorApplication.delayCall += delegate
            {
                AssetDatabase.AllowAutoRefresh();
                AssetDatabase.Refresh();
            };
        }
    }
}