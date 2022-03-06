using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Color filledColor;
    public Color missingColor;

    public GameObject barPart;

    public void SetValue(int value)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
        transform.Find("" + value).gameObject.SetActive(true);

    }
}
