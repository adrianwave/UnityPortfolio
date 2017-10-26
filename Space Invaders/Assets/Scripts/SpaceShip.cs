using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

	public float speed = 30;

	// Holds a reference to Bullet objects
	public GameObject theBullet;

	void FixedUpdate(){
		float horzMove = Input.GetAxisRaw ("Horizontal");

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (horzMove, 0) * speed;

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		// GetButtonDown will only fire once per click 
		// Jump is assigned to the Space Bar
		if (Input.GetButtonDown ("Jump")) {

			// Create a Bullet at transform.position which
			// is the ships current location
			// Quaternion.identity adds Bullet with no rotation
			Instantiate (theBullet, transform.position, Quaternion.identity);

			// Play bullet fire sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.bulletFire);
		}

	}
}