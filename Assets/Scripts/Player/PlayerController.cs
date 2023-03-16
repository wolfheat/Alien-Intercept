using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Crosshair crosshair;
    private LevelController levelController;
    private Vector3 startPos = new Vector3(4.5f, -2, 0);

	private void Start()
    {
		crosshair = FindObjectOfType<Crosshair>();
        levelController = FindObjectOfType<LevelController>();
		gameObject.transform.position = startPos;
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

    protected override void GettingHit(int dmg)
    {
        Debug.Log("Player hit by dmg: " + dmg);
		bool willDie = PlayerStats.Instance.RemoveHealth(dmg);
		base.GettingHit();

        if (willDie) Die();
    }
    public override void SetUnitPosition()
    {
        // Move Towards Cursor
        transform.position = Vector3.MoveTowards(transform.position, crosshair.transform.position, Speed*Time.deltaTime);
    }

	public void Die()
	{
		ParticleSystemController.Instance.PlayParticleAt(ParticleType.EnemyBlowUpA, transform);
		SoundController.Instance.PlaySFX(SFX.ShipDestroyedA);
		gameObject.SetActive(false);

        levelController.DieAndRestart();
        gameObject.transform.position = startPos;

	}

}
