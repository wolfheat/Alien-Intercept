using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType{EnemyA,EnemyB,AnimatedA,AnimatedB}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] GameObject enemyParent;
    [SerializeField] List<GameObject> enemySubParents = new List<GameObject>();

	//  0   1   2   3   4   5   6   7   8   9   10
	//      |               *               |
	/*
    VALID STARTPOS
    EnemyAnimationA = 2-7
    EnemyAnimationB = 1,2,3


    */
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
        //spawn = StartCoroutine(Spawn(Random.Range(1,20), Random.Range(0.05f, 1f)));
        // SPAWN        ENEMYTYPE, POSITION, ANIMATION
        SpawnOneAt(EnemyType.AnimatedA,5,"EnemyAnimationA");
	}

    private void SpawnOneAt(EnemyType type, int steps, string animation="EnemyAnimationA")
    {
        EnemyController newEnemy = Instantiate(enemies[(int)type], enemySubParents[steps].transform);
        newEnemy.GetComponent<Animator>().Play(animation);
	}

	private IEnumerator Spawn(int amt,float t)
    {
        int createdAmount = 0;

        while (createdAmount <= amt)
        {
            EnemyController newEnemy = Instantiate(enemies[0],enemyParent.transform);
			yield return new WaitForSeconds(t);
            createdAmount++;       
        }
        createdAmount = 0;
		while (createdAmount <= amt)
        {
            EnemyController newEnemy = Instantiate(enemies[1],enemyParent.transform);
			yield return new WaitForSeconds(t);
            createdAmount++;       
        }
    }
}
