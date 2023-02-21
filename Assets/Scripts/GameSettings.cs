using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static float ScreenWidth { get; private set; }
	public static float ScreenHeight { get; private set; }
	public static float AspectRatio { get; private set; }
	public static float GameScale { get; private set; }

	public static bool UseMusic { get; private set; } = true;
	public static bool IsPaused { get; set; }
	
	[SerializeField] bool useMusicSetting;


	private void Awake()
	{
		// Set Screenheight from reading of ortoghonal camera
		ScreenHeight = Camera.main.orthographicSize*2;
		ScreenWidth = ScreenHeight * ((float)Screen.width/ (float)Screen.height);
		AspectRatio = ScreenWidth/ScreenHeight;
		GameScale = ScreenHeight/ Screen.height;		
	}

	private void Update()
	{
		if (UseMusic != useMusicSetting)
		{
			UseMusic = useMusicSetting;
			FindObjectOfType<SoundController>().UseMusic(UseMusic);
		}
	}


}
