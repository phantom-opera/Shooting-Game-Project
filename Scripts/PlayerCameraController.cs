using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour

	
{
	[SerializeField] private float lookSensitivity;
	[SerializeField] private float smoothing;
	[SerializeField] private int maxLookRotation;

	private GameObject player;
	private Vector2 smoothedVelocity;
	private Vector2 currentLookingPos;

	void Start()
    {
		player = transform.parent.gameObject;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
		RotateCamera();
    }

	private void RotateCamera()
	{
		Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		input = Vector2.Scale(input, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));
		smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, input.x, 1f / smoothing);
		smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, input.y, 1f / smoothing);

		currentLookingPos += smoothedVelocity;

		currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -maxLookRotation, maxLookRotation);
		transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
	}
}
