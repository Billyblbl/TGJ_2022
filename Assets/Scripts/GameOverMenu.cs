using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable enable

public class GameOverMenu : MonoBehaviour {

	public void Retry() {
		SceneManager.LoadScene("GameScene");
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit() {
		Application.Quit();
	}

}
