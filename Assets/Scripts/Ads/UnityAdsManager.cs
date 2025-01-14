using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : IUnityAdsLoadListener
{
    public UnityAdsManager()
    {
        if (Instance is not null && Instance != this)
        {
            return;
        }

        Instance = this;
    }
    
    public static UnityAdsManager Instance { get; private set; }
    
    private const int _scoreToReset = 30;
    private int _currentScore;

    #region AdUnits

    private const string AndroidInterstitialAdUnit = "Interstitial_Android";

    #endregion

    public void AddScore()
    {
        _currentScore++;

        if (_currentScore != _scoreToReset) return;
        
        LoadAd();
        _currentScore = 0;
    }

    private void LoadAd()
    {
        Advertisement.Load(AndroidInterstitialAdUnit, this);
    }

    private static void ShowAd(string placementId)
    {
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad loaded");
        ShowAd(placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load ad: {placementId}. Error: {error}. Message: {message}");
    }
}
