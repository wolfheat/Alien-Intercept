using System;
using UnityEngine;

public class ChargerMovement : OscillatingMovement
{
	private float chargeSpeed = 4f;
	private float chargeSpeedup = 8f;
	private float chargeTimer = 0;
	private float chargeTime = 3f;

	public override void Movement()
	{
		if (GameSettings.IsPaused) return;
		chargeTimer += Time.deltaTime;
		if (chargeTimer < chargeTime) base.Movement();
		else ChargMovement();
	}

	private void ChargMovement()
	{
		chargeSpeed += Time.deltaTime*chargeSpeedup;
		transform.position += Vector3.down * chargeSpeed * Time.deltaTime;
	}
}
