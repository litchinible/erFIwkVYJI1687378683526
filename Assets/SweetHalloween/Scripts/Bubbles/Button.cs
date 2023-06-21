using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        if (name == "Change" && GamePlay.Instance.GameStatus == GameState.Playing)
        {
            mainscript.Instance.ChangeBoost();
        }

    }
	
	// Update is called once per frame
	void OnPress (bool press) {
        if (press) return;
 	}
}
