using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject screen1;
    public GameObject screen2;

    // Start is called before the first frame update
    void Start()
    {
        screen1.SetActive(true);
        screen2.SetActive(false);
        StartCoroutine(Screen1());
    }
    IEnumerator Screen1()
    {
        yield return new WaitForSeconds(5f);
        screen1.SetActive(false);
        screen2.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(0);
    }
}
