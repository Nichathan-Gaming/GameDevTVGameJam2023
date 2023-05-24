using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 0 : Skakoan,
/// 1 : Grund,
/// 2 : Hel,
/// 3 : Sandr,
/// 4 : MainMenu
/// </summary>
public enum MapRegion
{
    Skakoan,
    Grund,
    Hel,
    Sandr,
    MainMenu
}

public enum SoundEffects
{

}

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    [Header("Music")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip[] musicClips;

    [Header("SFX")]
    [SerializeField] AudioSource[] sfxAudioSources;

    [Header("UI Manager")]
    [SerializeField] Image musicImage;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;
    [SerializeField] Image sfxImage;
    [SerializeField] Sprite sfxOn;
    [SerializeField] Sprite sfxOff;

    const string MUSIC_PREFS = "AudioManager_MUSIC_PREFS";
    const string SFX_PREFS = "AudioManager_SFX_PREFS";

    static float musicVolume;
    float _sfxVolume;
    internal static float sfxVolume
    {
        get
        {
            return instance._sfxVolume;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat(MUSIC_PREFS, 1);
        SetMusic();
        _sfxVolume = PlayerPrefs.GetFloat(SFX_PREFS, 1);
        SetSFX();
    }

    /// <summary>
    /// Sets the music UI and volume
    /// </summary>
    void SetMusic()
    {
        musicImage.sprite = musicVolume>0?musicOn:musicOff;
        musicAudioSource.volume = musicVolume;
    }

    /// <summary>
    /// Sets the SFX UI
    /// </summary>
    void SetSFX()
    {
        sfxImage.sprite = _sfxVolume > 0 ? sfxOn : sfxOff;
    }

    /// <summary>
    /// Plays a given sound effect.
    /// -Reserved for global and player sounds
    /// </summary>
    /// <param name="soundEffects"></param>
    internal static void PlaySFX(SoundEffects soundEffects)
    {
        instance.sfxAudioSources[(int)soundEffects].volume = instance._sfxVolume;
        instance.sfxAudioSources[(int)soundEffects].Play();
    }

    /// <summary>
    /// Sets the musicAudioSource.clip to the clip in the spot of the map region
    /// </summary>
    /// <param name="mapRegion">
    /// Skakoan,
    /// Grund,
    /// Hel,
    /// Sandr,
    /// MainMenu
    /// </param>
    internal static void ChangeRegion(MapRegion mapRegion)
    {
        instance.musicAudioSource.clip = instance.musicClips[(int) mapRegion];
    }

    /// <summary>
    /// If musicVolume is greater than 0, set it to 0, otherwise, 1
    /// 
    /// then update UI
    /// </summary>
    public void ToggleMusic()
    {
        musicVolume = musicVolume > 0 ? 0 : 1;
        PlayerPrefs.SetFloat(MUSIC_PREFS, musicVolume);
        SetMusic();
    }

    /// <summary>
    /// Sets the music volume then updates the prefs and UI
    /// </summary>
    /// <param name="volume">0-1</param>
    public void SetMusicVolume(float volume)
    {
        if (volume < 0 || volume > 1) throw new System.ArgumentOutOfRangeException($"Expected a value from 0 to 1 but received {volume}.");

        musicVolume = volume;
        PlayerPrefs.SetFloat(MUSIC_PREFS, musicVolume);
        SetMusic();
    }

    /// <summary>
    /// If sfxVolume is greater than 0, set it to 0, otherwise, 1
    /// 
    /// then update UI
    /// </summary>
    public void ToggleSFX()
    {
        _sfxVolume = _sfxVolume > 0 ? 0 : 1;
        PlayerPrefs.SetFloat(SFX_PREFS, _sfxVolume);
        SetSFX();
    }

    /// <summary>
    /// Sets the music volume then updates the prefs and UI
    /// </summary>
    /// <param name="volume">0-1</param>
    public void SetSFXVolume(float volume)
    {
        if (volume < 0 || volume > 1) throw new System.ArgumentOutOfRangeException($"Expected a value from 0 to 1 but received {volume}.");

        _sfxVolume = volume;
        PlayerPrefs.SetFloat(SFX_PREFS, _sfxVolume);
        SetSFX();
    }
}
