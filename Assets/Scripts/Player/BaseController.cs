using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] GameObject bulletHolder;
	[SerializeField] GameObject rocketPrefab;
	[SerializeField] List<BulletCreationPosition> bulletCreationPositions;

	private float bulletCreationTime = 0.1f;
	private float rocketCreationTime = 0.5f;

	protected bool useBullets = false;
	protected bool useRockets = false;

	public bool UsingBullets { get { return useBullets; } }
	public bool UsingRockets { get { return useRockets; } }

	protected Coroutine bullets;
	protected Coroutine rockets;

	protected virtual void OnEnable()
	{
		if (useBullets) bullets = StartCoroutine(AutoBulletSpawner());
		if (useRockets) rockets = StartCoroutine(AutoRocketSpawner());
		Debug.Log("Base controller class, usebullets: "+useBullets);
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
			rockets = StartCoroutine(AutoRocketSpawner());
		}
		else if(rockets != null)StopCoroutine(rockets);
	}

	protected IEnumerator AutoBulletSpawner()
	{
		while (useBullets == true)
		{
			yield return new WaitForSeconds(bulletCreationTime);
			foreach (BulletCreationPosition pos in bulletCreationPositions)
				Instantiate(bulletPrefab, pos.transform.position, pos.transform.rotation, bulletHolder.transform);
		}
	}

	protected IEnumerator AutoRocketSpawner()
	{
		while (useRockets == true)
		{
			yield return new WaitForSeconds(rocketCreationTime);
			foreach (BulletCreationPosition pos in bulletCreationPositions)
				if (pos.rocket) Instantiate(rocketPrefab, pos.transform.position, pos.transform.rotation, bulletHolder.transform);
		}
	}
	private void Update()
	{
		if (GameSettings.IsPaused) return;
		SetUnitPosition();
	}

	public abstract void SetUnitPosition();
}
