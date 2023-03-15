using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	[SerializeField] private GameObject levelTextGameobject;
	[SerializeField] private TextMeshProUGUI text;

	public IEnumerator LevelCompletCO(int i)
	{
		text.text = "LEVEL "+ (i + 1) + " COMPLETED";
		levelTextGameobject.SetActive(true);
		yield return new WaitForSeconds(4);
		levelTextGameobject.SetActive(false);
	}
	public IEnumerator NewLevelStarting(int i)
	{
		text.text = "GET READY FOR LEVEL " + (i+1);
		levelTextGameobject.SetActive(true);
		yield return new WaitForSeconds(4);
		levelTextGameobject.SetActive(false);
	}
}
