using System.Collections;
using UnityEngine;

public class PlayerGhostController : BaseController
{
	[SerializeField] private PlayerController ghostProfile;


	protected override void OnEnable()
	{
		useBullets = ghostProfile.UsingBullets;
		useRockets = ghostProfile.UsingRockets;
		base.OnEnable();
	}

	public IEnumerator RemoveGhostAfter(float removeTime)
	{
		yield return new WaitForSeconds(removeTime);
		transform.gameObject.SetActive(false);
	}

	public override void SetUnitPosition()
	{
		// Sets Unit Position From Stored positions

		//Camera.main.ScreenToWorldPoint();
		transform.position += Vector3.left*Time.deltaTime;
	}
}
