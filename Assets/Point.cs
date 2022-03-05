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

    public void PlayerArrived()
    {
        foreach(Transform t in transform)
        {
            switch(t.name)
            {
                case "Obstacle":
                    Player.Instance.energy -= 1;
                    break;
                case "Shelter":
                    Player.Instance.energy += 1;
                    break;
                case "Fight":
                    Player.Instance.health -= 1;
                    break;
                case "Hospital":
                    Player.Instance.health += 1;
                    break;
                case "Bandits":
                    Player.Instance.supplies -= 1;
                    break;
                case "Supplies":
                    Player.Instance.supplies += 1;
                    break;
            }
        }
    }
}
