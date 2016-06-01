using UnityEngine;
using System.Collections;

using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System;

public class AdController : MonoBehaviour,IRewardedVideoAdListener
{

    int iterator = 0;

    string appKey = "72ac4ef24f84871ef30895b31c3b5f4ea60c4079d11e9ba6";

    Library library;

	// Use this for initialization
	void Awake () {
        library = GameObject.FindObjectOfType<Library>();
	}
	
    void Start()
    {
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.setLogging(true);
        //Appodeal.setTesting(true);
    }

    public void AddIterator()
    {
        iterator++;
    }

    public bool CanShowVideoAd()
    {
        if (iterator % 4 == 2 && Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            return true; 
        else
            return false;
    }

    public bool CanShowStaticAd()
    {
        if (iterator % 4 == 3)
            return true;
        else
            return false;
    }

    public void ShowVideoAd()
    {
        Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

    public void ShowStaticAd()
    {
        Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void OnCompleteVideoAd()
    {
        library.money.AddMoney(GameplayConstants.AdMoneyReward);
        library.screenController.endScreen.GetComponent<EndScreen>().HideAdButton();
    }

    public void onRewardedVideoLoaded()
    {
        Debug.Log("onRewardedVideoLoaded");
    }

    public void onRewardedVideoFailedToLoad()
    {
        library.screenController.endScreen.GetComponent<EndScreen>().HideAdButton();
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("onRewardedVideoShown");
    }

    public void onRewardedVideoFinished(int amount, string name)
    {
        Debug.Log("onRewardedVideoFinished");

        OnCompleteVideoAd();
    }

    public void onRewardedVideoClosed()
    {
        

        Debug.Log("onRewardedVideoClosed");
    }
}
