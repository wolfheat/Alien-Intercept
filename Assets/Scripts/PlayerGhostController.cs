using System.Collections;
using UnityEngine;

public class PlayerGhostController : PlayerController
{
	public IEnumerator RemoveGhostAfter(float removeTime)
	{
		Debug.Log("Destroy Ghost Timer Set");
		yield return new WaitForSeconds(removeTime);
		Debug.Log("Inactivate Ghost");
		transform.gameObject.SetActive(false);
	}

	public override void SetUnitPosition()
	{
		// Sets Unit Position From Stored positions

		//Camera.main.ScreenToWorldPoint();
		transform.position += Vector3.left*Time.deltaTime;
	}
}
