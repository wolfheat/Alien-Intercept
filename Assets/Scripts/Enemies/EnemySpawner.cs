using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType{EnemyA,EnemyB,AnimatedA,AnimatedB}
public enum EnemyMovement{ StraightDownSlow,DownRightDownA, DownRightDownB, DownRightDownC,DownLeftDownA, DownLeftDownB, DownLeftDownC, AnimationA,AnimationB,AnimationC,AnimationD}
public enum EnemyShooting {none,A,B,C,D}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] GameObject enemyParent;
    [SerializeField] List<GameObject> enemySubParents = new List<GameObject>();

    [SerializeField] public GameObject levelHolder;
    public GameObject currentLevel = null;
    public List<EDefinitionPoint> currentLevelPoints;
	List<EDefinitionPoint> pointsToRemove = new List<EDefinitionPoint>();
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
    private void Update()
    {
        if(currentLevel!=null) RunLevel();
    }
    private void RunLevel()
    {
        
        foreach(EDefinitionPoint point in currentLevelPoints)
        {
            point.transform.position += Vector3.down * Time.deltaTime;
            if (point.transform.position.y <= 0f)
            {

                StartCoroutine(SpawnPointData(point));
                pointsToRemove.Add(point);
            }
        }
        for (int i = pointsToRemove.Count-1; i >= 0; i--)
        {
            currentLevelPoints.Remove(pointsToRemove[i]);
            pointsToRemove[i].gameObject.SetActive(false);
        }
        pointsToRemove.Clear();
    }

    public void SetLevel(GameObject level)
    {
        Debug.Log("Set Level");
        if (currentLevel != null) RemoveLastLevel();

        currentLevel = Instantiate(level,levelHolder.transform);
        currentLevelPoints = new List<EDefinitionPoint>();
        currentLevelPoints = currentLevel.GetComponentsInChildren<EDefinitionPoint>().ToList();
	}

    private void RemoveLastLevel()
    {
		foreach (EDefinitionPoint point in currentLevelPoints)
		{
            Destroy(point.gameObject);
		}
        Destroy(currentLevel.gameObject);
        currentLevelPoints.Clear();
	}

    private void SpawnByPlayerInput()
    {
        //spawn = StartCoroutine(Spawn(Random.Range(1,20), Random.Range(0.05f, 1f)));
        // SPAWN        ENEMYTYPE, POSITION, ANIMATION
        //SpawnOneAt(EnemyType.AnimatedA,5,"EnemyAnimationA");
	}

	private IEnumerator SpawnPointData(EDefinitionPoint p)
    {
        
		EnemyDefinitionSO enemyDefinition = p.definition;
        int createdAmount = 0;
        while (createdAmount < enemyDefinition.unitsAmount)
        {
            int positionID = Mathf.RoundToInt(p.transform.localPosition.x)+1;
			SpawnOneAt(enemyDefinition.type, positionID, enemyDefinition.movement);
			yield return new WaitForSeconds((float)enemyDefinition.timer);
            createdAmount++;       
        }
    }

    private void SpawnOneAt(EnemyType type, int posID, EnemyMovement movement)
    {
        EnemyController newEnemy = Instantiate(enemies[(int)type], enemySubParents[posID].transform);
        //Debug.Log("Spawning an Enemy of type: "+type+" at pos: "+posID+" using Animation: "+movement.ToString());
        newEnemy.GetComponent<Animator>().Play(movement.ToString());
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
