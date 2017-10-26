using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float timetoDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timetoDestroy -= Time.deltaTime;
		if (timetoDestroy <= 0) {
		
			Destroy (gameObject);

		}
		
	}
}
