using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    private float Speed = 0.1f;
    private Vector2 startVel = Vector2.left;
    private Vector2 vel = Vector2.zero;
    private float forwardSpeed = 0.3f;
    private float velChangePerSec = 0.04f;
    private float velChangeTime = 1f;
    private float velChangeTimer;

    private float health = 100;



	private void Start()
    {
        velChangeTimer = velChangeTime / 2;
		PlaceRandom();
    }

    public void PlaceRandom()
    {
        transform.position = new Vector2(Random.Range(3f,9f), Random.Range(14f, 16f));
    }
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
		ParticleSystemController.Instance.PlayParticleAt(ParticleType.enemyBlowUpA,transform);
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
        Movement();
        OutOfBoundsCheck();

	}

    protected virtual void Movement()
    {
		OscillatingMovement();
		ForwardMovement();
	}

    private void ForwardMovement()
    {
        transform.position += Vector3.down* forwardSpeed*Time.deltaTime;

	}

    private void OscillatingMovement()
    {
        velChangeTimer += Time.deltaTime;
        if(velChangeTimer>= velChangeTime)
        {
            velChangeTimer -= velChangeTime;
			velChangePerSec *= -1;
        }

        //Change Vel
        vel += velChangePerSec * Time.deltaTime * startVel.normalized;

        transform.position += new Vector3(vel.x,vel.y,0);
    }
}
