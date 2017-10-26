using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour {

	private Rigidbody2D rigidBody;

	public float speed = 30;

	// Exploded Ship Image
	public Sprite explodedShipImage;

	// Use this for initialization
	void Start () {

		// Get reference to the ball Rigidbody
		rigidBody = GetComponent<Rigidbody2D>();

		// When the ball is created move it down
		// at the desired speed
		rigidBody.velocity = Vector2.down * speed;

	}

	// Called every time a ball collides with something
	// the object it hit is passed as a parameter
	void OnTriggerEnter2D(Collider2D col)
	{
		// If Bullet hits a wall destroy bullet
		if (col.tag == "Wall") {
			Destroy (gameObject);
		}

		// If Bullet hits Player destroy Alien and Bullet
		if(col.gameObject.tag == "Player")
		{
			// Play exploding ship sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.shipExplosion);

			// Change to exploded ship image
			col.GetComponent<SpriteRenderer> ().sprite = explodedShipImage;

			// Destroy AlienBullet
			Destroy (gameObject);

			// Wait .5 seconds and then destroy Player
			DestroyObject(col.gameObject, 0.5f);

		}

		// If Alien Bullet hits Shield destroy both
		if (col.tag == "Shield") {
			Destroy (gameObject);
			DestroyObject(col.gameObject);
		}
	}

	// Called when the Game Object isn't visible
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}