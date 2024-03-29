using UnityEngine;

public enum MusicType{Menu,Normal,Boss}

public enum SFX { ShipDestroyedA, GetHit, PlayerDeath, MenuStep, MenuSelect, MenuError, FireRocket, FireBullet, StarPickup}

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] menu;
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioClip[] pickup;
    [SerializeField] private AudioClip[] music;

    private AudioSource musicSource;
    private AudioSource musicSourceIntense;
    private AudioSource sfxSource;
    private bool doPlayMusic = true;
    private bool doPlaySFX=true;
    private bool doingFadeout = false;

    public MusicType activeMusic = MusicType.Menu;
    public static SoundController Instance { get; set; }

	private float presetVolume = 0.8f;
    //private float presetSFXVolume = 0.1f;
    private float presetSFXStepVolume = 0.5f;

    private float totalFadeOutTime = 3.5f;
    private float fadeOutMargin = 0.01f;
    private float currentFadeOutTime;

    private void Awake()
    {
        if (Instance != null) { Destroy(this.gameObject); return;}
        else Instance = this;

        GameObject musicSourceHolder = new GameObject("Music");
        GameObject sfxSourceHolder = new GameObject("SFX");
        musicSourceHolder.transform.SetParent(this.transform);
		//musicSourceHolder.name = "Music";
        sfxSourceHolder.transform.SetParent(this.transform);
        //sfxSourceHolder.name = "SFX";

        musicSourceIntense = gameObject.AddComponent<AudioSource>();
        musicSource = musicSourceHolder.AddComponent<AudioSource>();
        sfxSource = sfxSourceHolder.AddComponent<AudioSource>();
    }
    private void Start()
    {
        musicSourceIntense.loop = true;
        musicSourceIntense.volume = 0.5f;
        musicSource.loop = true;
        musicSource.volume = 0.2f;
        sfxSource.volume = presetSFXStepVolume;

        doPlayMusic = GameSettings.UseMusic;

        PlayMusic();

        Inputs.Instance.Controls.MainActionMap.MusicToggle.performed += _ => MuteToggle();// = _.ReadValue<float>();
	}

    public void SetMusicType(MusicType t)
    {
        activeMusic = t;
        DoMusicSetting();
    }
    public void UseMusic(bool use)
    {
        doPlayMusic = use;
        DoMusicSetting();
	}
    private void MuteToggle()
    {
		doPlayMusic = !doPlayMusic;
        DoMusicSetting();
	}
    private void DoMusicSetting()
    {
		if (doPlayMusic) PlayMusic();
		else
		{
			musicSource.Stop();
		}
    }

	private void Update()
    {
        if(doingFadeout) DoFadeout();
    }

	private void DoFadeout()
	{
        currentFadeOutTime += Time.deltaTime;

        float newVolume = presetVolume*(1 - currentFadeOutTime / totalFadeOutTime);
        musicSourceIntense.volume = newVolume;    
        if (currentFadeOutTime + fadeOutMargin >= totalFadeOutTime)
        {
            //Fadeout complete
            musicSourceIntense.volume = presetVolume;
            musicSourceIntense.Stop();
            doingFadeout = false;
        }
    }

	public void SetVolume(float vol)
	{
        musicSource.volume = vol;
        musicSourceIntense.volume = vol;
	}
	public void SetSFXVolume(float vol)
	{
        sfxSource.volume = vol;
        sfxSource.volume = presetSFXStepVolume;
	}
	private void PlayMusic()
	{
        if (doPlayMusic)
        {
            if ((int)activeMusic >= music.Length) { Debug.LogError("To few Music files assigned to SoundController"); return;}
            musicSource.clip = music[(int)activeMusic];
            musicSource.Play();
        }
        else musicSource.Stop(); 
	}

    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void PlaySFX(SFX type, bool playMulti=true)
	{

        // If not able to play multiple sounds exit if already playing
        if (!playMulti) if (sfxSource.isPlaying) return;

        if (!doPlaySFX) return;


        switch (type)
		{
            case SFX.ShipDestroyedA:
                sfxSource.PlayOneShot(sfx[0]);
                break;
			case SFX.FireRocket:
                sfxSource.PlayOneShot(sfx[1]);
                break;
			case SFX.MenuStep:
                sfxSource.PlayOneShot(menu[0]);
                break;
			case SFX.MenuSelect:
                sfxSource.PlayOneShot(menu[1]);
                break;
			case SFX.MenuError:
                sfxSource.PlayOneShot(menu[2]);
                break;
			case SFX.StarPickup:
                sfxSource.PlayOneShot(pickup[0]);
                break;
			default:
				break;
		}
	}


}
