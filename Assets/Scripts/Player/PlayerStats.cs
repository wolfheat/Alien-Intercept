using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int stars = 0;
    private int maxHealth = 100;
    private int health;


	public static PlayerStats Instance;

	private void Start()
	{
		if (Instance != null) Destroy(gameObject);
		Instance = this;
        ResetHealth();
	}

	public void AddStars(int amt = 1)
    {
        stars+=amt;
        UIHud.Instance.ShowStarAmount(stars);
    }
    	
	public bool RemoveHealth(int amt = 1)
    {
        health-=amt;
        UpdateHealthBar();
        return health < 0;
    }

	internal void ResetHealth()
	{
		health = maxHealth; 
        UpdateHealthBar();
	}

    private void UpdateHealthBar()
    {
        float barpercent = (float)health / (float)maxHealth;
		UIHud.Instance.UpdateHealthBar(barpercent);
    }

	internal void AddHealth(int value)
	{
        health = maxHealth;
        UpdateHealthBar();
	}
}
