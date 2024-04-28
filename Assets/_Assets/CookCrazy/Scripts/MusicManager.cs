using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume = .3f;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
    }

    public void Update()
    {
        volume = musicSlider.GetComponent<Slider>().value;
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
    }
    public void ChangeVolume()
    {

        audioSource.volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
    }

    public void MusicSlider()
    {
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
