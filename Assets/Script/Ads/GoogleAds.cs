using GoogleMobileAds.Api;
using System;
using Script.GameManagerScripts;
using Script.GameSceneScripts;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private readonly string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd extraGoldAd;
    private RewardedAd extraDiamondAd;
    private RewardedAd doubleEarningsRewardedAd;

    [SerializeField] private GameUIManager gameUIManager;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        extraGoldAd = RequestRewardBasedVideo(ReceiveGold);
        extraDiamondAd = RequestRewardBasedVideo(ReceiveDiamond);
        doubleEarningsRewardedAd = RequestRewardBasedVideo(DoubleEarning);
    }

    public RewardedAd RequestRewardBasedVideo(EventHandler<Reward> rewardCallback)
    {
        Debug.Log("Video requested");
        rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += rewardCallback;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request);

        return rewardedAd;
    }

    private void ReceiveGold(object sender, Reward args)
    {
        Debug.Log("User received gold " + GameManager.currentLevel + 1 * 200);
        GameManager.money += GameManager.currentLevel + 1 * 200;
    }

    private void ReceiveDiamond(object sender, Reward args)
    {
        Debug.Log("User received diamond " + GameManager.currentLevel + 1 * 200);

        GameManager.diamond += GameManager.currentLevel + 1 * 20;
    }

    private void DoubleEarning(object sender, Reward args)
    {
        Debug.Log("User received diamond " + GameManager.currentLevel + 1 * 200);

        GameManager.diamond += gameUIManager.tempDiamond;
        GameManager.money += gameUIManager.tempMoney;

        gameUIManager.tempDiamond *= 2;
        gameUIManager.tempMoney *= 2;
        gameUIManager.rewardObject.SetActive(false);
    }

    public void ShowReceiveGoldVideoRewardAd()
    {
        if (extraGoldAd.IsLoaded())
        {
            extraGoldAd.Show();
        }
    }

    public void ShowDoubleRewardVideoRewardAd()
    {
        if (doubleEarningsRewardedAd.IsLoaded())
        {
            doubleEarningsRewardedAd.Show();
        }
    }

    public void ShowExtraDiamondVideoRewardAd()
    {
        if (extraDiamondAd.IsLoaded())
        {
            extraDiamondAd.Show();
        }
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded");
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad" + args.LoadAdError);
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening");
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow" + args.Message);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed");
    }
}