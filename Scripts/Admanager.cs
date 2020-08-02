
using UnityEngine;
using UnityEngine.Advertisements;


public class Admanager : MonoBehaviour
{
    // Start is called before the first frame update
    private string playStoreID = "3731737";
    private string appStoreID = "3731736";
    private string interstialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";
    public bool isTargetPlayStore;
    public bool isTestAd;
    static int loadCount = 3;
    private void Start()
    {
        //if (loadCount%3==0)  // only show ad every third time
        //{
            InitializeAdvertisement();
        //}
        //loadCount++;
    }
    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore)
        {Advertisement.Initialize(playStoreID,isTestAd); return;}
        Advertisement.Initialize(appStoreID, isTestAd);
    }
    public void PlayInterstitialAd()
    {
        if(!Advertisement.IsReady(interstialAd))
        { return; }
        if (loadCount==0)
    
            {
                Advertisement.Show(interstialAd);
                loadCount = 3;
            }
        loadCount--;



    }
    public void PlayRewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd)) { return; }
        Advertisement.Show(rewardedVideoAd);
    }

}
