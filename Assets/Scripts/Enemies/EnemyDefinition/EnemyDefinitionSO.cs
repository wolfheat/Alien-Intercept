using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/EnemyDefinition", order = 1)]
public class EnemyDefinitionSO : ScriptableObject
{
		public EnemyType type;
		public EnemyMovement movement;
		//public int posID;
		[Range(1,10)] public int unitsAmount=1;	
		[Range(1,10)] public float timer=1;	
}