using System;
using UnityEngine;

public class Pickup : MonoBehaviour, ICanGetOutOfBounds, ICanCollideWithPlayer
{
	private const float MoveSpeed = 2;
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.transform.GetComponent<PlayerController>())
		{
			CollidingWithPlayer();			
		}
	}

	private void Update()
	{
		transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
		OutOfBoundsCheck();
	}

	public void OutOfBoundsCheck()
	{
		if (transform.position.y < 0f)
		{
			JustRemove();
		}
	}

	public void JustRemove()
	{
		gameObject.SetActive(false);
	}

	public void SetType(PickUpType type)
	{
		Debug.Log("Set Type");
	}

	public virtual void CollidingWithPlayer()
	{		
		Destroy(gameObject);
	}
}
