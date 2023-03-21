using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickUpType{ GoldStar, SilverStar}

public class PickUpSpawner : MonoBehaviour
{
	public static PickUpSpawner Instance;

	[SerializeField] List<Pickup> pickupsPrefab = new List<Pickup>();
	[SerializeField] GameObject pickupHolder;

	public GenericPool<Pickup> Pool { get; set; }
	public List<GenericPool<Pickup>> Pools = new List<GenericPool<Pickup>>();

	private void Start()
	{
		if(Instance!= null) Destroy(gameObject);
		Instance = this;

		GeneratePools();
	}

	private void GeneratePools()
	{
		for (int i = 0; i < pickupsPrefab.Count; i++)
		{
			GenericPool<Pickup> newPool;
			//Pools.Add(newPool);
		}
	}

	public void SpawnPickup(PickUpType type, Transform t)
	{
		Pickup pickup = Pools[(int)type].Get();

		//Check if one is available
		pickup.gameObject.SetActive(true);
		pickup.transform.position = t.position;

	}

	public void RemoveAllPickups()
	{
		foreach (Transform pickup in pickupHolder.transform)
		{
			pickup.gameObject.SetActive(false);	
		}
	}



}
