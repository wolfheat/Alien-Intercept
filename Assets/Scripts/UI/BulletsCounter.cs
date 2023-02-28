using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCounter : MonoBehaviour
{
	[Header("Automatically shows the amount of bullets in the hierarchy name")]
    private string mainName;

    private void Start()
    {
        mainName = gameObject.name;
    }
    void Update()
    {
        StartCoroutine(Counter());    
    }

    private IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int amount = transform.childCount;
            gameObject.name = mainName+" (" + amount+")";
        }
    }

}
