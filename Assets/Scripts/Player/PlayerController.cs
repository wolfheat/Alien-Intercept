using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] List<GameObject> bulletCreationPositions;
    

    private float bulletCreationTime = 0.1f;

    private const float Scale = 8f/350;
    
    private void OnEnable()
    {
        StartCoroutine(AutoBulletSpawner(true));
    }

    private IEnumerator AutoBulletSpawner(bool continious)
    {
        while (continious)
        {
            foreach(GameObject pos in bulletCreationPositions)
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
        Vector3 worldPosition = Mouse.current.position.ReadValue();
        Vector3 worldPositionClamped = new Vector2(Scale*Mathf.Clamp(worldPosition.x,5,390f), Scale * Mathf.Clamp(worldPosition.y, 20, 600f));
        
        //Camera.main.ScreenToWorldPoint();
        transform.position = worldPositionClamped;   

    }
}
