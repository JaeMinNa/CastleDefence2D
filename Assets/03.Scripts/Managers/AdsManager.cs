using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public bool IsTestMode; // TestMode 시, 인스펙터에서 클릭
    private LobyController _lobyController;
    private StageController _stageController;
    private RewardedAd _rewardedAd;
    private string _adUnitId;

    public void Init()
    {
        #if UNITY_ANDROID
        if (IsTestMode) _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트용 ID
        else _adUnitId = "ca-app-pub-5906820670754550/8284977605"; // 광고 ID
        #elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544~1458002511";
        #else
        _adUnitId = "unused";
        #endif

        MobileAds.Initialize((InitializationStatus initStatus) => { });

        if (GameManager.I.ScenesManager.CurrentSceneName == "BattleScene0")
        {
            _stageController = GameObject.FindWithTag("StageController").transform.GetComponent<StageController>();
        }
        else if (GameManager.I.ScenesManager.CurrentSceneName == "LobyScene")
        {
            _lobyController = GameObject.FindWithTag("LobyController").transform.GetComponent<LobyController>();
        }
    }

    public void Release()
    {

    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
                RegisterEventHandlers(_rewardedAd);
                ShowRewardedAd();
            });
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // 광고 보상
                if (GameManager.I.ScenesManager.CurrentSceneName == "BattleScene0")
                {
                    _stageController.AdReword();
                    _stageController.IsAd = false;
                }
                else if (GameManager.I.ScenesManager.CurrentSceneName == "LobyScene")
                {
                    GameManager.I.DataManager.GameData.Coin += 10000;
                    _lobyController.Init();
                    _lobyController.IsAd = false;
                    GameManager.I.DataManager.DataSave();
                }
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            //LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardedAd();
        };
    }
}