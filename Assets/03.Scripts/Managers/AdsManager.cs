using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public bool IsTestMode; // TestMode ��, �ν����Ϳ��� Ŭ��
    private LobyController _lobyController;
    private StageController _stageController;   
    private RewardedAd _rewardedAd;
    private string _adUnitId;

    public void Init()
    { 
        // ����� ���� SDK�� �ʱ�ȭ��.
        MobileAds.Initialize(initStatus => { });

        //���� �ε� : RewardedAd ��ü�� loadAd�޼��忡 AdRequest �ν��Ͻ��� ����
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd = new RewardedAd(_adUnitId);
        _rewardedAd.LoadAd(request);

        //adUnitId ����
        #if UNITY_ANDROID
        if(IsTestMode) _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // �׽�Ʈ�� ID*/
        else _adUnitId = "ca-app-pub-5906820670754550/8284977605"; // ���� ID*/
        #endif

        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded; // ���� �ε尡 �Ϸ�Ǹ� ȣ��
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad; // ���� �ε尡 �������� �� ȣ��
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening; // ���� ǥ�õ� �� ȣ��(��� ȭ���� ����)
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow; // ���� ǥ�ð� �������� �� ȣ��
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;// ���� ��û�� �� ������ �޾ƾ��� �� ȣ��
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed; // �ݱ� ��ư�� �����ų� �ڷΰ��� ��ư�� ���� ������ ���� ���� �� ȣ��

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