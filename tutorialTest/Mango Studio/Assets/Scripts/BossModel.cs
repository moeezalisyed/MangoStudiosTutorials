﻿using UnityEngine;
using System.Collections;

public class BossModel : MonoBehaviour {

	private tBoss owner;			// Pointer to the parent object.
	public Material mat;
	private string bossTex;

	public void init(tBoss owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Boss Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.


		if (owner.m.bossCurrentLife == 1) {
			this.bossTex = "Textures/boss1d";
			mat.mainTexture = Resources.Load<Texture2D>("Textures/boss1d0");	// Set the texture.  Must be in Resources folder.
		} else if (owner.m.bossCurrentLife == 2){
			this.bossTex = "Textures/boss2d";
			mat.mainTexture = Resources.Load<Texture2D>("Textures/boss2d0");	// Set the texture.  Must be in Resources folder.
		} else if(owner.m.bossCurrentLife == 3){
			mat.color = new Color(0,0,1);
		}



	}

	public void changeTexture(int texType){
		mat.mainTexture = Resources.Load<Texture2D>(this.bossTex+texType);	// Set the texture.  Must be in Resources folder.
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
