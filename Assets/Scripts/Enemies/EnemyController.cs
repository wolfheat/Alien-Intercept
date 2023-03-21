using UnityEngine;


public interface ICanGetOutOfBounds { void OutOfBoundsCheck(); };
public interface ICollideWithPlayer { void CollideWithPlayer(); };

public class EnemyController : Movement, ICanGetOutOfBounds
{
    private int health = 100;
	public int Health { get { return health; } set {health = value;} }
	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            Bullet bullet = col.gameObject.GetComponent<Bullet>();

            health -= bullet.Damage;
            bullet.Die();
            if(health <= 0f) Die();                
        }
	}
	public void Die()
	{
		//Spawn Star
		int type = Random.Range(0, 2);
		if(type == 0) PickUpSpawner.Instance.SpawnPickup(PickUpType.GoldStar,transform);
		else PickUpSpawner.Instance.SpawnPickup(PickUpType.SilverStar, transform);

		ParticleSystemController.Instance.PlayParticleAt(ParticleType.EnemyBlowUpA,transform);
		SoundController.Instance.PlaySFX(SFX.ShipDestroyedA);
		gameObject.SetActive(false);

	}
    
	public void JustRemove()
	{
		Debug.Log("Enemy below game area");
		gameObject.SetActive(false);
	}


	public void OutOfBoundsCheck()
	{

		if (transform.position.y < 0f)
		{
			JustRemove();
		}
	}

	private void Update()
    {
        OutOfBoundsCheck();
	}

}
