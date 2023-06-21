using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InitScriptName;

public class TargetIcon : MonoBehaviour {
    public Sprite[] targets;
	// Use this for initialization
	void OnEnable () {
	    
        GetTarget();
	}

    void GetTarget()
    {
        if( Application.loadedLevelName == "map" )
        {
            if( InitScript.Instance.currentTarget == Target.Top ) SetIcon( 0 );
            else if( InitScript.Instance.currentTarget == Target.Cherry ) SetIcon( 1 );

        }
        else
        {
            if( LevelData.mode == ModeGame.Vertical ) SetIcon( 0 );
            else if( LevelData.mode == ModeGame.Rounded ) SetIcon( 1 );

        }
    }

    void SetIcon( int num )
    {
        GetComponent<Image>().sprite = targets[num];
        GetComponent<Image>().SetNativeSize();
    }
}
