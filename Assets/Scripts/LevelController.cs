using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerPrefab;
    [SerializeField] BackgroundController backgroundController;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] SoundController soundController;
    [SerializeField] GameObject pausedScreen;
	[SerializeField] List<GameObject> levels = new List<GameObject>();

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

	}

    private void Pause(bool p)
    {
        GameSettings.IsPaused = p;
        Time.timeScale = p ? 0 : 1;
		Cursor.visible = p;
        pausedScreen.gameObject.SetActive(p);
	}
    private void RestartLevel()
    {
        Debug.Log("Restart Level");
        SetSpawnerLevel(0);
    }
    private void SetSpawnerLevel(int level)
    {
        if (levels.Count > 0) enemySpawner.SetLevel(levels[level]);
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
