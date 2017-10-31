using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public float followSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        iTween.MoveUpdate(this.gameObject, iTween.Hash("position", target, "speed", followSpeed, "easetype", iTween.EaseType.easeInOutSine));
	}
}
