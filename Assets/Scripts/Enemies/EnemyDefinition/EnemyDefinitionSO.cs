using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/EnemyDefinition", order = 1)]
public class EnemyDefinitionSO : BaseEnemyDefinitionSO
{
	public readonly bool isBoss = false;
	public EnemyType eType;
	public EnemyMovement movement;
	public EnemyShooting shooting;
	public EnemyDefinitionSO()
	{
		
		movementString = movement.ToString();
	}
}
public class BaseEnemyDefinitionSO : ScriptableObject
{
	public EnemyController type;
	public string movementString;
	[Range(0, 5000)] public int health = 100;	
	[Range(1,10)] public int unitsAmount=1;	
	[Range(1,10)] public float timer=1;	
	[Range(0.5f,1.5f)] public float speed=1;	
}
