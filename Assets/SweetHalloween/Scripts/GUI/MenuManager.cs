using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image bgMenu;
    void Start()
    {
        if(AdsManagerWrapper.INSTANCE) {
            AdsManagerWrapper.INSTANCE.ShowBanner((onAdLoded) => {}, onAdFailedToLoad => {}, AdsBannerPosition.BOTTOM_CENTER);
        }
        if(EASettingManager.instance && EASettingManager.instance.spriteMenu != null && bgMenu != null) {
            bgMenu.sprite = EASettingManager.instance.spriteMenu;
        }
    }

    public void HideBanner() {
        if(AdsManagerWrapper.INSTANCE) {
            AdsManagerWrapper.INSTANCE.HideBanner();
        }
    }
}
