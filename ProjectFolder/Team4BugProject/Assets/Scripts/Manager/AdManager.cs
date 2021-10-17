using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        //string adUnitId = 	"ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-6023793752348178/7663744399";
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void ShowAd()
    {
        this.interstitial.Show();
    }

    void Start()
    {
        RequestInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
