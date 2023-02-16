using System;
using UnityEngine;

public class ChargerController : EnemyController
{
	private float chargeSpeed = 4f;
	private float chargeSpeedup = 8f;
	private float chargeSpeedupTimer = 0f;
	private float chargeTimer = 0;
	private float chargeTime = 3f;


	protected override void Movement()
	{
		chargeTimer += Time.deltaTime;
		if (chargeTimer < chargeTime) base.Movement();
		else ChargerMovement();
	}

	private void ChargerMovement()
	{
		chargeSpeed += Time.deltaTime*chargeSpeedup;
		transform.position += Vector3.down * chargeSpeed * Time.deltaTime;
	}
}
