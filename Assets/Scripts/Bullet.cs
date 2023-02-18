using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private float lifeTime = 2f;
    private float outOfBoundsY = 2f;
    float screenHeight;

    public int Damage { get {return damage;}}
    [SerializeField] private int damage = 10;

    private void Update()
    {
        // Move forward
        Move();
        LifeDecrease();
        OutOfBoundsCheck();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystemController.Instance.PlayParticleAt(ParticleType.BulletSplatter, transform);
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
