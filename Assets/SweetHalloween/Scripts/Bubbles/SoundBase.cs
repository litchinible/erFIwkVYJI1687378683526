using UnityEngine;
using System.Collections;
[RequireComponent( typeof( AudioSource ) )]
public class SoundBase : MonoBehaviour {
	public static SoundBase Instance;
	public AudioClip click;
	public AudioClip[] combo;
	public AudioClip[] swish;
	public AudioClip bug;
	public AudioClip bugDissapier;
	public AudioClip pops;
 	public AudioClip boiling;
 	public AudioClip hit;
 	public AudioClip kreakWheel;
 	public AudioClip spark;
 	public AudioClip winSound;
 	public AudioClip gameOver;
 	public AudioClip scoringStar;
 	public AudioClip scoring;
 	public AudioClip alert;
 	public AudioClip aplauds;
 	public AudioClip OutOfMoves;
 	public AudioClip Boom;
 	public AudioClip black_hole;
 	public AudioClip coins;

    ///SoundBase.Instance.audio.PlayOneShot( SoundBase.Instance.kreakWheel );

   // Use this for initialization
	void Awake () {
		if(Instance!=null) {
			Destroy(this);
		}
		
		
	}

	private void Start() {
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
