using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float jumpraycastDistance;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Jump();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		float hAxis = Input.GetAxisRaw("Horizontal");
		float vAxis = Input.GetAxisRaw("Vertical");

		Vector3 playerMove = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime;

		Vector3 movePosition = rb.position + rb.transform.TransformDirection(playerMove);

		rb.MovePosition(movePosition);
	}

	private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isGrounded())
			{
				rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			}
		}
	}

	private bool isGrounded()
	{
		Debug.DrawRay(transform.position, Vector3.down * jumpraycastDistance, Color.blue);
		return Physics.Raycast(transform.position, Vector3.down, jumpraycastDistance);
	}
}
