using UnityEngine;

public class Pickup : MonoBehaviour
{
	private const float MoveSpeed = 2;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.transform.GetComponent<PlayerController>())
		{
			SoundController.Instance.PlaySFX(SFX.StarPickup);
			PlayerStats.Instance.AddStars();
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
	}

}