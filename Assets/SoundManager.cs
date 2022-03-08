using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }
            return _instance;

        }
    }
    #endregion

    public static float soundDefaultValue = 1f;

    public MusicManager.MusicAudio enemyClip;
    public MusicManager.MusicAudio emptyClip;
    public MusicManager.MusicAudio supportClip;

    public void PlayClip(MusicManager.MusicAudio audio)
    {
        var go = new GameObject("MENU_SOUND");
        go.transform.parent = transform;
        var musicSource = go.AddComponent<AudioSource>();

        musicSource.volume = audio.volume * PlayerPrefs.GetFloat("sound", soundDefaultValue);
        musicSource.clip = audio.clip;
        musicSource.Play();
        Destroy(go, audio.clip.length);
    }
}
