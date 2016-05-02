using UnityEngine;
using System.Collections;

public class Level1B : MonoBehaviour {

	private Level1BModel model;
	private int health;
	private TutorialGameManager m;

	// Use this for initialization
	public void init (TutorialGameManager m, int h) {
		this.m = m;		
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<Level1BModel>();					// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		playerbody.isTrigger = true;

		health = h;
		this.name = "Boss";
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			m.ChangeLevel (1);
			Destroy (this.gameObject);
		}
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Bullet" || other.name == "SpecialBullet") {
			health = health - 2;
		}
	}
}
