using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoringSystem : MonoBehaviour
{
	public GameObject scoreText;
	public static int score;

	void Start()
	{
		score = 0;
	}

	void Update ()
	{
		scoreText.GetComponent<Text>().text = "Kills: " + score;
		CheckWinCondition();
	}

	void CheckWinCondition()
	{
		if (score >= 8)
		{
			Time.timeScale = 3.0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("WinScreen");
		}
	}


}
