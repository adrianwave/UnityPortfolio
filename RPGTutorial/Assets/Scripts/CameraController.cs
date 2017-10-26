using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float playerCameraDistance { get; set; }
	public Transform cameraTarget;

	Camera playerCamera;
	float zoomSpeed = 35f;

	void Start(){
	
		playerCameraDistance = 12f;
		playerCamera = GetComponent<Camera>();

	}

	void Update()
	{
		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0) {

			float scroll = Input.GetAxis("Mouse ScrollWheel");
			playerCamera.fieldOfView -= scroll * zoomSpeed;
			playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, 15, 32);

		}
		transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y + playerCameraDistance, cameraTarget.position.z - playerCameraDistance);
	}


}
