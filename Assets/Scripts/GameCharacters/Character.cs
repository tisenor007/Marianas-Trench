using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //VARIABES
    protected int _attackDamage;
    protected GameManager gameManager;
    public int attackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    protected float _speed;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    protected int _health;
    public int health
    {
        get { return _health; }
        set { _health = value; }
    }

    protected int maxHealth;

    protected bool _outOfFuel;
    public bool outOfFuel
    {
        get { return _outOfFuel; }
        set { _outOfFuel = value; isDead = value; }
    }

    protected bool _hullIsBroken;
    public bool hullIsBroken
    {
        get { return _hullIsBroken; }
        set { _hullIsBroken = value; isDead = value; }
    }

    protected bool _isDead;
    public bool isDead
    {
        get { return _isDead; }
        set { _isDead = value; if (value == true) { this.GetComponent<Collider2D>().enabled = false; } if (value == false) { this.GetComponent<Collider2D>().enabled = true; } }
    }

    protected Rigidbody2D rb;
   
    //For entities that spawn after game manager, or are not in the same scene as game manager before start
    //to give definition to gameManager
    protected void SetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    //Only Player can takeDamager but enemies in the future (who are "Characters") could possibly take damage
    public virtual void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            health = 0;
            //isDead = true;
        }
    }
}
