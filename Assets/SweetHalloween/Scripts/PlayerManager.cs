using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public GameObject player;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public GameObject InstantiatePlayer(Transform parent)
    {
        
         var oldPlayer = GameObject.FindGameObjectWithTag("Player");
         var result = oldPlayer;
         if(parent) {
            RemoveAllChildrenFromParent(parent);
            result = Instantiate(player, parent);
         }
        else if(player!=null)
        {
            result = Instantiate(player, oldPlayer.transform.position, Quaternion.identity);
            Destroy(oldPlayer);
        }
        return result;
    }

    private void RemoveAllChildrenFromParent(Transform parent)
    {
        if(parent.transform.childCount > 0) {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
