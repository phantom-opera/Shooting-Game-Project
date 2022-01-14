using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
	public bool playerDetected = false;


	
	// Start is called before the first frame update

	// Switches the boolean 'playerDetected' to true if the player enters the detection radius and switches it back to false if they leave it.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerDetected = true;
		}
	}

	void OnTriggerExit(Collider other)
	{

		if (other.tag == "Player")
		{
			playerDetected = false;
		}
	}
}
