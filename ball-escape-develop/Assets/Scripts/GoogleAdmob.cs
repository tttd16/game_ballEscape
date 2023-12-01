
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections;

public class GoogleAdmob : MonoBehaviour
{
    public static GoogleAdmob Instance;

    //ID for Banner:
    string adUnitIdBanner;

    //ID for Interstitial:
    string adUnitIdInterstitial;

    //ID for Rewarded:
    string adUnitIdRewarded;
    public bool IsUnlockSkin;

    [SerializeField] private bool isTesting;
    
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewarded;
    public bool bannerIsShow= false;
    private static bool isInitialize = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        //ID for Banner:
#if UNITY_ANDROID
        adUnitIdBanner = "ca-app-pub-8245978264145318/4411311006";
#elif UNITY_IPHONE
        adUnitIdBanner = "ca-app-pub-8245978264145318/9984240573";
#else
        adUnitIdBanner = "unexpected_platform";
#endif


        //ID for Interstitial:
#if UNITY_ANDROID
        adUnitIdInterstitial = "ca-app-pub-8245978264145318/4028167621";
#elif UNITY_IPHONE
        adUnitIdInterstitial = "ca-app-pub-8245978264145318/3050348327";
#else
        adUnitIdInterstitial = "unexpected_platform";
#endif



        //ID for Rewarded:
#if UNITY_ANDROID
        adUnitIdRewarded = "ca-app-pub-8245978264145318/2513984676";
#elif UNITY_IPHONE
        adUnitIdRewarded = "ca-app-pub-8245978264145318/5729300799";
#else
        adUnitIdRewarded = "unexpected_platform";
#endif

        if (isTesting)
        {
            adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111"; 
            adUnitIdInterstitial = "ca-app-pub-3940256099942544/1033173712";
            adUnitIdRewarded = "ca-app-pub-3940256099942544/5224354917";
        }

        if (isInitialize)
            return;

        MobileAds.Initialize(initStatus => { });

        isInitialize = true;
    }

    private void Start()
    {
        RequestInterstitial();
        RequestRewarded();
        RequestBanner();
    }

    public void ShowBanner()
    {
        if (Utils.ADREMOVE)
            return;
        bannerView?.Show();
        bannerIsShow = true;
    }
    public void changeBannerPosTop()
    {
        bannerView.SetPosition(AdPosition.Top);
    }
    public void ShowInterstitial()
    {
       
        if (Utils.ADREMOVE)
            return;
        if (interstitial.CanShowAd())
        {
            interstitial.Show();
            Debug.Log(" show Interstitial");
        }
        else
        {
           Debug.Log("admob not loaded");
        }
        RequestInterstitial();
    }

  
    public void ShowRewarded(bool unlockskin)
    {
       
        IsUnlockSkin = unlockskin;
        if (rewarded.CanShowAd())
        {
            rewarded.Show();
            //Debug.Log("show reward");
        }
        else
        {
            Debug.Log("admob not loaded");
        }
    }
    

    void RequestBanner()
    {
        if (Utils.ADREMOVE)
            return;
        bannerView = new BannerView(adUnitIdBanner, AdSize.SmartBanner, AdPosition.Bottom);
        bannerView.LoadAd(new AdRequest.Builder().Build());
        bannerView.Hide();
    }
    void RequestInterstitial()
    {
        if (Utils.ADREMOVE)
            return;
        Debug.Log("Requesting Interstitial");
        interstitial = new InterstitialAd(adUnitIdInterstitial);
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        interstitial.OnAdFailedToLoad += HandleFailedToLoad;
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitial.LoadAd(adRequest);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }
    public void HandleFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        LoadAdError loadAdError = args.LoadAdError;

        // Gets the domain from which the error came.
        string domain = loadAdError.GetDomain();

        // Gets the error code. See
        // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
        // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
        // for a list of possible codes.
        int code = loadAdError.GetCode();

        // Gets an error message.
        // For example "Account not approved yet". See
        // https://support.google.com/admob/answer/9905175 for explanations of
        // common errors.
        string message = loadAdError.GetMessage();

        // Gets the cause of the error, if available.
        AdError underlyingError = loadAdError.GetCause();

        // All of this information is available via the error's toString() method.
        Debug.Log("Load error string: " + loadAdError.ToString());

        // Get response information, which may include results of mediation requests.
        ResponseInfo responseInfo = loadAdError.GetResponseInfo();
        Debug.Log("Response info: " + responseInfo.ToString());
    }

    void RequestRewarded()
    {
        rewarded = new RewardedAd(adUnitIdRewarded);
        rewarded.OnAdLoaded += HandleOnAdLoaded;
        rewarded.OnUserEarnedReward += HandleUserEarnedReward;
        rewarded.OnAdClosed += HandleRewardedAdClosed;
        rewarded.OnAdFailedToLoad += HandleFailedToLoad;

        AdRequest adRequest = new AdRequest.Builder().Build();
        rewarded.LoadAd(adRequest);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewarded();
        
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        StartCoroutine(GivePlayerRewardCo());
    }
    private IEnumerator GivePlayerRewardCo()
    {
        yield return new WaitForEndOfFrame();
        if (!IsUnlockSkin)
        {
            GameManager.instance.CompleteLevel();
        }
        else
        {
            SkinManager.instance.RewardSkin();
        }
        


    }


    public void RemoveAds()
    {
        Utils.RemoveAds();
        bannerView.Destroy();
    }
} 
