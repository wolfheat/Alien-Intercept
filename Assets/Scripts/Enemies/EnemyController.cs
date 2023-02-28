using UnityEngine;

public class EnemyController : Movement
{
    private float health = 100;

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
		ParticleSystemController.Instance.PlayParticleAt(ParticleType.EnemyBlowUpA,transform);
		SoundController.Instance.PlaySFX(SFX.ShipDestroyedA);
		gameObject.SetActive(false);
	}
    
	public void JustRemove()
	{
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
