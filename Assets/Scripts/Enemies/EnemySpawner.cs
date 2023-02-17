using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] GameObject EnemyParent;

    private Coroutine spawn;

	private void Start()
    {        
		Inputs.Instance.Controls.MainActionMap.G.performed += _ => SpawnByPlayerInput();// = _.ReadValue<float>();
	}

    public void StartSpawnRoutine(int type)
    {
		spawn = StartCoroutine(Spawn(40, 0.5f));
	}
    
    private void SpawnByPlayerInput()
    {
        spawn = StartCoroutine(Spawn(Random.Range(1,20), Random.Range(0.05f, 1f)));
    }

    private IEnumerator Spawn(int amt,float t)
    {
        Debug.Log("Creating: "+amt+" enemies. 1 each :"+ t);
        int createdAmount = 0;
        //Simple spawner
        while (createdAmount <= amt)
        {
            EnemyController newEnemy = Instantiate(enemies[0],EnemyParent.transform);
            newEnemy.PlaceRandom();
			yield return new WaitForSeconds(t);
            createdAmount++;       
        }
        createdAmount = 0;
		while (createdAmount <= amt)
        {
            EnemyController newEnemy = Instantiate(enemies[1],EnemyParent.transform);
            newEnemy.PlaceRandom();
			yield return new WaitForSeconds(t);
            createdAmount++;       
        }
    }
}
