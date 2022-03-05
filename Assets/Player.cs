using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<Player>();
            }
            return _instance;

        }
    }
    #endregion

    public float speed = 3f;

    private Point target = null;
    private Point actualPoint;

    public Point startPoint;

    public int maxHealth = 3;
    public int maxSupplies = 3;
    public int maxEnergy = 3;

    private int health = 2;
    private int supplies = 3;
    private int energy = 1;

    public StatBar healthBar;
    public StatBar suppliesBar;
    public StatBar energyBar;


    private void Start()
    {
        actualPoint = startPoint;
        healthBar.Init(maxHealth, health);
        suppliesBar.Init(maxSupplies, supplies);
        energyBar.Init(maxEnergy, energy);
    }


    public void MoveTo(Point point)
    {
        if (target == null && actualPoint.connectedTo.Contains(point))
        {
            target = point;
        }
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            if (Vector3.Distance(target.transform.position, transform.position) < 0.01f)
            {
                transform.position = target.transform.position;
                actualPoint = target;
                target = null;
            }
        }
    }
}
