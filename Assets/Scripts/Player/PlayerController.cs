using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Vector2 XLimits = new Vector2(0.05f,.95f);
    private Vector2 YLimits = new Vector2(0.05f,.9f);

    protected override void OnEnable()
    {
        useBullets = true;
        useRockets = false;
        base.OnEnable();    
		Inputs.Instance.Controls.MainActionMap.LeftClick.performed += _ => ToggleRockets();
    }

	protected override void OnDisable()
    {
        base.OnDisable();
        Inputs.Instance.Controls.MainActionMap.LeftClick.performed -= _ => ToggleRockets();
    }

	private void Update()
    {
        if (GameSettings.IsPaused) return;
        SetUnitPosition();
    }

    public override void SetUnitPosition()
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
