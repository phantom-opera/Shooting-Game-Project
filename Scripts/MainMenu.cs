using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string newGameScene;

	public void newGame()
	{
		SceneManager.LoadScene(newGameScene);
	}

	public void control()
	{
		SceneManager.LoadScene("Controls");
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
