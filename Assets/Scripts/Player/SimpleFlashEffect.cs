using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlashEffect : MonoBehaviour
{
	[SerializeField] private Material flashMaterial;

	private const float duration = 0.1f;

	private SpriteRenderer spriteRenderer;
	private Material baseMaterial;
	private Coroutine flashCoroutine;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		baseMaterial = spriteRenderer.sharedMaterial;
	}

	public void DoSimpleFlash()
	{
		Debug.Log("DO SIMPLE FLASH");
		if (flashCoroutine == null)
			flashCoroutine = StartCoroutine(SimpleFlashCO());
	}

	private IEnumerator SimpleFlashCO()
	{
		Debug.Log("FLASH");
		spriteRenderer.material = flashMaterial;
		yield return new WaitForSeconds(duration);
		spriteRenderer.material = baseMaterial;
		flashCoroutine = null;
	}
}
