using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Threading.Tasks;

public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public class MusicAudio
    {
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    #region Singleton
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindGameObjectsWithTag("Music")[0].GetComponent<MusicManager>();
            }
            return _instance;

        }
    }
    #endregion

    public static float musicDefaultValue = 0.5f;

    public MusicAudio menuMusic;
    public MusicAudio gameMusic;

    public MusicAudio winMusic;
    public MusicAudio looseMusic;

    private AudioSource musicSource;

    void Awake()
    {
        HandleSceneMusic(SceneManager.GetActiveScene().name);
    }

    public void HandleSceneMusic(String sceneName)
    {
        Debug.Log("HandleSceneMusic for " + sceneName);

        if (sceneName.Equals("MenuScene"))
        {
            PlayMenuSound();
        }
        else
        {
            PlayGameSound();
        }
    }
    public void FadeOutMusic()
    {
        StartCoroutine(FadeOut(musicSource));
    }

    public IEnumerator FadeOut(AudioSource music)
    {
        Debug.Log("FadeOut " + music.gameObject.name);
        var originaVolume = music.volume;
        while(music.volume > 0)
        {
            music.volume -= originaVolume / 20;
            yield return new WaitForSeconds(0.9f / 20f);
        }
        Destroy(music.gameObject);
    }

    public void PlayMenuSound()
    {
        var go = new GameObject("MENU_SOUND");
        go.transform.parent = transform;
        musicSource = go.AddComponent<AudioSource>();

        musicSource.volume = menuMusic.volume * PlayerPrefs.GetFloat("music", musicDefaultValue);
        musicSource.clip = menuMusic.clip;
        musicSource.loop = true;
        musicSource.Play();
        //Destroy(go, Instance.intro.clip.length);
    }
    public void PlayGameSound()
    {
        var go = new GameObject("GAME_SOUND");
        go.transform.parent = transform;
        musicSource = go.AddComponent<AudioSource>();

        musicSource.volume = gameMusic.volume * PlayerPrefs.GetFloat("music", musicDefaultValue);
        musicSource.clip = gameMusic.clip;
        musicSource.loop = true;
        musicSource.Play();
        //Destroy(go, Instance.intro.clip.length);
    }

    public void PlayWinSound()
    {
        var go = new GameObject("WIN_SOUND");
        go.transform.parent = transform;
        musicSource = go.AddComponent<AudioSource>();

        musicSource.volume = gameMusic.volume * PlayerPrefs.GetFloat("music", musicDefaultValue);
        musicSource.clip = winMusic.clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayLooseSound()
    {
        var go = new GameObject("LOOSE_SOUND");
        go.transform.parent = transform;
        musicSource = go.AddComponent<AudioSource>();
        musicSource.volume = gameMusic.volume * PlayerPrefs.GetFloat("music", musicDefaultValue);
        musicSource.clip = looseMusic.clip;
        musicSource.loop = true;
        musicSource.Play();

    }
    public static void SetMusicVolume(float value)
    {
        if (Instance.musicSource != null)
        {
            Instance.musicSource.volume = MusicManager.Instance.gameMusic.volume * value;
        }
    }
}
