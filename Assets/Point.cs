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

    private Vector3 origScale;

    private void Start()
    {
        transform.Find("img").SetAsLastSibling();
        origScale = transform.Find("img").localScale;
    }

    private void OnMouseUp()
    {
        Player.Instance.MoveTo(this);
    }
    private void OnMouseEnter()
    {
        if(!Player.Instance.actualPoint.Equals(this) && Player.Instance.actualPoint.connectedTo.Contains(this))
        HighLight();
    }
    private void OnMouseExit()
    {
        UnhighLight();
    }    
    
    public void HighLight()
    {
        transform.Find("img").localScale *= 1.3f;
    }
    public void UnhighLight()
    {
        transform.Find("img").localScale = origScale;

    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, GetId().ToString(), sceneLabelStyle);
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
    }
# endif

    public int GetId()
    {
        Match m = Regex.Match(gameObject.name, @"\w+ \((\d+)\)", RegexOptions.IgnoreCase);
        return System.Int32.Parse(m.Groups[1].Value);
    }

    public void PlayerArrived()
    {
        if(transform.childCount == 1)
        {
            return;
        }
        int amount;
        var t = transform.GetChild(0);
        switch (t.name)
        {
            case "Obstacle":
                amount = Random.Range(0, 100) < 60 ? -1 : -2;
                Messages.Instance.AddMessage(Messages.Instance.obstickle, "" + amount, Messages.Instance.energyIcon);
                Player.Instance.energy += amount;
                break;
            case "Shelter":
                Messages.Instance.AddMessage(Messages.Instance.shelter, "+2", Messages.Instance.energyIcon);
                Player.Instance.energy += 2;
                break;
            case "Fight":
                amount = Random.Range(0, 3);
                switch (amount)
                {
                    case 0:
                        Messages.Instance.AddMessage(Messages.Instance.fight0, "-" + amount, Messages.Instance.healthIcon);
                        break;
                    case 1:
                    case 2:
                        Messages.Instance.AddMessage(Messages.Instance.fightn, "-" + amount, Messages.Instance.healthIcon);
                        Player.Instance.health -= amount;
                        break;
                    case 3:
                        Messages.Instance.AddMessage(Messages.Instance.fightKill, "-" + amount, Messages.Instance.healthIcon);
                        Player.Instance.health -= amount;
                        break;

                }
                break;
            case "Hospital":
                Messages.Instance.AddMessage(Messages.Instance.hospital, "+2", Messages.Instance.healthIcon);
                Player.Instance.health += 2;
                break;
            case "Bandits":
                amount = Random.Range(0, 100) < 60 ? -1 : -2;
                Messages.Instance.AddMessage(Messages.Instance.uplatok, "" + amount, Messages.Instance.moneyIcon);
                Player.Instance.supplies += amount;
                break;
            case "Supplies":
                Messages.Instance.AddMessage(Messages.Instance.sellStuff, "+2", Messages.Instance.moneyIcon);
                Player.Instance.supplies += 2;
                break;
            case "NPC":
                var type = Random.Range(0, 2);
                var direction = Random.Range(0, 100) < 60 ? +1 : -1;
                if (type == 0)
                {
                    Messages.Instance.AddMessage(direction > 0 ? Messages.Instance.npcHealthUp : Messages.Instance.npcHealthDown, "" + (direction > 0 ? "+" + direction : direction), Messages.Instance.healthIcon);

                    Player.Instance.health += direction;
                }
                else
                {
                    Messages.Instance.AddMessage(direction > 0 ? Messages.Instance.npcMoneyIncome : Messages.Instance.npcMonyeDonate, "" + (direction > 0 ? "+" + direction : direction), Messages.Instance.moneyIcon);
                    Player.Instance.supplies += direction;
                }
                break;
        }
        LeanTween.scale(t.gameObject, Vector3.zero, 0.3f);
        Destroy(t.gameObject, 0.3f);
    }

    public void AfterPlayerTrun()
    {
        var staticEntities = new List<string>() { "Shelter", "Hospital", "Supplies" };
        if (transform.childCount > 1 && staticEntities.Contains(transform.GetChild(0).name))
        {
            return;
        }

        if (Player.Instance.startPoint.Equals(this) || Player.Instance.endPoint.Equals(this))
        {
            return;
        }

        if (Player.Instance.actualPoint.Equals(this))
        {
            return;
        }

        var specialBlock = Random.Range(0, 100) < 50;
        if (specialBlock && transform.childCount > 1)//zostava nezmeneny;
        {
            return;
        }
        else if (specialBlock && transform.childCount == 1)//pridava sa ;
        {
            GameObject go;
            var specialType = Random.Range(0, 100);
            if (specialType < 25) //prekazka
            {
                go = Instantiate(obstacklePrefab, transform);
                go.transform.SetAsFirstSibling();
                go.name = obstacklePrefab.name;
            }
            else if (specialType < 50)  //bandits
            {
                go = Instantiate(banditsPrefab, transform);
                go.transform.SetAsFirstSibling();
                go.name = banditsPrefab.name;
            }
            else if (specialType < 75)  //Fight
            {
                go = Instantiate(fightPrefab, transform);
                go.transform.SetAsFirstSibling();
                go.name = fightPrefab.name;

            }
            else // Random NPC
            {
                go = Instantiate(randomNPCPrefab, transform);
                go.transform.SetAsFirstSibling();
                go.name = randomNPCPrefab.name;
            }
            go.transform.localScale = Vector3.zero;
            LeanTween.scale(go, Vector3.one, 0.3f);
        }
        else if (!specialBlock && transform.childCount > 1)
        {
            LeanTween.scale(transform.GetChild(0).gameObject, Vector3.zero, 0.3f);
            Destroy(transform.GetChild(0).gameObject, 0.3f);

        }
    }
}
