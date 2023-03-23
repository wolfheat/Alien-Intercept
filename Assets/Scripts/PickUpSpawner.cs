using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickUpType{ GoldStar, SilverStar, Health}

public class PickUpSpawner : MonoBehaviour
{
	public static PickUpSpawner Instance;

	//[SerializeField] List<Pickup> pickupsPrefab = new List<Pickup>();
	[SerializeField] GameObject pickupHolder;

	public GenericPool<Pickup> Pool { get; set; }
	public List<GenericPool<Pickup>> Pools = new List<GenericPool<Pickup>>();

	private void Start()
	{
		if(Instance!= null) Destroy(gameObject);
		Instance = this;

	}

	public void SpawnPickup(PickUpType type, Transform t)
	{
		Pickup pickup = Pools[(int)type].Get();
		pickup.pool = Pools[(int)type];
		pickup.value = (int)type + 1;
		pickup.gameObject.SetActive(true);
		pickup.transform.position = t.position;
		pickup.transform.parent = Pools[(int)type].transform;

	}

	public void RemoveAllPickups()
	{
		foreach (Transform pickupParent in pickupHolder.transform)
		{
			foreach (Transform pickup in pickupParent.transform)
			{
				pickup.gameObject.SetActive(false);
			}
		}
	}



}
