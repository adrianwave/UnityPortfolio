using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

	public int playerMaxHealth;
	public int playerCurrentHealth;

	private bool flashActive;
	public float flashLength;
	private float flashCounter;

	private SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {

		playerSprite = GetComponent<SpriteRenderer> ();

		playerCurrentHealth = playerMaxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (playerCurrentHealth <= 0) {
		
			gameObject.SetActive (false);
		}

		if (flashActive == true) {
			if (flashCounter > flashLength * .66f) {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			} else if (flashCounter > flashLength * .33f) {
			
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);

			} else if (flashCounter > 0f) {
			
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			} else {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				flashActive = false;
			}

			flashCounter -= Time.deltaTime;
		}
	}

	public void hurtPlayer(int damageToGive) {

		playerCurrentHealth -= damageToGive;

		flashActive = true;
		flashCounter = flashLength;

	}

	public void setMaxHealth(){
	
		playerCurrentHealth = playerMaxHealth;

	}
}
