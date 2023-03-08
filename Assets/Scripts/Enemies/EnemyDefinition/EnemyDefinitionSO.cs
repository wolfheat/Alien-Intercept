using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/EnemyDefinition", order = 1)]
public class EnemyDefinitionSO : BaseEnemyDefinitionSO
{
		public readonly bool isBoss = false;
		public EnemyType type;
		public EnemyMovement movement;
		public EnemyShooting shooting;
		//public int posID;
		[Range(1,10)] public int unitsAmount=1;	
		[Range(1,10)] public float timer=1;	
		[Range(0.5f,1.5f)] public float speed=1;	
}
public class BaseEnemyDefinitionSO : ScriptableObject
{
	[Range(0, 5000)] 
	public int health = 100;	
}
