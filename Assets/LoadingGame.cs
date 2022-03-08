using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingGame : MonoBehaviour
{
    private static bool firstStart = true;
    public GameObject blackBackgouund;
    void Start()
    {
        blackBackgouund.SetActive(false);
        StartCoroutine(LateFadeOut());
    }

    IEnumerator LateFadeOut()
    {
        float fadeStatus;
        if (firstStart)
        {
            blackBackgouund.SetActive(true);
            fadeStatus = 0f;
            while (fadeStatus < 1f)
            {
                fadeStatus += 1f / (0.3f / 0.01f);
                foreach (var img in GetComponentsInChildren<Image>())
                {
                    img.color = new Color(img.color.r, img.color.g, img.color.b, fadeStatus);
                }
                foreach (var t in GetComponentsInChildren<TMP_Text>())
                {
                    t.color = new Color(t.color.r, t.color.g, t.color.b, fadeStatus);
                }

                yield return new WaitForSeconds(0.01f);
            }
            blackBackgouund.SetActive(false);

            firstStart = false;
        }
        yield return new WaitForSeconds(2f);
        fadeStatus = 1f;
        while (fadeStatus > 0f)
        {
            fadeStatus -= 1f / (0.3f / 0.01f);
            foreach (var img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, fadeStatus);
            }
            foreach (var t in GetComponentsInChildren<TMP_Text>())
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, fadeStatus);
            }
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
