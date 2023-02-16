using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private float lifeTime = 2f;
    private float outOfBoundsY = 2f;
    float screenHeight;

    private void Update()
    {
        // Move forward
        Move();
        LifeDecrease();
        OutOfBoundsCheck();

    }

    private void OutOfBoundsCheck()
	{

		if (transform.position.y > GameSettings.ScreenHeight)
        {        
            Die();
        }
    }

    private void LifeDecrease()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += transform.up * speed*Time.deltaTime;
    }
}
