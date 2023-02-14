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
	
	private List<string> names = new List<string>() {"Start", "Settings","Credits","Quit"};
		 

	private void Start()
    {
		Inputs.Instance.Controls.MainActionMap.Space.performed += _ => ToggleStartMenu();// = _.ReadValue<float>();

		NameButtons();
	}


	private void NameButtons()
	{
		int i = 0;
		foreach (var button in buttonHolder.GetComponentsInChildren<Button>())
		{
			button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = names[i];
			i++;
		}
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
