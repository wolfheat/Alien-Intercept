using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenuInput : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject startMenu;
	[SerializeField] private GameObject buttonHolder;	
	[SerializeField] private List<Image> images;
	private LevelController levelController;
	
	private void Start()
    {
		levelController = FindObjectOfType<LevelController>();

		//Inputs.Instance.Controls.MainActionMap.Space.performed += _ => ToggleStartMenu();// = _.ReadValue<float>();
	}

	private void ToggleStartMenu()
	{
		Debug.Log("Toggle start Menu.");
		Debug.Log("Gameobject: "+ startMenu.gameObject);
		startMenu.gameObject.SetActive(!startMenu.gameObject.activeSelf);

	}

	public void StartGameClicked()
	{
		Debug.Log("StartGame Clicked");
		startMenu.gameObject.SetActive(false);
		levelController.StartLevel();
	}
	public void SettingsClicked()
	{
		Debug.Log("Settings Clicked");
	}
	public void CreditsClicked()
	{
		Debug.Log("Credits Clicked");
	}
	public void QuitGameClicked()
	{
		Debug.Log("QuitGame Clicked");	
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Something Clicked");
	}
}
