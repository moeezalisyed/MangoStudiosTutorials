
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public TutorialplayerModel model;		// The model object.
	public int playerType;
	//private int initHealth;
	public TutorialGameManager m;
	public int direction = 0;
	public BoxCollider2D playerbody;
	public bool usingability = false;
	public bool usingcircpowerup = false;
	public	float cdbufA= -0.5f;		//time until ability cooldown is over
	public float cdA= 0f;	//cooldown length for ability
	public float clock;	// to keep track of the time(not used for now)
	private float damageclock = .7f;
	public bool isTutorial;

	public void init(int playerType, TutorialGameManager m) {

		this.playerType = playerType;
		//this.initHealth = initHealth;
		this.m = m;

		if (this.playerType == 0){
			//triangle

			this.cdA = 1.5f;


		} else if (this.playerType == 1){
			//circle

			this.cdA = 1.5f;

		} else if (this.playerType == 2){
			//square

			this.cdA = 1.5f;

		}

//		camera = GetComponent<Camera>();
//		camera.clearFlags = CameraClearFlags.SolidColor;
//

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		playerbody = gameObject.AddComponent<BoxCollider2D> ();

		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<TutorialplayerModel>();						// Add an playerModel script to control visuals of the gem.
		model.init(playerType, this);		
		this.tag = "Player";
		transform.localScale = new Vector3 (0.75f, 0.75f, 1);
		if (this.playerType == 1) {
			transform.localScale = new Vector3 (1.4f, 1f, 1);

		}
	}
	void Start(){
		clock = 0f;
	
	}

	public void move(int x, int y){
		this.model.transform.eulerAngles = new Vector3 (0, 0, 90 * this.direction);
		model.move (x, y);
	}

	public int getHealth(){
		return model.getHealth();
	}

	public void useAbility(){
		if (clock - cdbufA > cdA) {
			if (this.model.firstRun) {
				m.PlayEffect (this.m.abilityon);
			}
			StartCoroutine (usingabil ());
			cdbufA = clock;
		}

	}


	IEnumerator startPowerUp (){
		this.usingcircpowerup = true;
		yield return new WaitForSeconds (5);
		this.usingcircpowerup = false;
	}

	//Making the player temporary flash when it got hit
	IEnumerator whenGotHit (){
		this.model.mat.color = Color.red;
//		camera.backgroundColor = Color.red;
		yield return new WaitForSeconds (0.03f);
		this.model.mat.color = new Color(1,1,1,1);
//		camera.backgroundColor = Color.black;
	}

	IEnumerator usingabil (){
		this.usingability = true;
		if (this.playerType == 2) {
			this.setCD (this.model.cd/1.7f);
		}

		if (this.playerType == 1) {
			this.tag = "inviscircle";
			//print ("changed tag to " + this.tag);
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/inviscircle");
			transform.localScale = new Vector3 (3f, 3f, 0);
		}
		yield return new WaitForSeconds (5);
		this.usingability = false;
		if (this.playerType == 2) {
			this.setCD (this.model.cd * 1.7f);
		}
		if (this.playerType == 1) {
			this.tag = "Player";
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			transform.localScale = new Vector3 (0.75f, 0.75f, 1);
			transform.localScale = new Vector3 (1.4f, 1f, 1);
		}
	}


	public void shoot(){
		model.shoot ();
	}

	public void setCD(float a){
		model.setCD (a);
	}

	public int getType(){
		return model.getType();
	}

	public float getX(){
		return transform.position.x;
	}

	public float getY(){
		return transform.position.y;
	}

	public void makeTutorial(){
		isTutorial = true;
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "inviscircle") {
			//Here it will check if it collided with a circle
			/*if (other.GetComponentInParent (Texture2D) == "inviscircle") {
				//increase the damage stuff
				print("got to the invis circle");
			}*/

			print ("Got a powerup!");
			StartCoroutine (startPowerUp ());

		}


	}




}

