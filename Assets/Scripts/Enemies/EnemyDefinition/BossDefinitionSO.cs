using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/BossDefinition", order = 2)]
public class BossDefinitionSO : BaseEnemyDefinitionSO
{		
		public readonly bool isBoss = true;
		public BossType type3;
		public BossMovement movement;
		public EnemyShooting shooting;

	public BossDefinitionSO()
	{
		health = 500;
		//type = EnemyController;
		movementString = movement.ToString();
	}
}
