using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UIElements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    const string gameID = "5420273";
    const string bannerID = "Banner_Android";
    const string interstitialID = "Interstitial_Standard";
    const string interstitialSkipableID = "Interstitial_Skipable";
    const string rewardedID = "Rewarded_Android";

    public delegate void DelegateReward();

    public DelegateReward delegateCompleteReward;
    public DelegateReward delegateHalfReward;

    #region Singleton
    public static AdsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion


    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameID, false, this);

    }

    public void SetBanner(bool active)
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        if (active)
        {
            Advertisement.Banner.Show(bannerID);
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }
    
    public void ShowInterstitial(bool skipable)
    {
        if (skipable)
        {
            Advertisement.Show(interstitialSkipableID, this);
        }
        else
        {
            Advertisement.Show(interstitialID, this);
        }
    }

    public void ShowRewarded()
    {
        Advertisement.Show(rewardedID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
       
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == rewardedID)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                delegateCompleteReward();
            }
            else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
            {
                delegateHalfReward();
            }
        }
    }
}
