using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public enum State
    {
        roam,
        chasing,
        attacking,
        retreating
    }
    
    public float minY;
    public float maxY;

    public float randomY;
    protected bool right = true;
    protected GameObject target;
    protected Character targetScript;
    protected float targetDistance;
    protected State state;
    protected float attackDistance;
    protected int attackSpeed;
    private int attackTime = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void CheckToAttack()
    {
        if (targetScript != null)
        {
            if (targetScript.isDead == false)
            {
                if (targetDistance <= attackDistance) { state = State.attacking; }
            }
        }
    }
    protected void Attack()
    {
        if (target != null)
        {
            if (Time.time > attackTime)
            {
                //Debug.Log("WORKS");
                targetScript.TakeDamage(this.attackDamage);
                attackTime = Mathf.RoundToInt(Time.time) + attackSpeed;
            }
        }
    }
    protected void CheckToRoam()
    {   
        if (this.transform.position.y <= randomY + 2f && this.transform.position.y >= randomY - 2f)
        {
            state = State.roam;
        }
        else if (targetDistance > attackDistance) { state = State.roam; }
        else if (target == null || targetScript.isDead == true)
        {
            state = State.roam;
        }
     
    }

    protected void Roam()
    {
       
        if (right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x > (Screen.width / 100) + 2)
            {
                right = false;
            }
        }

        if (right == false)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x < -(Screen.width / 100) - 2)
            {
                right = true;
            }
        }
    }
   



}
