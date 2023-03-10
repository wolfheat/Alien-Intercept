using UnityEngine;

public class EnemyController : Movement
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
		PickUpSpawner.Instance.SpawnPickup(PickUpType.GoldStar,transform);

		ParticleSystemController.Instance.PlayParticleAt(ParticleType.EnemyBlowUpA,transform);
		SoundController.Instance.PlaySFX(SFX.ShipDestroyedA);
		gameObject.SetActive(false);

	}
    
	public void JustRemove()
	{
		Debug.Log("Enemy below game area");
		gameObject.SetActive(false);
	}


	private void OutOfBoundsCheck()
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
