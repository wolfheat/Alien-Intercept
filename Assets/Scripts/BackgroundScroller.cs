using System;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
 
    [SerializeField] private float ScrollSpeed = 0.2f;

    public bool IsScrolling { get; private set; } = true;

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
