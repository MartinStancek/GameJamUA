using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingGame : MonoBehaviour
{
    
    private float fadeStatus = 1f;
    void Start()
    {
        StartCoroutine(LateFadeOut());
    }

    IEnumerator LateFadeOut()
    {
        yield return new WaitForSeconds(2f);

        while(fadeStatus > 0f)
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
