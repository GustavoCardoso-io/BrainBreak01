using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System.Collections.Generic;

public class ADSManager : MonoBehaviour, IUnityAdsListener
{
    string placementRewarded = "rewardedVideo";

    private void Awake()
    {

    }
    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3455743", false);
    }
    public void ShowAdReward(string p)
    {
        if (PlayerPrefs.GetInt("TimeToPlayerDie") >= 3)
        {
            Advertisement.Show(p);

        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("TimeToPlayerDie", 0);
        }
        else if (showResult == ShowResult.Failed)
        {
            PlayerPrefs.SetInt("TimeToPlayerDie", 3);
        }
        else if (showResult == ShowResult.Skipped)
        {
            PlayerPrefs.SetInt("TimeToPlayerDie", 2);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {

    }
    public void OnUnityAdsDidError(string message)
    {

    }
}
