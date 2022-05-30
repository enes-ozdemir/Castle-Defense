using GoogleMobileAds.Api;
using System;
using Script.GameManagerScripts;
using Script.GameSceneScripts;
using Script.LevelSceneScripts;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private readonly string adUnitId = "ca-app-pub-9430454817447319/5608069181";
    private static readonly string bannerAdUnitId = "ca-app-pub-9430454817447319/8087930279";
    private RewardedAd extraGoldAd;
    private RewardedAd extraDiamondAd;
    private RewardedAd doubleEarningsRewardedAd;
    public static RewardedAd itemRewardedAd;
    public static SellableItem currentItemReward;

    private static BannerView bannerView;

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
        itemRewardedAd = RequestRewardBasedVideo(GetItemReward);
    }

    public static void LoadBannerAd()
    {
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, 320, 100);
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public static void HideBannerAd()
    {
        if (bannerView != null) bannerView.Destroy();
    }

    public RewardedAd RequestRewardBasedVideo(EventHandler<Reward> rewardCallback)
    {
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
        Debug.Log("User received gold " + GameManager.currentLevel + 1 * 150);
        GameManager.money += GameManager.currentLevel + 1 * 150;
    }

    private static void GetItemReward(object sender, Reward args)
    {
        Debug.Log("User received an item" + currentItemReward.itemName);
        if (currentItemReward != null)
        {
            ShopItem.SetRewardItem(currentItemReward);
        }
        else
        {
            Debug.Log("current Item is null");
        }
    }


    private void ReceiveDiamond(object sender, Reward args)
    {
        Debug.Log("User received diamond " + 25);

        GameManager.diamond += 25;
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

    public static void ShowItemRewardAd()
    {
        if (itemRewardedAd.IsLoaded())
        {
            itemRewardedAd.Show();
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