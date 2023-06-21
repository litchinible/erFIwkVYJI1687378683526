using UnityEngine;
using System.Collections;
using System;

public class CharAnim : MonoBehaviour {
    public Animator anim;
	public creatorBall _creatorBall;
	public float delayShootSecond = 15;
	// Use this for initialization
	void Awake () {
        // anim = GetComponent<Animator>();
		Debug.Log("start Char Anim");
		_creatorBall =  FindObjectOfType<creatorBall>();
	}

    void Update()
    {
       
    }

	public void ShootBubble() {
		Debug.Log("ShootBubble, anim: "+anim);
		if(anim) {
			anim.SetTrigger("shoot");
		}
	}

	public void ShootBall() {
		Debug.Log("tembak sini");
		_creatorBall.ball_hd.GetComponent<ball>().ShootBall();
	}

	public void LevelComplete() {
		if(anim) {
			anim.SetTrigger("levelComplete");
		}
	}
	
	// Update is called once per frame
	public void Idle () {
        
	}

	public IEnumerator WaitForAnimation(Action callback)
    {
        yield return new WaitForSeconds(delayShootSecond);
        callback?.Invoke();
    }
}
