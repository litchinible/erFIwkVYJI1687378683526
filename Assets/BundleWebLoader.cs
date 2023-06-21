	using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BundleWebLoader : MonoBehaviour
{
    public string bundleUrl = "http://localhost/assetbundles/testbundle";
    public string assetName = "BundledObject";
    public string nextScene = "";
    public Text textLoading;
    public Slider loadingSlider;

    // Start is called before the first frame update
    IEnumerator getBundle()
    {
        //using (WWW web = new WWW(bundleUrl))
        //{
        //    yield return web;
        //    AssetBundle remoteAssetBundle = web.assetBundle;
        //    if (remoteAssetBundle == null) {
        //        Debug.LogError("Failed to download AssetBundle!");
        //        yield break;
        //    }

        //    //var parent = GameObject.FindGameObjectWithTag("Player");
        //    var player = remoteAssetBundle.LoadAsset<GameObject>(assetName);
        //    Debug.Log("player", player);
        //    PlayerManager.instance.SetPlayer(player);
        //    //Instantiate(remoteAssetBundle.LoadAsset(assetName));
        //    remoteAssetBundle.Unload(false);
        //    SceneManager.LoadScene(nextScene);
        //}

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        www.SendWebRequest();

        while (!www.isDone)
        {
            Debug.Log("progress" + www.downloadProgress);
            if(loadingSlider != null)
            {
                loadingSlider.value = www.downloadProgress;
            }

            if(textLoading != null)
            {
                textLoading.text = string.Format("{0:0}%", www.downloadProgress * 100);
            }
            

            yield return null;
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //loadingSlider.gameObject.SetActive(false); // Hide the slider

            //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            //GameObject obj = bundle.LoadAsset<GameObject>("YourAssetName");
            //Instantiate(obj);
            //var parent = GameObject.FindGameObjectWithTag("Player");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            GameObject player = bundle.LoadAsset<GameObject>(assetName);
            Debug.Log("player", player);
            PlayerManager.instance.SetPlayer(player);
            //Instantiate(remoteAssetBundle.LoadAsset(assetName));
            //remoteAssetBundle.Unload(false);
            SceneManager.LoadScene(nextScene);
        }
    }

    public void LoadBundle()
    {
        StartCoroutine(getBundle());
    }


}