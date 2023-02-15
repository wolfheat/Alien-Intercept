using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
 
    [SerializeField] private float ScrollSpeed = 0.2f;
    [SerializeField] private List<GameObject> backgroundParts;
    [SerializeField] private GameObject backgroundParent;
    private const float ScreenWidth = 2f;
    private const float ScreenHeight = 3.5f;
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
            backgroundParts[i].transform.position = new Vector2(0, BackgroundPartHeight/2+BackgroundPartHeight * i);
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
