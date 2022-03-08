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
    public Point actualPoint;

    public Point startPoint;
    public Point endPoint;

    public int maxHealth = 3;
    public int maxSupplies = 3;
    public int maxEnergy = 3;

    public int startHealth = 3;
    public int startSupplies = 3;
    public int startEnergy = 3;

    private int _health = 2;
    private int _supplies = 3;
    private int _energy = 1;

    public int health
    {
        get => _health;
        set
        {
            value = Mathf.Clamp(value, 0, maxHealth);
            _health = value;
            healthBar.SetValue(value);
            if (value == 0)
            {
                GameManager.Instance.LooseGame();
            }
        }
    }
    public int supplies
    {
        get => _supplies;
        set
        {
            value = Mathf.Clamp(value, 0, maxSupplies);
            _supplies = value;
            suppliesBar.SetValue(value);
            if (value == 0)
            {
                GameManager.Instance.LooseGame();
            }
        }
    }
    public int energy
    {
        get => _energy;
        set
        {
            value = Mathf.Clamp(value, 0, maxEnergy);
            _energy = value;
            energyBar.SetValue(value);
            if(value == 0)
            {
                GameManager.Instance.LooseGame();
            }
        }
    }

    public StatBar healthBar;
    public StatBar suppliesBar;
    public StatBar energyBar;

    private Animator anim;


    private void Start()
    {
        actualPoint = startPoint;
        healthBar.SetValue(startHealth);
        suppliesBar.SetValue(startSupplies);
        energyBar.SetValue(startEnergy);
        _health = startHealth;
        _supplies = startSupplies;
        _energy = startEnergy;
        anim = GetComponent<Animator>();
    }


    public void MoveTo(Point point)
    {
        if (target == null && actualPoint.connectedTo.Contains(point))
        {
            target = point;
            var kido = transform.Find("kido");

            if (transform.position.x - target.transform.position.x > 0)
            {
                kido.transform.localScale = new Vector3(Mathf.Abs(kido.transform.localScale.x), kido.transform.localScale.y, kido.transform.localScale.z);
            } 
            else 
            {
                kido.transform.localScale = new Vector3(-Mathf.Abs(kido.transform.localScale.x), kido.transform.localScale.y, kido.transform.localScale.z);
            }
            anim.SetBool("isRunning", true);
            SoundManager.Instance.PlayClip(SoundManager.Instance.emptyClip);
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
                target.PlayerArrived();
                target = null;
                MapGen.Instance.AfterPlayerMoved();
                anim.SetBool("isRunning", false);

                if (actualPoint.Equals(endPoint))
                {
                    GameManager.Instance.WinGame();
                }

            }
        }
    }
}
