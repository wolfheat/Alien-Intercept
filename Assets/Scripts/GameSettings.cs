using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static float ScreenHeight { get; private set; }

	private void Start()
	{
		ScreenHeight = Camera.main.orthographicSize*2;
	}

}
