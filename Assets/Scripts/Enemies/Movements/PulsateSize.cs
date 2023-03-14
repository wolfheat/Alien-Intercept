using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateSize : MonoBehaviour
{
    [SerializeField] GameObject thingToPulsate;
    [SerializeField,Range (0.5f,5f)] float pulseTime;
    [SerializeField, Range(0.5f, 1.5f)] float minSize;
    [SerializeField, Range(0.5f, 1.5f)] float maxSize;

    private float pulseTimer = 0;
    private bool countingUp = true;

    private void Start()
    {
        SetScale(minSize);
    }

    void Update()
    {
        DoPulse();
    }

    private void DoPulse()
    {
        if(pulseTimer+Time.deltaTime > pulseTime) countingUp = false;
        else if(pulseTimer - Time.deltaTime < 0) countingUp = true;

		pulseTimer += (countingUp?1:-1)*Time.deltaTime;

        float newSize = Mathf.Lerp(minSize, maxSize, pulseTimer/pulseTime);

        SetScale(newSize);
	}

    private void SetScale(float size)
    {
        thingToPulsate.transform.localScale = Vector3.one * size;
    }
}
