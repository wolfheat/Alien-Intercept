using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickUpType{ GoldStar, SilverStar}

public class PickUpSpawner : MonoBehaviour
{
	public static PickUpSpawner Instance;

	[SerializeField] List<Pickup> pickupsPrefab = new List<Pickup>();
	[SerializeField] GameObject pickupHolder;

	private void Start()
	{
		if(Instance!= null) Destroy(gameObject);
		Instance = this;	
	}

	public void SpawnPickup(PickUpType type, Transform transform)
	{
		var pickup = Instantiate(pickupsPrefab[(int)type],pickupHolder.transform);
		pickup.transform.position = transform.position;

	}
	public void RemoveAllPickups()
	{
		foreach (Transform pickup in pickupHolder.transform)
		{
			pickup.gameObject.SetActive(false);	
		}
	}



}
