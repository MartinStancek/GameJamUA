using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
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

    public RectTransform content;
    public GameObject msgPrefab;

    public readonly string obstickle = "obstacklePopup";
    public readonly string shelter = "shelterPopup";
    public readonly string uplatok = "stealPopup";
    public readonly string sellStuff = "suppliesPopup";
    public readonly string fight0 = "fight0Popup";
    public readonly string fightn = "fightnPopup";
    public readonly string fightKill = "fightDeadPopup";
    public readonly string hospital = "hospitalPopup";
    public readonly string npcMoneyIncome = "npcGainSupplies";
    public readonly string npcMonyeDonate = "npcDonateSupplies";
    public readonly string npcHealthUp = "npcHealed";
    public readonly string npcHealthDown = "npcDamaged";

    public Sprite healthIcon;
    public Sprite moneyIcon;
    public Sprite energyIcon;

    public void AddMessage(string msgCode, string amount, Sprite icon)
    {
        var msgGo = Instantiate(msgPrefab, content);
        msgGo.transform.Find("Amount").GetComponent<TMP_Text>().text = amount;
        var loc = msgGo.transform.Find("Msg").GetComponent<LocalizeStringEvent>();
        loc.StringReference.TableReference = "Texts";
        loc.StringReference.TableEntryReference = msgCode;
        msgGo.transform.Find("Icon").GetComponent<Image>().sprite = icon;

        var msgT = msgGo.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, msgT.sizeDelta.y * content.childCount);
        GetComponent<ScrollRect>().velocity = Vector2.up * 300;
    }

}
