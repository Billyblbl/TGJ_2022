using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#nullable enable

public class MenuManager : MonoBehaviour {

	public GameObject?	pauseMenu;
	public PlayerInput? input;
	public float endOfGameDelay = 3f;
	public string mainMenuSceneName = "MainMenu";
	public string gameOverScreenScene = "GameOver";
	public string gameWonScreenScene = "GameWon";

	private void Update() {
		if (input != null && input.actions["Pause"].triggered) TogglePause();
	}

	public void TogglePause() {
		if (pauseMenu!.activeSelf) Resume();
		else Pause();
	}

	public void Pause() {
		Time.timeScale = 0f;
		pauseMenu?.SetActive(true);
	}

	public void Resume() {
		Time.timeScale = 1f;
		pauseMenu?.SetActive(false);
	}

	public void Restart() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(mainMenuSceneName);
	}

	public void Quit() {
		Application.Quit();
	}

	IEnumerator	Transition(string sceneName) {
		yield return new WaitForSeconds(endOfGameDelay);
		SceneManager.LoadScene(sceneName);
		yield return null;
	}

	public void Win() => StartCoroutine(Transition(gameWonScreenScene));

	public void Lose() => StartCoroutine(Transition(gameOverScreenScene));
}
