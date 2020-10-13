using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour, IUnityAdsListener
{
    static string gameId = "3545633";
    static string myPlacementId = "rewardedVideo";
    static bool isHaveOnline = false;

    public static Ads ads;
    [SerializeField]
    GameObject T_Help;
    public static void PlayAds()
    {
        //Debug.Log(Advertisement.IsReady());
        if (Advertisement.IsReady())
        {
            Advertisement.Show(myPlacementId);
            ads.T_Help.SetActive(false);
        }
        else
        {
            ads.T_Help.SetActive(true);
            isHaveOnline = true;
        }
    }
    void Start()
    {
        ads = this;
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Advertisement.AddListener(ads);
            Advertisement.Initialize(gameId, false);
            isHaveOnline = false;
            //Debug.Log("Initialized");
        }
    }
    private void Update()
    {
        if(isHaveOnline)
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                Advertisement.AddListener(ads);
                Advertisement.Initialize(gameId, false);
                //Debug.Log("Initialized");
            }
    }
    public void OnUnityAdsReady(string placementId) {
        //if (placementId == myPlacementId)
            
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            EnergyBar.Energies = 5;
            EnergyBar.referenceEnergyBar.ChangeSprite();
            SaveLoad.SaveEnergies();
            //Debug.Log("Finish Saw Ad");
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if(showResult == ShowResult.Failed)
        {
            //Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("Start play - Ad.");
    }
}
