using System.Collections;
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

	private void OnEnable()
	{
		Debug.Log("basematerial set "+baseMaterial);
		spriteRenderer.material = baseMaterial;
		flashCoroutine = null;
	}
	public void DoSimpleFlash()
	{
		if (flashCoroutine == null)
			flashCoroutine = StartCoroutine(SimpleFlashCO());
	}

	private IEnumerator SimpleFlashCO()
	{
		spriteRenderer.material = flashMaterial;
		yield return new WaitForSeconds(duration);
		spriteRenderer.material = baseMaterial;
		flashCoroutine = null;
	}
}
