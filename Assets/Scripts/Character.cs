using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int _attackDamage;
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
        set { _isDead = value; }
    }
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
