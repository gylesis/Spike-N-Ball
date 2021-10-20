using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Networking;

namespace For_Unique_Objects
{
    public class AdController : MonoBehaviour
    {
        public static AdController Instance;

        public event Action OnAdClose;

        private void Awake() =>
            Instance = this;

        private void Start()
        {
            MobileAds.Initialize(status => { });
        }

        public void TryPlayAdByDeatCount(int deathCount)
        {
            if (CheckInternetConnection()) return;

            if (deathCount % 4 != 0) return;

            string adId;

            var isDebugBuild = Debug.isDebugBuild;
            
            if (isDebugBuild)
            {
                adId = Constants.AdMob.Interstetial;
            }
            else
            {
                adId = Constants.AdMob.TestAd;
            }

            InterstitialAd interstitialAd = new InterstitialAd(adId);
            
            AdRequest adRequest = new AdRequest.Builder().Build();
            
            interstitialAd.LoadAd(adRequest);

            interstitialAd.Show();
            interstitialAd.OnAdClosed += OnAdClosed;
        }

        public void TryPlayRewardedAd()
        {
            if (CheckInternetConnection()) return;
            
            string adId;

            var isDebugBuild = Debug.isDebugBuild;
            
            if (isDebugBuild)
            {
                adId = Constants.AdMob.Reward;
            }
            else
            {
                adId = Constants.AdMob.TestAd;
            }
            
            RewardedAd rewardedAd = new RewardedAd(adId);

            var adRequest = new AdRequest.Builder().Build();
            
            rewardedAd.LoadAd(adRequest);

            rewardedAd.Show();
            rewardedAd.OnAdClosed += OnAdClosed;
        }

        private bool CheckInternetConnection()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://www.google.com/");
            request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log(request.error);
                return true;
            }

            return false;
        }

        private void OnAdClosed(object sender, EventArgs e)
        {
            Time.timeScale = 5;
            OnAdClose?.Invoke();
        }
    }
}