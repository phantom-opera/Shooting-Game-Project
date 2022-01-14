using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{


	public AudioSource playerDamageSound;
	public float playerMaxHealth;
	public float playerCurrentHealth;

	public float waitTime = 1f;

	// Start is called before the first frame update
	void Start()
	{

		playerCurrentHealth = playerMaxHealth;

	}

	// Update is called once per frame
	void Update()
	{

		if (playerCurrentHealth < 1) // if player's health is below 1, sends them to the lose screen
		{
			Time.timeScale = 3.0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("LoseScreen");

		}

	}


	public void DamagePlayer(int damageToGive)
	{
	
			playerCurrentHealth -= damageToGive;
		    playerDamageSound.Play();

	}

	public void SetMaxHealth()
	{
		playerCurrentHealth = playerMaxHealth;
	}

}
