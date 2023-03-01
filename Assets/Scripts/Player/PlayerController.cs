using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Crosshair crosshair;

    private void Start()
    {
		crosshair = FindObjectOfType<Crosshair>();
    }
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
        // Move Towards Cursor
        transform.position = Vector3.MoveTowards(transform.position, crosshair.transform.position, Speed*Time.deltaTime);
    }
}
