using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetOnlineManager : MonoBehaviour
{
    public static AssetOnlineManager Instance;
    public Sprite spriteMenu;
    public Sprite spriteGamePlay;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

}
