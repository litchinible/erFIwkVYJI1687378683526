using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using Assets.Plugins.SmartLevelsMap.Scripts;
using UnityEngine.EventSystems;
public enum BoostType
{
    FiveBallsBoost = 0,
    ColorBallBoost,
    FireBallBoost
}
public enum Target
{
    Top = 0,
    Cherry
}

namespace InitScriptName
{


    public class InitScript : MonoBehaviour
    {
        public static InitScript Instance;
        private int _levelNumber = 1;
        private int _starsCount = 1;
        private bool _isShow;
        public static int openLevel;

        public static bool boostJelly;
        public static bool boostMix;
        public static bool boostChoco;

        public static bool sound = false;
        public static bool music = false;

        public static int waitedPurchaseGems;

        public static List<string> selectedFriends;
        public static bool Lauched;
        public static int scoresForLeadboardSharing;
        public static int lastPlace;
        public static int savelastPlace;
        public static bool beaten;
        public static List<string> Beatedfriends;
        int messCount;
        public static bool loggedIn;
        //	public GameObject LoginButton;
        //	public GameObject InviteButton;
        public GameObject EMAIL;
        public GameObject MessagesBox;


        public static bool FirstTime;
        public static int Lifes;
        public static int CapOfLife = 5;
        public static int Gems;

        public static float RestLifeTimer;
        public static string DateOfExit;
        public static DateTime today;
        public static DateTime DateOfRestLife;
        public static string timeForReps;

        public static bool openNext;
        public static bool openAgain;

        public int FiveBallsBoost;
        public int ColorBallBoost;
        public int FireBallBoost;
        public bool BoostActivated;

        Hashtable mapFriends = new Hashtable();

        public Target[] targets;
        public Target currentTarget;

        public void Awake()
        {
            Instance = this;
            if( Application.loadedLevelName == "map" )
            {
                if( GameObject.Find( "Canvas" ).transform.Find( "MenuPlay" ).gameObject.activeSelf ) GameObject.Find( "Canvas" ).transform.Find( "MenuPlay" ).gameObject.SetActive( false );

            }
            RestLifeTimer = PlayerPrefs.GetFloat( "RestLifeTimer" );

//			if(InitScript.DateOfExit == "")
//			print(InitScript.DateOfExit );
				DateOfExit = PlayerPrefs.GetString( "DateOfExit", "");

            Gems = PlayerPrefs.GetInt( "Gems" );
            Lifes =  PlayerPrefs.GetInt( "Lifes" );

            if( PlayerPrefs.GetInt( "Lauched" ) == 0 )
            {    //First lauching
                FirstTime = true;
                Lifes = CapOfLife;
                Gems = 5;
                PlayerPrefs.SetInt( "Gems", Gems );
                PlayerPrefs.SetInt( "Lifes", Lifes );
                PlayerPrefs.SetInt( "Lauched", 1 );
                PlayerPrefs.SetInt( "Music", 1 );
                PlayerPrefs.SetInt( "Sound", 1 );
                PlayerPrefs.Save();
            }

            GameObject.Find( "Music" ).GetComponent<AudioSource>().volume = PlayerPrefs.GetInt( "Music" );
            SoundBase.Instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt( "Sound" );

            ReloadBoosts();

            boostPurchased = false;

            if (GameObject.Find("CoreEvents") == null)
            {
                GameObject core = Instantiate(Resources.Load("Soomla/CoreEvents")) as GameObject;
                core.name = "CoreEvents";
                Instantiate(Resources.Load("Soomla/StoreEvents"));
            }
            //StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
            //StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;

        }

        void onSoomlaStoreInitialized()
        {
            //soomla

        }

        void Start()
        {
            if( Application.platform == RuntimePlatform.WindowsEditor )
            {
                Application.targetFrameRate = 60;
                //	PlayerPrefs.DeleteAll();

                //			RestScores();
            }

        }


        public static bool boostPurchased;


        void Update()
        {

        }

        //soomla
        public void onMarketPurchase(string payload, Dictionary<string, string> extra) 
        {		
            PurchaseSucceded();
        }

        public void AddGems( int count )
        {
            Gems += count;
            PlayerPrefs.SetInt( "Gems", Gems );
            PlayerPrefs.Save();
        }

        public void SpendGems( int count )
        {
            SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot( SoundBase.Instance.coins );
            Gems -= count;
            PlayerPrefs.SetInt( "Gems", Gems );
            PlayerPrefs.Save();
        }

        public void RestoreLifes()
        {
            Lifes = CapOfLife;
            PlayerPrefs.SetInt( "Lifes", Lifes );
            PlayerPrefs.Save();
        }


        public void AddLife( int count )
        {
            Lifes += count;
            if( Lifes > CapOfLife ) Lifes = CapOfLife;
            PlayerPrefs.SetInt( "Lifes", Lifes );
            PlayerPrefs.Save();
        }

        public int GetLife()
        {
            if( Lifes > CapOfLife )
            {
                Lifes = CapOfLife;
                PlayerPrefs.SetInt( "Lifes", Lifes );
                PlayerPrefs.Save();
            }
            return Lifes;
        }

        public void PurchaseSucceded()
        {
            AddGems( waitedPurchaseGems );
            waitedPurchaseGems = 0;
        }
        public void SpendLife( int count )
        {
            if( Lifes > 0 )
            {
                Lifes -= count;
                PlayerPrefs.SetInt( "Lifes", Lifes );
                PlayerPrefs.Save();
            }
            else
            {

            }
        }

        public void BuyBoost( BoostType boostType, int count, int price )
        {
            SpendGems( price );
            if( boostType != BoostType.FiveBallsBoost )
            {
                PlayerPrefs.SetInt( "" + boostType, count );
                PlayerPrefs.Save();
            }
            else
            {
//                LevelData.LimitAmount += 5;
            }
            ReloadBoosts();
        }

        public void SpendBoost( BoostType boostType )
        {
            InitScript.Instance.BoostActivated = true;
            if( boostType != BoostType.FiveBallsBoost )
                mainscript.Instance.boxCatapult.GetComponent<Grid>().Busy.GetComponent<ball>().SetBoost( boostType );
            else
                LevelData.LimitAmount += 5;
            PlayerPrefs.SetInt( "" + boostType, PlayerPrefs.GetInt( "" + boostType ) - 1 );
            PlayerPrefs.Save();
            ReloadBoosts();
        }

        public void ReloadBoosts()
        {
            FiveBallsBoost = PlayerPrefs.GetInt( "" + BoostType.FiveBallsBoost );
        //    FiveBallsBoost = 0;
            ColorBallBoost = PlayerPrefs.GetInt( "" + BoostType.ColorBallBoost );
            FireBallBoost = PlayerPrefs.GetInt( "" + BoostType.FireBallBoost );

        }


        #region selectlevel
        public int LoadLevelStarsCount( int level )
        {
            return level > 10 ? 0 : ( level % 3 + 1 );
        }

        public void SaveLevelStarsCount( int level, int starsCount )
        {
            Debug.Log( string.Format( "Stars count {0} of level {1} saved.", starsCount, level ) );
        }

        public void ClearLevelProgress( int level )
        {

        }



        void OnApplicationPause( bool pauseStatus )
        {
            if( pauseStatus )
            {
                if( RestLifeTimer > 0 )
                {
                    PlayerPrefs.SetFloat( "RestLifeTimer", RestLifeTimer );
                }
                PlayerPrefs.SetInt( "Lifes", Lifes );
                PlayerPrefs.SetString( "DateOfExit", DateTime.Now.ToString() );
                PlayerPrefs.SetInt( "Gems", Gems );
                PlayerPrefs.Save();
            }
        }

        public void OnLevelClicked( object sender, LevelReachedEventArgs args )
        {
            if (EventSystem.current.IsPointerOverGameObject(-1)) return;
            if( !GameObject.Find( "Canvas" ).transform.Find( "MenuPlay" ).gameObject.activeSelf )
            {
                PlayerPrefs.SetInt( "OpenLevel", args.Number );
                PlayerPrefs.Save();
                openLevel = args.Number;
                currentTarget = targets[args.Number];
                GameObject.Find( "Canvas" ).transform.Find( "MenuPlay" ).gameObject.SetActive( true );
            }
        }

        void OnEnable()
        {
            LevelsMap.LevelSelected += OnLevelClicked;
        }

        void OnDisable()
        {
            LevelsMap.LevelSelected -= OnLevelClicked;

            //		if(RestLifeTimer>0){
            PlayerPrefs.SetFloat( "RestLifeTimer", RestLifeTimer );
            //		}
            PlayerPrefs.SetInt( "Lifes", Lifes );
			if(Application.loadedLevel != 2)
            	PlayerPrefs.SetString( "DateOfExit", DateTime.Now.ToString() );
            PlayerPrefs.SetInt( "Gems", Gems );
            PlayerPrefs.Save();

            //		FacebookSNSAgent.OnUserInfoArrived -= OnUserInfoArrived;
            //		FacebookSNSAgent.OnUserFriendsArrived -= OnUserFriendsArrived;
        }


        #endregion


    }

}