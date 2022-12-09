using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace HomaGames.HomaBelly
{
    public class ApplovinMaxBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (HomaBellyManifestConfiguration.TryGetString(out var sdkKeyInManifest, HomaBellyAppLovinMaxConstants.ID,
                    "s_sdk_key"))
            {
                if (sdkKeyInManifest != AppLovinSettings.Instance.SdkKey)
                {
                    Debug.Log("[Homa Belly] Applovin MAX SDK key found in manifest is different from the one found in Applovin Settings. Applying the one from manifest.");
                    AppLovinSettings.Instance.SdkKey = sdkKeyInManifest;
                }
            }
            else
            {
                throw new BuildFailedException("[Homa Belly] Applovin MAX SDK key not found in manifest. Aborting build.");
            }
        }
    }
}