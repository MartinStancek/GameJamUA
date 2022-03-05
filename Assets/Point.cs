using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEditor;

public class Point : MonoBehaviour
{

    public List<Point> connectedTo;

    public GUIStyle sceneLabelStyle;

    private void OnMouseUp()
    {
        Player.Instance.MoveTo(this);
    }

    private void OnDrawGizmos()
    {


        Handles.Label(transform.position, GetId().ToString(), sceneLabelStyle);
    }

    public int GetId()
    {
        Match m = Regex.Match(gameObject.name, @"\w+ \((\d+)\)", RegexOptions.IgnoreCase);
        return Int32.Parse(m.Groups[1].Value);
    }
}
