using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerPrefab;
    [SerializeField] BackgroundController backgroundController;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] SoundController soundController;
    [SerializeField] GameObject pausedScreen;
	[SerializeField] List<GameObject> levels = new List<GameObject>();
    private int currentLevel = 0;
    [SerializeField] Transition transition;



	private void Start()
    {
		Inputs.Instance.Controls.MainActionMap.R.performed += _ => RestartLevel();// = _.ReadValue<float>();
        Inputs.Instance.Controls.MainActionMap.ESC.performed += _ => Pause(!GameSettings.IsPaused);// = _.ReadValue<float>();
	}

	public void StartLevel()
    {
        // Add player
        ActivatePlayer();

        // Add background
        SetBackground();
        ActivateBackground();

        // Activate Enemy Spawner
        SetSpawnerLevel(0);

        //Start Normal Music
        soundController.SetMusicType(MusicType.Normal);

        //UnPause
		Pause(false);
        GameSettings.AtMenu = false;
	}

    private void Pause(bool p)
    {
        GameSettings.IsPaused = p;
        Time.timeScale = p ? 0 : 1;
		Cursor.visible = p;
        pausedScreen.gameObject.SetActive(p);
	}

    private delegate void TransitionCallback();

    private IEnumerator DoTransitionThenSetLevel(int level)
    {

		GameSettings.CurrentGameState = GameState.Transition;
        enemySpawner.StopLevelSpawning();

		// Kill all enemies
		yield return StartCoroutine(enemySpawner.KillAll());

        // Start Fade
        //transition.DoTransition(() => { SetSpawnerLevel(0); });
        yield return StartCoroutine(transition.Darken());
        Debug.Log("Transition FADE complete");

        // Load new Level
        SetSpawnerLevel(level);

        // Remove Fade (can be changed to yield return to make it wait before starting game again)
		StartCoroutine(transition.Lighten());

		GameSettings.CurrentGameState = GameState.RunGame;
		Debug.Log("Restart Level");
	}

    private void Update()
    {
		int enemiesRemaining = FindObjectsOfType<EnemyController>().ToArray().Length;
		int pointsRemaining = enemySpawner.currentLevelPoints.Count;
		UIHud.Instance.SetEnemiesRemaining(pointsRemaining, enemiesRemaining);

		if (enemiesRemaining == 0 && pointsRemaining == 0 && GameSettings.CurrentGameState == GameState.RunGame) StartNextLevel();
	}

	private void StartNextLevel()
    {
        if(GameSettings.AtMenu) return; // Do not restart the level if game has not started yet and player is at the menu

        currentLevel = (currentLevel + 1) % levels.Count;

		StartCoroutine(DoTransitionThenSetLevel(currentLevel));
        
    }
	private void RestartLevel()
    {
        if(GameSettings.AtMenu) return; // Do not restart the level if game has not started yet and player is at the menu

		StartCoroutine(DoTransitionThenSetLevel(currentLevel));
        
    }
    private void SetSpawnerLevel(int level)
    {
        Debug.Log("Spawner Level set to: "+level+" Loading that level.");
        if (levels.Count > level) enemySpawner.SetLevel(levels[level]);
        else Debug.LogError("Forgot to assign levels to LevelController");
	}

    private void ActivateBackground()
    {
        backgroundController.IsScrolling = true;
    }

    private void SetBackground()
    {
        backgroundController.SetLevel(0);
	}

    private void ActivatePlayer()
    {
		playerControllerPrefab.gameObject.SetActive(true);

	}
}
