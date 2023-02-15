using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
 
    [SerializeField] private List<GameObject> backgroundParts;
    [SerializeField] private GameObject backgroundParent;
    private float ScrollSpeed = 5f;
    private const float ScreenWidth = 9f;
    private const float ScreenHeight = 15f;
    private const float BackgroundPartScale = 4.7f;
    private const float BackgroundPartWidth = 2f;
    private const float BackgroundPartHeight = 6.4f;

    public bool IsScrolling { get; private set; } = true;

    private void Start()
    {
        SetBackgroundPositions();
    }

    private void SetBackgroundPositions()
    {   

		for (int i = 0; i < backgroundParts.Count; i++)
        {
            backgroundParts[i].transform.position = new Vector2(0, BackgroundPartHeight/2* BackgroundPartScale + BackgroundPartHeight * i* BackgroundPartScale);
        }
        backgroundParent.transform.position = new Vector3 (ScreenWidth/2, 0, 0);

    }

    private void Update()
    {
        Scroll();
    }

    public void setScroll(bool setTo)
    {
        IsScrolling = setTo;
    }

    private void Scroll()
    {
        if (!IsScrolling)return;

        transform.position += Vector3.down * ScrollSpeed*Time.deltaTime;
    }
}
