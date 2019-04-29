using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
	STARTING,
	LOADING,
	READY,
	PLAYING,
	PAUSED
}

public class GameStateManager : MonoBehaviour
{
	[HideInInspector]
	public GameState status = GameState.STARTING;

	public GameObject startSign ;
	public GameObject loadingSign;
	public UIScreen inGameScreen ;
	public UIScreen endGameScreen ;

	private IEnumerator loadGame()
	{
		if (loadingSign != null) loadingSign.SetActive(true);

		yield return new WaitForSecondsRealtime(1.3f);

		AsyncOperation op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

		while (!op.isDone)
		{
			yield return op.isDone;
		}

		if (startSign != null) startSign.SetActive(true);

		if (loadingSign != null) loadingSign.SetActive(false);

		status = GameState.READY;

	}

    // Start is called before the first frame update
    void Start()
    {
		if (startSign != null) startSign.SetActive(false) ;

		StartCoroutine(loadGame());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator reloGame()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

		while (!op.isDone)
		{
			yield return op.isDone;
		}

		if (startSign != null) startSign.SetActive(true);

		if (loadingSign != null) loadingSign.SetActive(false);

		status = GameState.READY;

		yield return new WaitForEndOfFrame();

		op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

		while (!op.isDone)
		{
			yield return op.isDone;
		}

		if (startSign != null) startSign.SetActive(true);

		if (loadingSign != null) loadingSign.SetActive(false);
	}

	public void endGame()
	{
		endGameScreen.EnableMe();
	}
}
