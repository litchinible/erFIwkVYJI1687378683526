using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

namespace EaAds
{
    public class InitDataNetworkAds : MonoBehaviour
    {
        public Slider sliderLoading;
        public Text textLoading;
        public string nextScene;
        public string baseUrl;

        // Use this for initialization
        void Start()
        {
            // StartCoroutine(GetDataFromServer((isComplet) => {
            //     SceneManager.LoadScene(nextScene);
            // }));
            string url = "https://app.masyadi.com/configApp/apps?packageName=" + Application.identifier;
            StartCoroutine(
                EaAds.AdsUtils.GetAdConfigFromServer(
                    url,
                    (progress) =>
                    {
                        textLoading.text = progress.progressPersen;
                        sliderLoading.value = progress.progressFloat;
                    },
                    (adConfig, isSuccess) =>
                    {
                        EaAds.AdsUtils.MapConfigAd(adConfig);
                        AdsManagerWrapper.INSTANCE.Initialize((init) => {});
                        AdsManagerWrapper.INSTANCE.LoadInterstitial();
                        AdsManagerWrapper.INSTANCE.LoadRewards();
                        Debug.Log("AdConfig: "+adConfig+", isSuccces: " + isSuccess);
                        SceneManager.LoadScene(nextScene);
                    }
                )
            );
        }

    }
}
