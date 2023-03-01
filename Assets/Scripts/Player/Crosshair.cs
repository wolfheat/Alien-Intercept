using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
	private Vector2 XLimits = new Vector2(0.05f, .95f);
	private Vector2 YLimits = new Vector2(0.05f, .9f);

	private void Update()
	{
		SetPosition();
	}
	public void SetPosition()
	{
		// Get Mouse Position
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		Vector3 worldPositionClamped = new Vector2(
			Mathf.Clamp(worldPosition.x, GameSettings.ScreenWidth * XLimits.x, GameSettings.ScreenWidth * XLimits.y),
			Mathf.Clamp(worldPosition.y, GameSettings.ScreenHeight * YLimits.x, GameSettings.ScreenHeight * YLimits.y)
			);
		transform.position = worldPositionClamped;

	}
}
