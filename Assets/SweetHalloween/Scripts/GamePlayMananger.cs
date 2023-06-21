using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayMananger : MonoBehaviour
{
    public Image bgGamePlay;
    public CharAnim charAnim;
    // Start is called before the first frame update

    private void Awake() {
        if(EASettingManager.instance && EASettingManager.instance.spriteGamePlay != null && bgGamePlay != null) {
            bgGamePlay.sprite = EASettingManager.instance.spriteGamePlay;
        }
    }

    void Start()
    {
        if(PlayerManager.instance) {
            var oldPlayer = GameObject.FindGameObjectWithTag("Player");
            var newObj = PlayerManager.instance.InstantiatePlayer(oldPlayer.transform);
            Debug.Log("newObj: "+newObj);
            if(!charAnim) {
                charAnim = FindObjectOfType<CharAnim>();
            }
            // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().anim = newPlayer.GetComponentInChildren<Animator>();
            charAnim.delayShootSecond = newObj.GetComponent<CharVariable>().delayShootSecond;
            charAnim.anim = newObj.GetComponent<Animator>();
            // charAnim.anim.avatar = newObj.GetComponent<Animator>().avatar;
            Debug.Log("charAnim: "+charAnim.anim);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
