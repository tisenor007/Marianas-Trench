using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackDamage;
    public float speed;
    public int health;
    protected int maxHealth;

    protected bool outOfFuel;
    protected bool hullIsBroken;
    protected bool isDead;
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getHealth()
    {
        return health;
    }
    public bool GetIsDead()
    {
        return isDead;
    }
    public bool GetIsOutOfFuel()
    {
        return outOfFuel;
    }
    public bool GetIsHullBroken()
    {
        return hullIsBroken;
    }
    public void SetIsDead(bool status)
    {
        isDead = status;
    }
    public void SetIsOutOfFuel(bool status)
    {
        outOfFuel = status;
        isDead = status;
    }
    public void SetIsHullBroken(bool status)
    {
        hullIsBroken = status;
        isDead = status;
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            health = 0;
            //isDead = true;
        }

    }
    public void Heal(int hp)
    {
        health = health + hp;

    }
}
