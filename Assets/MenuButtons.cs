using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MenuButtons : MonoBehaviour
{

    public Image musicImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Image languageImage;
    public Sprite languageSkSprite;
    public Sprite languageEnSprite;
    public Sprite languageUaSprite;

    private static Language currentLanguage;
    private static bool music = true;

    private enum Language
    {
        SK,EN,UA
    }

    private void Start()
    {
        switch (currentLanguage)
        {
            case Language.SK:
                languageImage.sprite = languageSkSprite;
                LoadLocale("sk");
                break;
            case Language.EN:
                languageImage.sprite = languageEnSprite;
                currentLanguage = Language.SK;
                LoadLocale("en");
                break;
        }

        MusicManager.SetMusicVolume(music ? 1f : 0f);
        musicImage.sprite = music ? musicOnSprite : musicOffSprite;

    }


    public void Quit()
    {
#if !PLATFORM_WEBGL

        Application.Quit();
#endif

#if PLATFORM_WEBGL

        Screen.fullScreen = false;
#endif


    }
    public void ToggleLanguage()
    {
        switch (currentLanguage)
        {
            case Language.SK:
                languageImage.sprite = languageEnSprite;
                currentLanguage = Language.EN;
                LoadLocale("en");
                break;
            case Language.EN:
                languageImage.sprite = languageSkSprite;
                currentLanguage = Language.SK;
                LoadLocale("sk");
                break;

        }
    }
    public void ToggleMusic()
    {
        if (music)
        {
            musicImage.sprite = musicOffSprite;
            MusicManager.SetMusicVolume(0f);
        }
        else
        {
            musicImage.sprite = musicOnSprite;
            MusicManager.SetMusicVolume(1f);
        }
        music = !music;
    }

    public void RestartRound()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLocale(string languageIdentifier)
    {
        LocalizationSettings settings = LocalizationSettings.Instance;
        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);//can be "en" "de" "ja" etc.
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale aLocale = LocalizationSettings.AvailableLocales.Locales[i];
            LocaleIdentifier anIdentifier = aLocale.Identifier;
            if (anIdentifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = aLocale;
            }
        }
    }
}
