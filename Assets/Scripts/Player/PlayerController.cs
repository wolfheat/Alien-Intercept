using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] List<BulletCreationPosition> bulletCreationPositions;

    private Vector2 XLimits = new Vector2(0.05f,.95f);
    private Vector2 YLimits = new Vector2(0.05f,.9f);

    private float bulletCreationTime = 0.1f;
    private float rocketCreationTime = 0.5f;

    private const float Scale = 8f/350;

    private Coroutine rockets;

    private void OnEnable()
    {
		StartCoroutine(AutoBulletSpawner(true));
		Inputs.Instance.Controls.MainActionMap.LeftClick.performed += _ => ToggleRockets();
    }

    private void ToggleRockets()
    {
        if (rockets == null) rockets = StartCoroutine(AutoRocketSpawner(true));
        else
        {
            StopCoroutine(rockets);
            rockets = null;
        }
    }

    private IEnumerator AutoRocketSpawner(bool continious)
    {
        while (continious)
        {
            foreach(BulletCreationPosition pos in bulletCreationPositions)
            if(pos.rocket) Instantiate(rocketPrefab, pos.transform.position, pos.transform.rotation,bulletHolder.transform);
            yield return new WaitForSeconds(rocketCreationTime);
        }
    }

    private IEnumerator AutoBulletSpawner(bool continious)
    {
        while (continious)
        {
            foreach(BulletCreationPosition pos in bulletCreationPositions)
            Instantiate(bulletPrefab,pos.transform.position, pos.transform.rotation,bulletHolder.transform);
            yield return new WaitForSeconds(bulletCreationTime);
        }
    }
	private void Update()
    {
        SetUnitPosition();
    }

    public virtual void SetUnitPosition()
    {
        // Get Mouse Position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 worldPositionClamped = new Vector2(
            Mathf.Clamp(worldPosition.x,GameSettings.ScreenWidth *XLimits.x, GameSettings.ScreenWidth * XLimits.y), 
            Mathf.Clamp(worldPosition.y,GameSettings.ScreenHeight*YLimits.x, GameSettings.ScreenHeight* YLimits.y)
            );        
        transform.position = worldPositionClamped;   

    }
}
