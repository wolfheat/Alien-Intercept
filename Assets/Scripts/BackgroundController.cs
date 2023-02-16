using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
 
    [SerializeField] private GameObject backgroundParent;
    [SerializeField] private List<BackgroundByParts> backgrounds = new List<BackgroundByParts>();
    private BackgroundByParts activeBackground = null;

    private const float ScreenWidth = 9f;
    private int currentLevel = 0;

    public bool IsScrolling { get; set; } = false;

    private void SetLevelPosition()
    {
		activeBackground.transform.position = new Vector3(ScreenWidth / 2, 0, 0);
	}


    private void Update()
    {
		if(IsScrolling)activeBackground.Scroll();
    }

    public void setScroll(bool setTo)
    {
        IsScrolling = setTo;
    }


	public void SetLevel(int lev)
	{
        currentLevel = lev;
        activeBackground = backgrounds[lev];

        Debug.Log("Set Level "+ lev + " active");
		activeBackground.gameObject.SetActive(true);

        SetLevelPosition();
	}
}
