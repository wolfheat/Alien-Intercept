using System;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    [SerializeField] PlayerGhostController ghost;

	private void Start()
    {
        Inputs.Instance.Controls.MainActionMap.Space.performed += _ => SpawnNewGhost();
	}

    private void SpawnNewGhost()
    {
        Debug.Log("Spawn");
        ghost.gameObject.SetActive(true);
        ghost.transform.position = transform.position;
        StartCoroutine(ghost.RemoveGhostAfter(2f));
    }

}