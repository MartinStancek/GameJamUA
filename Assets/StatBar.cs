using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Color filledColor;
    public Color missingColor;

    public GameObject barPart;

    public void Init(int maxValue, int actualValue)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (var i = 0; i < maxValue; i++)
        {
            var go = Instantiate(barPart, transform);
            go.GetComponentInChildren<Image>().color = i < actualValue ? filledColor : missingColor;
        }
    }

    public void SetValue(int value)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var go = transform.GetChild(i);
            go.GetComponentInChildren<Image>().color = i < value ? filledColor : missingColor;
        }
    }
}
