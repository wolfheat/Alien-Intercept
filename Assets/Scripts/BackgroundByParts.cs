using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundByParts : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgroundPartsSprites;
    private List<GameObject> backgroundParts = new	List<GameObject>();
    private const float BackgroundPartScale = 4.7f;
    private const float BackgroundPartHeight = 6.4f;
    private float totalHeigh = 0;
	private float ScrollSpeed = 5f;

	private void OnEnable()
    {
        GenerateObjectsFromSprites();
        SetBackgroundPositions();
	}
	
	public void Scroll()
	{
		transform.position += Vector3.down * ScrollSpeed * Time.deltaTime;
		CheckForEndOfScroll();
	}


	private void CheckForEndOfScroll()
	{
		if (transform.position.y < -BackgroundPartHeight*BackgroundPartScale)
		{
			transform.position += Vector3.up * BackgroundPartHeight * BackgroundPartScale;
			MoveBottomPartToTop();
		}
	}




	private void SetBackgroundPositions()
	{
		for (int i = 0; i < backgroundParts.Count; i++)
		{
			backgroundParts[i].transform.localPosition = new Vector2(0, BackgroundPartHeight / 2 * BackgroundPartScale + BackgroundPartHeight * i * BackgroundPartScale);
			backgroundParts[i].transform.parent = transform;
			totalHeigh += BackgroundPartHeight;
		}
	}

	private void GenerateObjectsFromSprites()
	{
		for (int i = 0; i < backgroundPartsSprites.Count; i++)
		{
			GameObject newPart = new GameObject("part" + i);
			backgroundParts.Add(newPart);
			newPart.transform.localScale = new Vector3(BackgroundPartScale,BackgroundPartScale,1); 
			SpriteRenderer newSpriteRenderer = newPart.AddComponent<SpriteRenderer>();
			newSpriteRenderer.sprite = backgroundPartsSprites[i];
			newSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Background");
		}
	}

	public void MoveBottomPartToTop()
	{
		if (backgroundParts.Count < 2) return;
		GameObject firstItem = backgroundParts[0];
		backgroundParts.RemoveAt(0);
		backgroundParts.Add(firstItem);
		SetBackgroundPositions();
	}

}
