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
        // 모바일 광고 SDK를 초기화함.
        MobileAds.Initialize(initStatus => { });

        //광고 로드 : RewardedAd 객체의 loadAd메서드에 AdRequest 인스턴스를 넣음
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd = new RewardedAd(_adUnitId);
        _rewardedAd.LoadAd(request);

        //adUnitId 설정
        #if UNITY_ANDROID
        if(IsTestMode) _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트용 ID*/
        else _adUnitId = "ca-app-pub-5906820670754550/8284977605"; // 광고 ID*/
        #endif

        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded; // 광고 로드가 완료되면 호출
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad; // 광고 로드가 실패했을 때 호출
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening; // 광고가 표시될 때 호출(기기 화면을 덮음)
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow; // 광고 표시가 실패했을 때 호출
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;// 광고를 시청한 후 보상을 받아야할 때 호출
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed; // 닫기 버튼을 누르거나 뒤로가기 버튼을 눌러 동영상 광고를 닫을 때 호출

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

    public void HandleRewardedAdLoaded(object sender, EventArgs args) { }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) { }

    public void HandleRewardedAdOpening(object sender, EventArgs args) { }

    public void HandleRewardedAdFailedToShow(object sender, EventArgs args) { }

    public void HandleRewardedAdClosed(object sender, EventArgs args) 
    {
        if(GameManager.I.ScenesManager.CurrentSceneName == "BattleScene0")
        {
            _stageController.AdReword();
        }
        else if(GameManager.I.ScenesManager.CurrentSceneName == "LobyScene")
        {
            GameManager.I.DataManager.GameData.Coin += 10000;
            _lobyController.Init();
            GameManager.I.DataManager.DataSave();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args) { }

    public void ShowAds()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }

}