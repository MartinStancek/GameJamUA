using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    public Image screen1;
    public Image screen2;
    Coroutine cor;
    int screen = 0;
    // Start is called before the first frame update
    void Start()
    {
        cor = StartCoroutine(Screen1());
    }

    void Update()
    {
        if (Input.anyKeyDown && cor != null) 
        {
            Debug.Log("key");
            StopCoroutine(cor);
            if (screen == 1)
            {
                cor = StartCoroutine(Screen2());
            }
            else if (screen == 2) 
            {
                cor = null;
                StartCoroutine(LoadGamePlay());

            }
        }
    }
    IEnumerator Screen1()
    {
        screen = 1;
        screen1.color = new Color(screen1.color.r, screen1.color.g, screen1.color.b, 1f);
        screen2.color = new Color(screen2.color.r, screen2.color.g, screen2.color.b, 0f);
        yield return new WaitForSeconds(5.5f);
        cor = StartCoroutine(Screen2());


    }

    IEnumerator Screen2()
    {
        screen = 2;
        var fadeStatus = 0f;
        while (fadeStatus < 1f)
        {
            fadeStatus += 1f / (0.3f / 0.01f);
            screen1.color = new Color(screen1.color.r, screen1.color.g, screen1.color.b, 1f - fadeStatus);
            screen2.color = new Color(screen2.color.r, screen2.color.g, screen2.color.b, fadeStatus);
            foreach (var t in screen1.GetComponentsInChildren<TMP_Text>())
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, 1f - fadeStatus);
            }
            foreach (var t in screen2.GetComponentsInChildren<TMP_Text>())
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, fadeStatus);
            }


            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(5.5f);
        cor = StartCoroutine(LoadGamePlay());
        MusicManager.Instance.FadeOutMusic();
    }

    IEnumerator LoadGamePlay()
    {
        screen = 3;
        var fadeStatus = 1f;
        while (fadeStatus > 0f)
        {
            fadeStatus -= 1f / (0.3f / 0.01f);
            var i = screen2.GetComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, fadeStatus);
            foreach (var t in screen2.GetComponentsInChildren<TMP_Text>())
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, fadeStatus);
            }
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadSceneAsync(1);
    }



}
