using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

	public float speed = 10;

	public Rigidbody2D rigidBody;

	// Starting sprite
	public Sprite startingImage;

	// Alternative image used for the Alien
	public Sprite altImage;

	// Used to change the Alien image
	private SpriteRenderer spriteRenderer;

	// Wait time before switching sprites
	public float secBeforeSpriteChange = 0.5f;

	// Reference to bullet GameObject
	public GameObject alienBullet;

	// Minimum time to wait before firing
	public float minFireRateTime = 1.0f;

	// Maximum time to wait before firing
	public float maxFireRateTime = 3.0f;

	// Base firing wait time
	public float baseFireWaitTime = 3.0f;

	// Exploded Ship Image
	public Sprite explodedShipImage;

	void Start(){

		rigidBody = GetComponent<Rigidbody2D>();

		// Set the starting direction and speed
		rigidBody.velocity = new Vector2 (1, 0) * speed;

		// Access the SpriteRenderer component 
		spriteRenderer = GetComponent<SpriteRenderer>();

		// Call changeAlienSprite () to cycle the Alien sprites
		StartCoroutine (changeAlienSprite ());

		// Defines a random fire wait time for each Alien
		baseFireWaitTime = baseFireWaitTime + 
			Random.Range (minFireRateTime, maxFireRateTime);

	}

	// Changes the direction for the Alien object
	void Turn(int direction) {
		Vector2 newVelocity = rigidBody.velocity;
		newVelocity.x = speed * direction;
		rigidBody.velocity = newVelocity;
	}

	// Moves the Alien vertically down
	void MoveDown() {
		Vector2 position = transform.position;
		position.y -= 1;
		transform.position = position;
	}


	// Switch direction on collision
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "LeftWall")
		{
			Turn (1);
			MoveDown ();
		}
		if (col.gameObject.name == "RightWall")
		{
			Turn (-1);
			MoveDown ();
		}

		if (col.gameObject.tag == "Bullet")
		{
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.alienDies);
			Destroy (gameObject);
		}


	}

	// Used to change the current sprite and play sounds
	public IEnumerator changeAlienSprite(){
		while (true) {
			if (spriteRenderer.sprite == startingImage) {
				spriteRenderer.sprite = altImage;
				//SoundManager.Instance.PlayOneShot (SoundManager.Instance.alienBuzz1);
			} else {
				spriteRenderer.sprite = startingImage;
				SoundManager.Instance.PlayOneShot (SoundManager.Instance.alienBuzz2);
			}

			yield return new WaitForSeconds (secBeforeSpriteChange);
		}
	}

	// Have Aliens fire bullets at random times
	void FixedUpdate(){

		if (Time.time > baseFireWaitTime) {

			baseFireWaitTime = baseFireWaitTime + 
				Random.Range (minFireRateTime, maxFireRateTime); 

			Instantiate (alienBullet, transform.position, Quaternion.identity);

		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "Player") {
			// Play exploding ship sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.shipExplosion);

			// Change to exploded ship image
			col.GetComponent<SpriteRenderer> ().sprite = explodedShipImage;

			// Destroy AlienBullet
			Destroy (gameObject);

			// Wait .5 seconds and then destroy Player
			DestroyObject (col.gameObject, 0.5f);
		}
	}


}