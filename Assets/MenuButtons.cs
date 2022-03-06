using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
                break;
            case Language.EN:
                languageImage.sprite = languageUaSprite;
                currentLanguage = Language.UA;
                break;
            case Language.UA:
                languageImage.sprite = languageSkSprite;
                currentLanguage = Language.SK;
                break;

        }
    }
    public void ToggleMusic()
    {
        if (musicImage.sprite.Equals(musicOnSprite))
        {
            musicImage.sprite = musicOffSprite;
        }
        else
        {
            musicImage.sprite = musicOnSprite; ;
        }
    }

    public void RestartRound()
    {
        SceneManager.LoadScene(0);
    }
}
