using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    private const string MaxSdkKey =
        "TatA_rfIz3EQ1Wt4N7lFw-ntTNTTmL_sX9wW1tD9yTyhMqseZZbs3yTH9-GT01WEgGXmerWAHb59xJRNdTnTzD";

    private const string InterstitialAdUnitId = "2b86fcec3d12c0f5";
    private const string RewardedAdUnitId = "c8cd1fa2164d63e7";
    private const string RewardedInterstitialAdUnitId = "ENTER_REWARD_INTER_AD_UNIT_ID_HERE";
    private const string BannerAdUnitId = "c8c5f8609dabd156";
    private const string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";

    public bool MoneyReward;
    public bool SceneReward;

    private bool isBannerShowing;
    private bool isMRecShowing;

    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    private int rewardedInterstitialRetryAttempt;


    public static AdManager init;

    private void Awake()
    {
        init = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
    }

    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {
        // Attach callbacks
        // Load the first interstitial
        LoadInterstitial();
    }

    void LoadInterstitial()
    {
    }

    public void ShowInterstitial()
    {
    }

    #endregion
}