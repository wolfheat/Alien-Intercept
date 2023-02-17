using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private float lifeTime = 2f;
    private float outOfBoundsY = 2f;
    float screenHeight;

    public float Damage { get; internal set; } = 10f;

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

    public void Die()
	{
		//Destroy(gameObject);
		gameObject.SetActive(false);
    }

    private void Move()
    {
        transform.position += transform.up * speed*Time.deltaTime;
    }
}
