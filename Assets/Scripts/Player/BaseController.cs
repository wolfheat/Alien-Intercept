using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] GameObject rocketPrefab;
	[SerializeField] List<BulletCreationPosition> bulletCreationPositions;
	[SerializeField] List<BulletCreationPosition> rocketCreationPositions;
	[SerializeField] protected RocketPool rocketPool;
	[SerializeField] protected BulletPool bulletPool;
	[SerializeField] protected SimpleFlashEffect flashEffect;

	private float bulletCreationTime = 0.1f;
	private float rocketCreationTime = 0.5f;
	

	protected bool useBullets = false;
	protected bool useRockets = false;

	public bool UsingBullets { get { return useBullets; } }
	public bool UsingRockets { get { return useRockets; } }
	public float Speed { get; set; } = 25f;

	protected Coroutine bullets;
	protected Coroutine rockets;


	private void OnCollisionEnter2D(Collision2D collision)
	{		
		if(collision.gameObject.GetComponent<EnemyController>() != null)
		GettingHit();
	}

	protected virtual void OnEnable()
	{
		if (useBullets) bullets = StartCoroutine(AutoSpawner(bulletPool, bulletCreationPositions,bulletCreationTime));
		if (useRockets) rockets = StartCoroutine(AutoSpawner(rocketPool, rocketCreationPositions, rocketCreationTime));
	}

	protected virtual void OnDisable()
	{
		if(bullets!=null) StopCoroutine(bullets);
		if(rockets!=null) StopCoroutine(rockets);
	}

	protected void ToggleRockets()
	{
		useRockets = !useRockets;
		if (useRockets)
		{
			if (rockets != null) StopCoroutine(rockets);
			rockets = StartCoroutine(AutoSpawner(rocketPool, rocketCreationPositions, rocketCreationTime));	
		}
		else if(rockets != null)StopCoroutine(rockets);
	}

	protected IEnumerator AutoSpawner(GenericPool<Bullet> pool, List<BulletCreationPosition> list, float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			if(GameSettings.IsPaused)continue;
			foreach (BulletCreationPosition pos in list)
			{
				Bullet newBullet = pool.Get();
				newBullet.Pool = pool;
				newBullet.transform.position = pos.transform.position;
				newBullet.transform.rotation = pos.transform.rotation;
				newBullet.transform.parent = pool.gameObject.transform;
			}
		}
	}

	public void GettingHit()
	{
		Debug.Log("GETTIGN HIT");
		Debug.Log("Flash Effect: "+flashEffect);
		if (flashEffect != null)
			flashEffect.DoSimpleFlash();
	}
	private void Update()
	{
		if (GameSettings.IsPaused) return;
		SetUnitPosition();
	}

	public abstract void SetUnitPosition();
}
