using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;

        }
    }
    #endregion

    public GameObject WinPanel;
    public GameObject LoosePanel;

    private float fadeStatus;

    private void Start()
    {
        WinPanel.SetActive(false);
        LoosePanel.SetActive(false);
    }

    public void WinGame()
    {
        StartCoroutine(WinGameLate());

    }
    IEnumerator WinGameLate()
    {
        fadeStatus = 0;

        yield return new WaitForSeconds(1f);
        WinPanel.SetActive(true);
        while (fadeStatus < 1f)
        {
            fadeStatus += 1f / (0.3f / 0.01f);
            var i = WinPanel.transform.Find("Image").GetComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, fadeStatus);
            yield return new WaitForSeconds(0.01f);
        }

    }


    public void LooseGame()
    {
        StartCoroutine(LooseLate());
    }

    IEnumerator LooseLate()
    {
        fadeStatus = 0;

        yield return new WaitForSeconds(1f);
        LoosePanel.SetActive(true);
        while (fadeStatus < 1f)
        {
            fadeStatus += 1f / (0.3f / 0.01f);
            var i = LoosePanel.transform.Find("Image").GetComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, fadeStatus);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
