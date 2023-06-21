using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreTutorial : MonoBehaviour {
    public Sprite[] pictures;
    Image image;
	// Use this for initialization
	void OnEnable () {
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = pictures[(int)LevelData.mode];
        image.SetNativeSize();
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot( SoundBase.Instance.swish[0] );

	}
	
	// Update is called once per frame
	public void  Stop() {
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot( SoundBase.Instance.swish[1] );

        GamePlay.Instance.GameStatus = GameState.Playing;
        gameObject.SetActive( false );
	}
}
