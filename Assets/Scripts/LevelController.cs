using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerPrefab;
    [SerializeField] BackgroundController backgroundController;
    [SerializeField] EnemySpawner enemySpawner;
	[SerializeField] List<GameObject> levels = new List<GameObject>();


	private void Start()
    {
        
    }

	public void StartLevel()
    {
        // Add player
        ActivatePlayer();

        // Add background
        SetBackground();
        ActivateBackground();

        // Activate Enemy Spawner
        SetSpawnerLevel();

    }

    private void SetSpawnerLevel()
    {
        if (levels.Count > 0) enemySpawner.SetLevel(levels[0]);
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
