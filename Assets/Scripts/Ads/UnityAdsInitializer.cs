using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class UnityAdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string _androidGameId = "5628824";
        private const bool _testMode = false;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_androidGameId, _testMode, this);
            }

            var unityAdsManager = new UnityAdsManager();
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity ads initialized!");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"error: {error} \nmessage: {message}");
        }
    }
}