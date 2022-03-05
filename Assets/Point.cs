using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Point : MonoBehaviour
{

    public List<Point> connectedTo;

    public GUIStyle sceneLabelStyle;

    public GameObject obstacklePrefab;
    public GameObject banditsPrefab;
    public GameObject fightPrefab;
    public GameObject randomNPCPrefab;


    private void OnMouseUp()
    {
        Player.Instance.MoveTo(this);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, GetId().ToString(), sceneLabelStyle);
    }
# endif

    public int GetId()
    {
        Match m = Regex.Match(gameObject.name, @"\w+ \((\d+)\)", RegexOptions.IgnoreCase);
        return System.Int32.Parse(m.Groups[1].Value);
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
                    Player.Instance.energy += 2;
                    break;
                case "Fight":
                    Player.Instance.health -= 1;
                    break;
                case "Hospital":
                    Player.Instance.health += 2;
                    break;
                case "Bandits":
                    Player.Instance.supplies -= 1;
                    break;
                case "Supplies":
                    Player.Instance.supplies += 2;
                    break;
            }
        }
    }

    public void AfterPlayerTrun()
    {
        var staticEntities = new List<string>() { "Shelter", "Hospital", "Supplies" };
        if(transform.childCount > 0 && staticEntities.Contains(transform.GetChild(0).name))
        {
            return;
        }

        if (Player.Instance.actualPoint.Equals(this))
        {
            return;
        }

        var specialBlock = Random.Range(0, 100) < 50;
        if (specialBlock && transform.childCount > 0)//zostava nezmeneny;
        {
            return;
        }
        else if (specialBlock && transform.childCount == 0)//pridava sa ;
        {
            var specialType = Random.Range(0, 100);
            if (specialType < 35) //prekazka
            {
                var go = Instantiate(obstacklePrefab, transform);
                go.name = obstacklePrefab.name;
            }
            else if (specialType < 55)  //bandits
            {
                var go = Instantiate(banditsPrefab, transform);
                go.name = banditsPrefab.name;
            }
            else if (specialType < 80)  //Fight
            {
                var go = Instantiate(fightPrefab, transform);
                go.name = fightPrefab.name;

            }
            else // Random NPC
            {
                Debug.Log("TODO: Random NPC");
                /*;;
                var go = Instantiate(randomNPCPrefab, transform);
                go.name = randomNPCPrefab.name;*/

            }
        }
        else if (!specialBlock && transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);

        }
    }
}
