using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] GameObject EnemyParent;
    
	private void Start()
    {
        StartCoroutine(Spawn(10,0.5f));
    }

	private IEnumerator Spawn(int amt,float t)
    {
        Debug.Log("Creating: "+amt+" enemies. 1 each :"+ t);
        int createdAmount = 0;
        //Simple spawner
        while (createdAmount < amt)
        {
            EnemyController newEnemy = Instantiate(enemyPrefab,EnemyParent.transform);
            newEnemy.PlaceRandom();
			yield return new WaitForSeconds(t);
            createdAmount++;       
        }
    }
}
