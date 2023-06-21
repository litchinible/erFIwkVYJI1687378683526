using UnityEngine;
using System.Collections;
using InitScriptName;

public class AudioSettingsUI : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        GameObject Off = transform.GetChild(0).gameObject;
        if (name == "MusicOff")
        {
            if (PlayerPrefs.GetInt("Music") == 0)
            {
                Off.SetActive(true);
            }
            else
            {

                Off.SetActive(false);

            }
        }
        else if (name == "SoundOff")
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                Off.SetActive(true);
            }
            else
            {

                Off.SetActive(false);

            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
