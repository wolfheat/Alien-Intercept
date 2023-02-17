using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static float ScreenHeight { get; private set; }

	public static bool UseMusic { get; private set; } = true;

	private void Start()
	{	
		// Set Screenheight from reading of ortoghonal camera
		ScreenHeight = Camera.main.orthographicSize*2;
	}

}
