using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TheyAreComing
{
    public class AdsManager : MonoBehaviour, IUnityAdsListener
    {
        private const string GameId = "4722187";
        private static Action _onRewardedAdComplete;

        private void Start()
        {
            Advertisement.Initialize(GameId);
            Advertisement.AddListener(this);
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult != ShowResult.Finished) return;
            _onRewardedAdComplete?.Invoke();
        }

        public static void PlayRewardedAd(Action onRewardedAdComplete)
        {
            _onRewardedAdComplete = onRewardedAdComplete;
            if (Advertisement.IsReady("Rewarded_Android"))
            {
                Advertisement.Show("Rewarded_Android");
            }
        }
    }
}