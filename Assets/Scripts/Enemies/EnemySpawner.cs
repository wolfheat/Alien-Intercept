using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType{EnemyA,EnemyB,AnimatedA,AnimatedB}
public enum BossType{BossA,BossB,BossC,BossD}
public enum BossMovement{ BossAInit,BossBInit, BossCInit, BossDInit }
public enum EnemyMovement{ StraightDownSlow,DownRightDownA, DownRightDownB, DownRightDownC,DownLeftDownA, DownLeftDownB, DownLeftDownC, AnimationA,AnimationB,AnimationC,AnimationD}
public enum EnemyShooting {none,A,B,C,D}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] List<EnemyController> bosses;
    [SerializeField] GameObject enemyParent;
    [SerializeField] List<GameObject> enemySubParents = new List<GameObject>();

    [SerializeField] public GameObject levelHolder;
    private float SpeedFactor = 1f;


	public GameObject currentLevel = null;
    public List<EDefinitionPoint> currentLevelPoints;
	
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
		Inputs.Instance.Controls.MainActionMap.P.performed += _ => ToggleSpeedFactor();// = _.ReadValue<float>();
	}

    private void Update()
    {
        if (GameSettings.CurrentGameState != GameState.RunGame) { if (spawn != null) StopCoroutine(spawn); return; }
        if(currentLevel!=null) RunLevel();
    }
    private void RunLevel()
    {
        List<EDefinitionPoint> pointsToRemove = new List<EDefinitionPoint>();

        //Move the parent
        levelHolder.transform.position += Vector3.down * Time.deltaTime * SpeedFactor;

        for (int i = currentLevelPoints.Count-1; i >= 0; i--)
        {
            EDefinitionPoint point = currentLevelPoints[i];
            if (point.transform.position.y <= 0f)
            {
                spawn = StartCoroutine(SpawnPointData(point));
                //pointsToRemove.Add(point);
                point.gameObject.SetActive(false);
                currentLevelPoints.RemoveAt(i);
            }
        }        
    }

    public void StopLevelSpawning()
    {
		if (spawn != null) StopCoroutine(spawn);
        spawn = null;
		Debug.Log("Stopped Spawn CO");
	}
    public void SetLevel(GameObject level)
    {
        Debug.Log("Creating Level "+level);
        if (currentLevel != null) RemoveLastLevel();
        currentLevel = Instantiate(level,levelHolder.transform);
        currentLevelPoints = new List<EDefinitionPoint>();
        Debug.Log("Creating new Level points.");
        currentLevelPoints = currentLevel.GetComponentsInChildren<EDefinitionPoint>().ToList();
        levelHolder.transform.position = new Vector3(levelHolder.transform.position.x, 0f, levelHolder.transform.position.z);  
	}

    private void RemoveLastLevel()
    {
		for (int i = currentLevelPoints.Count - 1; i >= 0; i--)
        {
            Destroy(currentLevelPoints[i].gameObject);
		}
        Destroy(currentLevel.gameObject);
        currentLevel = null;
        currentLevelPoints.Clear();
	}

    private void ToggleSpeedFactor()
    {
        SpeedFactor = (SpeedFactor == 1) ? 5 : 1;
	}
    private void SpawnByPlayerInput()
    {
        //spawn = StartCoroutine(Spawn(Random.Range(1,20), Random.Range(0.05f, 1f)));
        // SPAWN        ENEMYTYPE, POSITION, ANIMATION
        //SpawnOneAt(EnemyType.AnimatedA,5,"EnemyAnimationA");
	}

	private IEnumerator SpawnPointData(EDefinitionPoint p)
	{

        /*
		if (p.definition is BaseEnemyDefinitionSO)
		{
            var enemyDefinition = p.definition;
			int createdAmount = 0;

			while (createdAmount < enemyDefinition.unitsAmount && spawn != null)
			{
				int positionID = Mathf.RoundToInt(p.transform.localPosition.x) + 1;
                Transform positionTransform = enemySubParents[positionID].transform;
				SpawnOneMob(enemyDefinition.type, positionTransform, enemyDefinition.movementString,enemyDefinition.health);
				yield return new WaitForSeconds((float)enemyDefinition.timer);
				createdAmount++;
			}
		}*/

		
		if (p.definition is EnemyDefinitionSO)
        {

            EnemyDefinitionSO enemyDefinition = p.definition as EnemyDefinitionSO;
            int createdAmount = 0;
            while (createdAmount < enemyDefinition.unitsAmount && spawn != null)
			{
				int positionID = Mathf.RoundToInt(p.transform.localPosition.x)+1;
			    SpawnOneAt(enemyDefinition.type, positionID, enemyDefinition.movement);
			    yield return new WaitForSeconds((float)enemyDefinition.timer);
                createdAmount++;       
            }
        } 
        else if(p.definition is BossDefinitionSO)
        {
			BossDefinitionSO enemyDefinition = p.definition as BossDefinitionSO; 
            int positionID = Mathf.RoundToInt(p.transform.localPosition.x) + 1;
			SpawnOneBossAt(enemyDefinition.type, positionID, enemyDefinition.movement,enemyDefinition.health);

		}
        
		yield return new WaitForEndOfFrame();

	}

    //TODO: Fix so its not repeated, move int one

	private void SpawnOneMob(EnemyController type, Transform trans, string movement, int health)
    {
        EnemyController newEnemy = Instantiate(type, trans);
        newEnemy.Health = health;
		Animator animator = newEnemy.GetComponent<Animator>();
		if (animator != null) animator.Play(movement.ToString());
	}
	private void SpawnOneBossAt(BossType type, int posID, BossMovement movement, int health)
    {
        EnemyController newEnemy = Instantiate(bosses[(int)type], enemySubParents[posID].transform);
        newEnemy.Health = health;
		Animator animator = newEnemy.GetComponent<Animator>();
		if (animator != null) animator.Play(movement.ToString());
	}
    private void SpawnOneAt(EnemyType type, int posID, EnemyMovement movement)
    {
        EnemyController newEnemy = Instantiate(enemies[(int)type], enemySubParents[posID].transform);
        Animator animator = newEnemy.GetComponent<Animator>();
		if(animator!=null)animator.Play(movement.ToString());
	}

    public IEnumerator KillAll()
    {
        Debug.Log("KILL ALL");
		List<EnemyController> enemiesAlive = FindObjectsOfType<EnemyController>().ToList();
		foreach (EnemyController e in enemiesAlive)
        {
            e.Die();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
