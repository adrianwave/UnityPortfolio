using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int damageToGive;
	private int currentDamage;
	public GameObject damageNumber;

	PlayerStats thePS;

	// Use this for initialization
	void Start () {
		thePS = FindObjectOfType<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		 if(other.gameObject.name == "Player")
		{
			currentDamage = damageToGive - thePS.currentDefense;

			if (currentDamage <= 0) {
			
				currentDamage = 1;

			}

			other.gameObject.GetComponent<PlayerHealthManager> ().hurtPlayer (currentDamage);

			var clone = (GameObject)Instantiate (damageNumber, other.transform.position, Quaternion.Euler (Vector3.zero));
			clone.GetComponent<FloatingNumbers> ().damageNumber = currentDamage;
		}
	}
}
 