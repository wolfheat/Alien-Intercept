using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerPrefab;
    [SerializeField] BackgroundController backgroundController;
    [SerializeField] EnemySpawner enemySpawner;

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
        ActivateEnemySpawner();

    }

    private void ActivateEnemySpawner()
    {
        enemySpawner.StartSpawnRoutine(0);
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
