using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messages : MonoBehaviour
{
    #region Singleton
    private static Messages _instance;
    public static Messages Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<Messages>();
            }
            return _instance;

        }
    }
    #endregion

    public Transform zapcha;
    public Transform ukryt;
    public Transform okradnutie;
    public Transform zasoby;
    public Transform fight0;
    public Transform fightn;
    public Transform fightDead;
    public Transform nemocnica;
    public Transform npcHealthGain;
    public Transform npcHealthReduce;
    public Transform npcSuppliesGain;
    public Transform npcSuppliesReduce;
    // Start is called before the first frame update
    void Start()
    {
        SetPanel(null, null);
    }

    public void SetPanel(Transform panel, string amount)
    {
        zapcha.gameObject.SetActive(false);
        ukryt.gameObject.SetActive(false);
        okradnutie.gameObject.SetActive(false);
        zasoby.gameObject.SetActive(false);
        fight0.gameObject.SetActive(false);
        fightn.gameObject.SetActive(false);
        fightDead.gameObject.SetActive(false);
        nemocnica.gameObject.SetActive(false);
        npcHealthGain.gameObject.SetActive(false);
        npcHealthReduce.gameObject.SetActive(false);
        npcSuppliesGain.gameObject.SetActive(false);
        npcSuppliesReduce.gameObject.SetActive(false);
        if (panel)
        {
            panel.gameObject.SetActive(true);
            panel.Find("Amount").GetComponent<TMP_Text>().text = amount;
        }
    }
}
