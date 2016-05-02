using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TutorialGameManager : MonoBehaviour
{

    public int boardWidth, boardHeight; // board size init is in Unity editor
    private GameObject playerFolder;// folders for object organization
	public List<GameObject> bulletsFolder = new List<GameObject>();


    private List<Player> players; // list of all placed players
//	******** FELIPE LOOK HERE FOR current player ********
	public Player currentplayer;

//	******** FELIPE LOOK HERE FOR Shadow CHARACTER List ********
	public List<Player> shadowPlayers = new List<Player>();
    // Beat tracking
    private float clock;
    private float startTime;
    private float BEAT = .5f;
    private int numBeats = 0;
    int playerbeaten = 0;
    int playernum = 0;
	private int playertype = 0;
	public Text HealthText;
	private List<Vector3> shadow;
	private int shadowiterator;
	private Boolean startitr;
	public Boolean gameover = false;
	public Boolean gamewon = false;

	private int maxEnv = 4;


	//******Handling player lives, as well as the order and iteration of player********
	private int playerLives = 9;
	private int[] playerOrder;// = new int[playerLives];
	private int playerOrderIndex = 0;

	//******Boss Lives********
	private int bossTotalLives = 3;
	public int bossCurrentLife = 1;

	//Iteration Transition Variables
	private bool inTransition = false;
	private bool guiTransition = false;


	//Textures for GUI
	public Texture forSq;
	public Texture forC;
	public Texture forT;
	public Texture bossText;
	public Texture bossHpText;
	public Texture lastLifeText;
	public Texture nextBossText;
	public Texture nextUpText;
	public Texture scoreText;
	public Texture cooldownText;
	public Texture specialText;
	public Texture abilityOnText;
	public Texture gameoverText;
	public Texture gamewonText;
	public Texture startText;
	public Texture quitText;
	public Texture nextbossText;
	public Texture titleText;

	// These are the readonly CD Functions
	public readonly float coolDownCircle = 0.4f;
	public readonly float coolDownTriangle = 0.6f;
	public readonly float coolDownSquare = 1.3f;

	//define character speed for every iteration blowup and slowdown
	public float charSpeed;
	public float bossSpeed;
	public bool inSlowDown = false;

    // Level number

    private int level = 0;


    //button locations
    float trayx = 0;
    float traywidth = 0;
    float trayspace = 0;

    // Sound stuff
    public AudioSource music;
    public AudioSource sfx;

    // Music clips
	public AudioClip bgm;
    private AudioClip menumusic;

    // Sound effect clips
	public AudioClip bossDead;
	public AudioClip playerHit;
	public AudioClip playerHitX;
	public AudioClip bossHit;
	public AudioClip bossHitX;
    public AudioClip abilityon;
    public AudioClip abilityoff;
	public AudioClip shootClip;

    // Use this for initialization
    void Start(){
		
		this.charSpeed = 2.9f;
		clock = 30;
		level = 1;
		GameObject playerObject = new GameObject();		
		currentplayer = playerObject.AddComponent<Player>();
		currentplayer.transform.position = new Vector3(5,0,0);
		currentplayer.init (0,this);

		GameObject bossObject = new GameObject();		
		Level1B boss = bossObject.AddComponent<Level1B>();
		boss.init (this, 20);
		boss.transform.position = new Vector3(-5,0,0);

		music.GetComponent<AudioSource> ().clip = bgm;
		music.Play ();

        SoundSetUp();

	}

    void Update(){
		clock = clock - Time.deltaTime;
		Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(currentplayer.transform.position);
		float speed = this.charSpeed;

		if (Input.GetKey (KeyCode.RightArrow)  && playerPosScreen.x < Screen.width -22) {
			if (currentplayer.playerType != 2 || !currentplayer.usingability) {
				currentplayer.direction = 3;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, 3 * 90);
				if (Input.GetKey (KeyCode.UpArrow) && playerPosScreen.y < Screen.height -22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 3 * 90 + 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 7;
				}
				if (Input.GetKey (KeyCode.DownArrow) && playerPosScreen.y > 22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 3 * 90 - 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 6;
				}
				currentplayer.transform.Translate (Vector3.up * this.charSpeed * Time.deltaTime);
			} 
		} 
		if (Input.GetKey (KeyCode.UpArrow) && playerPosScreen.y < Screen.height -22 ) {


			//above
			if (currentplayer.playerType != 2 || !currentplayer.usingability) {
				currentplayer.direction = 0;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, 0 * 90);
				if (Input.GetKey (KeyCode.RightArrow) && playerPosScreen.x < Screen.width -22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 0 * 90 - 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 7;
				}
				if (Input.GetKey (KeyCode.LeftArrow) && playerPosScreen.x > 22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 0 * 90 + 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 4;
				}
				currentplayer.transform.Translate (Vector3.up * speed * Time.deltaTime);

			}

		}
		if (Input.GetKey (KeyCode.LeftArrow) && playerPosScreen.x > 22 ){

			//above
			if (currentplayer.playerType != 2 || !currentplayer.usingability) {
				currentplayer.direction = 1;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, 1 * 90);
				if (Input.GetKey (KeyCode.UpArrow) && playerPosScreen.y < Screen.height -22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 1 * 90 - 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 4;
				}
				if (Input.GetKey (KeyCode.DownArrow) && playerPosScreen.y > 22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 1 * 90 + 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 5;
				}
				currentplayer.transform.Translate (Vector3.up * this.charSpeed * Time.deltaTime);

			} 
		}
		if (Input.GetKey (KeyCode.DownArrow) && playerPosScreen.y > 22 ) {


			//bove
			if (currentplayer.playerType != 2 || !currentplayer.usingability) {
				currentplayer.direction = 2;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, 2 * 90);
				if (Input.GetKey (KeyCode.LeftArrow) && playerPosScreen.x > 22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 2 * 90 - 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 5;

				}
				if (Input.GetKey (KeyCode.RightArrow) && playerPosScreen.x < Screen.width -22) {
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, 2 * 90 + 45);
					speed = this.charSpeed * (1/2);
					currentplayer.direction = 6;
				}
				currentplayer.transform.Translate (Vector3.up * speed * Time.deltaTime);

			}
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			currentplayer.shoot();
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			currentplayer.useAbility ();

		}

		if (clock <= 0) {
			ChangeLevel (0);
		}



    }

	public void ChangeLevel(int win){
		if (win == 1) {
			level++;
			if (level == 1) {
				clock = 30;
				GameObject playerObject = new GameObject ();		
				currentplayer = playerObject.AddComponent<Player> ();
				currentplayer.transform.position = new Vector3 (5, 0, 0);
				currentplayer.init (0, this);

				GameObject bossObject = new GameObject ();		
				Level1B boss = bossObject.AddComponent<Level1B> ();
				boss.init (this, 20);
				boss.transform.position = new Vector3 (-5, 0, 0);
			} else if (level == 2) {
				clock = 30;
				GameObject playerObject = new GameObject ();		
				currentplayer = playerObject.AddComponent<Player> ();
				currentplayer.transform.position = new Vector3 (5, -4, 0);
				currentplayer.init (2, this);

				GameObject playerObject2 = new GameObject ();		
				Player player = playerObject.AddComponent<Player> ();
				player.transform.position = new Vector3 (5, 0, 0);
				player.init (0, this);
				player.isTutorial = true;

				GameObject bossObject = new GameObject ();		
				Level1B boss = bossObject.AddComponent<Level1B> ();
				boss.init (this, 20);
				boss.transform.position = new Vector3 (-5, 0, 0);
			} else if (level == 3) {
				clock = 30;
				GameObject playerObject = new GameObject ();		
				currentplayer = playerObject.AddComponent<Player> ();
				currentplayer.transform.position = new Vector3 (6, -5, 0);
				currentplayer.init (1, this);

				GameObject playerObject2 = new GameObject ();		
				Player player = playerObject.AddComponent<Player> ();
				player.transform.position = new Vector3 (5, 0, 0);
				player.init (0, this);
				player.isTutorial = true;

				GameObject playerObject3 = new GameObject ();		
				Player player3 = playerObject.AddComponent<Player> ();
				player3.transform.position = new Vector3 (4, 0, 0);
				player3.init (0, this);
				player3.isTutorial = true;

				GameObject bossObject = new GameObject ();		
				Level1B boss = bossObject.AddComponent<Level1B> ();
				boss.init (this, 20);
				boss.transform.position = new Vector3 (-5, 0, 0);
			}
		}
	}

    private void SoundSetUp()
    {
        // music
/*        idle = Resources.Load<AudioClip>("Music/title song");
        gametrack = Resources.Load<AudioClip>("Music/Main song loop");
        winmusic = Resources.Load<AudioClip>("Music/You Win Song");*/

        // sfx
        bossDead = Resources.Load<AudioClip>("Music/explosion");
        playerHit = Resources.Load<AudioClip>("Music/shoot");
        playerHitX = Resources.Load<AudioClip>("Music/shootX");  //Special bullet when ability is on
  		bossHit = Resources.Load<AudioClip>("Music/bosshit");
        bossHitX = Resources.Load<AudioClip>("Music/bosshitX");
        abilityon = Resources.Load<AudioClip>("Music/abilityon");
        abilityoff = Resources.Load<AudioClip>("Music/abilityoff");    }
        // Music section

    public void PlayEffect(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        this.music.loop = true;
        this.music.clip = clip;
        this.music.Play();
    }



}