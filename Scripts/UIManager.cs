using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Slider healthBar;
	public PlayerManager playerHealth;
	public GameObject ammoText;
	public static int ammoCount;

	public GameObject playerChar;
	private PlayerManager playerScript;

	void Start()
    {
		playerScript = playerChar.GetComponent<PlayerManager>();
	}

    // Update is called once per frame
    void Update()
    {
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;
	}

}
