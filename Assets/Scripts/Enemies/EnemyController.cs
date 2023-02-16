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

	private void Start()
    {
        velChangeTimer = velChangeTime / 2;
		PlaceRandom();
    }

    public void PlaceRandom()
    {
        transform.position = new Vector3(Random.Range(3f,9f), Random.Range(14f, 16f),0.1f);
    }

    private void Update()
    {
        Movement();
        
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
