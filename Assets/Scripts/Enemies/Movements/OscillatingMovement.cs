using UnityEngine;
using UnityEngine.PlayerLoop;

interface IHaveMovement
{
    void Movement();
}
    
public class OscillatingMovement : Movement, IHaveMovement
{
	private float forwardSpeed = 0.3f;
	private float velChangePerSec = 0.04f;
	private float velChangeTime = 1f;
	private float velChangeTimer;

	private Vector2 startVel = Vector2.left;
	private Vector2 vel = Vector2.zero;

	private void Start()
	{
		velChangeTimer = velChangeTime / 2;
		PlaceRandom();
	}

	private void Update()
	{
		Movement();
	}

	public void PlaceRandom()
	{
		transform.position = new Vector2(Random.Range(3f, 9f), Random.Range(14f, 16f));
	}

	public virtual void Movement()
    {

		OscillateMovement();
		ForwardMovement();
	}

	private void ForwardMovement()
	{
		transform.position += Vector3.down * forwardSpeed * Time.deltaTime;

	}

	private void OscillateMovement()
	{
		velChangeTimer += Time.deltaTime;
		if (velChangeTimer >= velChangeTime)
		{
			velChangeTimer -= velChangeTime;
			velChangePerSec *= -1;
		}

		//Change Vel
		vel += velChangePerSec * Time.deltaTime * startVel.normalized;

		transform.position += new Vector3(vel.x, vel.y, 0);
	}
}
