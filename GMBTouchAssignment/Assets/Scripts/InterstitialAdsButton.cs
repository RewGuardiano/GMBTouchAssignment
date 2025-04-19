using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    private bool _isAdLoaded = false;

    void Awake()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (_isAdLoaded)
        {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.LogWarning("Ad not loaded yet. Please wait or call LoadAd() first.");
            LoadAd();
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded Successfully: {adUnitId}");
        _isAdLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        _isAdLoaded = false;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        _isAdLoaded = false;
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"Ad Started: {adUnitId}");
    }

    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Completed: {adUnitId} - State: {showCompletionState}");
        _isAdLoaded = false;
        LoadAd();
    }
}