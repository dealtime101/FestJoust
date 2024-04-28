public Slider musicVolumeSlider = musicSlider;
public float musicVolumeNumber = volume;

public void Start()
{
    // remember volume level from last time
    GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
}


public void Update()
{
    volum = musicSlider.GetComponent<Slider>().value;
    musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);

}

public void MusicSlider()
{
    PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
}

public void UpdateVolume()
{
    GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
}
