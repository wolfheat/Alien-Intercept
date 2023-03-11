using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	[SerializeField] private GameObject gameObject;
	public IEnumerator LevelCompletCO()
	{
		gameObject.SetActive(true);
		yield return new WaitForSeconds(4);
		gameObject.SetActive(false);
	}
}
