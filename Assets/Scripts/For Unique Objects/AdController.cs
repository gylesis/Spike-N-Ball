using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Networking;

namespace For_Unique_Objects
{
    public class AdController : MonoBehaviour
    {
        public static AdController Instance;

        public event Action OnAdClose;

        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;
        
        private void Awake() =>
            Instance = this;

        private void Start()
        {
            var requestConfiguration = new RequestConfiguration();
            requestConfiguration.TestDeviceIds.Add("A16833AA06699A2A7C45D03148503C19");
            MobileAds.SetRequestConfiguration(requestConfiguration);
            
            MobileAds.Initialize(OnAdsInit);
        }

        private void OnAdsInit(InitializationStatus initializationStatus)
        {
            Debug.Log(initializationStatus.getAdapterStatusForClassName(""));
        }

        public void TryPlayAdByDeatCount(int deathCount)
        {
            if (deathCount % 4 != 0) return;

            TryShowInterInterstitialAd();
        }

        private void TryShowInterInterstitialAd()
        {
            if (_interstitialAd != null)    
            {
                _interstitialAd.Show();
            }
            else
            {
                LoadInterstitialAd();
            }
        }
        
        public void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.OnAdFullScreenContentClosed -= OnAdClosed;
                _interstitialAd.OnAdFullScreenContentFailed -= OnAdClosed;
                _interstitialAd.OnAdFullScreenContentOpened -= OnAdClosed;
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            string adId;

            var isDebugBuild = Debug.isDebugBuild;
            
            if (isDebugBuild)
            {
                adId = Constants.AdMob.TestInterstetialAd;
            }
            else
            {
                adId = Constants.AdMob.Interstetial;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            InterstitialAd.Load(adId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                    _interstitialAd.OnAdFullScreenContentClosed += OnAdClosed;
                    _interstitialAd.OnAdFullScreenContentFailed += OnAdClosed;
                    _interstitialAd.OnAdFullScreenContentOpened += OnAdClosed;
                    
                });
        }
        
        public void LoadRewardedAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.OnAdFullScreenContentClosed -= OnAdClosed;
                _rewardedAd.OnAdFullScreenContentFailed -= OnAdClosed;
                _rewardedAd.OnAdFullScreenContentOpened -= OnAdClosed;
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");
            
            string adId;

            var isDebugBuild = Debug.isDebugBuild;
            
            if (isDebugBuild)
            {
                adId = Constants.AdMob.TestRewardedAd;
            }
            else
            {
                adId = Constants.AdMob.Reward;
            }
            
            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedAd.Load(adId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad;
                    _rewardedAd.OnAdFullScreenContentClosed += OnAdClosed;
                    _rewardedAd.OnAdFullScreenContentFailed += OnAdClosed;
                    _rewardedAd.OnAdFullScreenContentOpened += OnAdClosed;
                });
        }
        
        public void TryPlayRewardedAd()
        {
            if (_rewardedAd != null)    
            {
                _rewardedAd.Show(OnAdRewarded);
            }
            else
            {
                LoadRewardedAd();
            }
        }

        private bool CheckInternetConnection()
        {
            return Application.internetReachability == NetworkReachability.NotReachable;
            
            UnityWebRequest request = UnityWebRequest.Get("https://www.google.com/");
            request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log(request.error);
                return true;
            }

            return false;
        }

        private void OnAdClosed(AdError adError)
        {
            OnAdClosed();
        }

        private void OnAdRewarded(Reward reward)
        {
            Debug.Log($"Ad rewarded {reward.Type}, {reward.Amount}");
        }
        
        private void OnAdClosed()
        {
            _interstitialAd.OnAdFullScreenContentClosed -= OnAdClosed;
            _interstitialAd.OnAdFullScreenContentFailed -= OnAdClosed;
            _interstitialAd.OnAdFullScreenContentOpened -= OnAdClosed;
            _interstitialAd.Destroy();
            _interstitialAd = null;
            
            Time.timeScale = 5;
            OnAdClose?.Invoke();
        }
    }
}