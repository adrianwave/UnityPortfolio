using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

	public float moveSpeed;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;
	private Rigidbody2D myRigidBody;
	private bool isWalking;
	public float walkTime;
	public float waitTime;
	private float walkCounter;
	private float waitCounter;

	private int walkDirection;

	public Collider2D walkZone;

	private bool hasWalkZone;

	public bool canMove;
	private DialogueManager DM;

	// Use this for initialization
	void Start () {

		DM = FindObjectOfType<DialogueManager> ();

		waitCounter = waitTime;
		walkCounter = walkTime;
		myRigidBody = GetComponent<Rigidbody2D>();

		chooseDirection ();

		if (walkZone != null) {
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		}

		canMove = true;

	}
	
	// Update is called once per frame
	void Update () {

		if (!DM.dialogueActive) {
		
			canMove = true;

		}

		if (!canMove) {
		
			myRigidBody.velocity = Vector2.zero;
			return;

		}

		if (isWalking == true) {

			walkCounter -= Time.deltaTime;

			switch (walkDirection)
			{
			case 0:
				myRigidBody.velocity = new Vector2 (0, moveSpeed);
				if(hasWalkZone == true && transform.position.y > maxWalkPoint.y)
				{
					isWalking = false;
					waitCounter = waitTime;
				}
				break;
			case 1:
				myRigidBody.velocity = new Vector2 (moveSpeed, 0);
				if(hasWalkZone == true && transform.position.x > maxWalkPoint.x)
				{
					isWalking = false;
					waitCounter = waitTime;
				}
				break;
			case 2:
				myRigidBody.velocity = new Vector2 (0, -moveSpeed);
				if(hasWalkZone == true && transform.position.y <  minWalkPoint.y)
				{
					isWalking = false;
					waitCounter = waitTime;
				}
				break;
			case 3:
				myRigidBody.velocity = new Vector2 (-moveSpeed, 0);
				if(hasWalkZone == true && transform.position.x < minWalkPoint.x)
				{
					isWalking = false;
					waitCounter = waitTime;
				}
				break;
			}

			if (walkCounter <= 0) {

				isWalking = false;
				waitCounter = waitTime;
			}


		} else {
		
			waitCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;
			if (waitCounter <= 0) {
			
			
				chooseDirection ();

			}

		}
	}

	public void chooseDirection()
	{
		walkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}
}
