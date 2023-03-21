using System;
using UnityEngine;

public class Pickup : MonoBehaviour, ICanGetOutOfBounds, ICollideWithPlayer
{
	private const float MoveSpeed = 2;
	public GenericPool<Pickup> pool;
	public int value = 1;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.transform.GetComponent<PlayerController>())
		{
			CollideWithPlayer();			
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
			ReturnToPool();
		}
	}

	public void ReturnToPool()
	{
		pool?.ReturnToPool(this);
		if(pool==null)Destroy(gameObject);
	}

	public void SetType(PickUpType type)
	{
		Debug.Log("Set Type");
	}

	public virtual void CollideWithPlayer()
	{
		ReturnToPool();
	}
}
