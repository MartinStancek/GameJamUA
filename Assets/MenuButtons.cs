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

    private enum Language
    {
        SK,EN,UA
    }
    private Language currentLanguage;

    public void Quit()
    {
        Application.Quit();
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
                languageImage.sprite = languageUaSprite;
                currentLanguage = Language.UA;
                LoadLocale("uk");
                break;
            case Language.UA:
                languageImage.sprite = languageSkSprite;
                currentLanguage = Language.SK;
                LoadLocale("sk");
                break;

        }
    }
    public void ToggleMusic()
    {
        if (musicImage.sprite.Equals(musicOnSprite))
        {
            musicImage.sprite = musicOffSprite;
            MusicManager.SetMusicVolume(0f);
        }
        else
        {
            musicImage.sprite = musicOnSprite; ;
            MusicManager.SetMusicVolume(1f);

        }
    }

    public void RestartRound()
    {
        SceneManager.LoadScene(0);
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
